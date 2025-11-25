using System.Runtime.CompilerServices;

namespace Results;

public static class ResultTAsyncFunctionality
{
    [OverloadResolutionPriority(1)] public static async ValueTask<Result<T, E>> PeekAsync<T, E>(this ValueTask<Result<T, E>> task, Action<T>? onSuccess = null, Action<E>? onFail = null) where E : Error
        => (await task).Peek(onSuccess, onFail);
    [OverloadResolutionPriority(1)] public static async ValueTask<Result<T, E>> PeekAsync<T, E>(this ValueTask<Result<T, E>> task, Func<T, ValueTask>? onSuccess = null, Func<E, ValueTask>? onFail = null) where E : Error
    {
        var res = await task;
        if (res.Success && onSuccess is { }) await onSuccess(res.Value);
        else if (onFail is { }) await onFail(res.Error);
        return res;
    }
    [OverloadResolutionPriority(0)] public static async ValueTask<Result<T, E>> PeekAsync<T, E>(this ValueTask<Result<T, E>> task, Func<T, Task>? onSuccess = null, Func<E, Task>? onFail = null) where E : Error
    {
        var res = await task;
        if (res.Success && onSuccess is { }) await onSuccess(res.Value);
        else if (onFail is { }) await onFail(res.Error);
        return res;
    }

    [OverloadResolutionPriority(9)] public static async ValueTask<Result<OT, E>> BindAsync<T, E, OT>(this ValueTask<Result<T, E>> task, Func<T, Result<OT, E>> onSuccess) where E : Error
        => (await task).Bind(onSuccess);
    [OverloadResolutionPriority(9)] public static async ValueTask<Result<OT, E>> BindAsync<T, E, OT>(this ValueTask<Result<T, E>> task, Func<T, ValueTask<Result<OT, E>>> onSuccess) where E : Error
    {
        var res = await task;
        return res.Success
            ? await onSuccess(res.Value)
            : res.Error;
    }
    [OverloadResolutionPriority(8)] public static async ValueTask<Result<OT, E>> BindAsync<T, E, OT>(this ValueTask<Result<T, E>> task, Func<T, Task<Result<OT, E>>> onSuccess) where E : Error
    {
        var res = await task;
        return res.Success
            ? await onSuccess(res.Value)
            : res.Error;
    }

    [OverloadResolutionPriority(9)] public static async ValueTask<Result<OT, OE>> BindAsync<T, E, OT, OE>(this ValueTask<Result<T, E>> task, Func<T, Result<OT, OE>> onSuccess, Func<E, Result<OT, OE>> onFail) where E : Error where OE : Error
        => (await task).Bind(onSuccess, onFail);
    [OverloadResolutionPriority(9)] public static async ValueTask<Result<OT, OE>> BindAsync<T, E, OT, OE>(this ValueTask<Result<T, E>> task, Func<T, ValueTask<Result<OT, OE>>> onSuccess, Func<E, ValueTask<Result<OT, OE>>> onFail) where E : Error where OE : Error
    {
        var res = await task;
        return res.Success
            ? await onSuccess(res.Value)
            : await onFail(res.Error);
    }
    [OverloadResolutionPriority(8)] public static async ValueTask<Result<OT, OE>> BindAsync<T, E, OT, OE>(this ValueTask<Result<T, E>> task, Func<T, Task<Result<OT, OE>>> onSuccess, Func<E, Task<Result<OT, OE>>> onFail) where E : Error where OE : Error
    {
        var res = await task;
        return res.Success
            ? await onSuccess(res.Value)
            : await onFail(res.Error);
    }

    [OverloadResolutionPriority(5)] public static async ValueTask<Result<E>> BindAsync<T, E>(this ValueTask<Result<T, E>> task, Func<T, Result<E>> onSuccess) where E : Error
        => (await task).Bind(onSuccess);
    [OverloadResolutionPriority(5)] public static async ValueTask<Result<E>> BindAsync<T, E>(this ValueTask<Result<T, E>> task, Func<T, ValueTask<Result<E>>> onSuccess) where E : Error
    {
        var res = await task;
        return res.Success
            ? await onSuccess(res.Value)
            : res.Error;
    }
    [OverloadResolutionPriority(4)] public static async ValueTask<Result<E>> BindAsync<T, E>(this ValueTask<Result<T, E>> task, Func<T, Task<Result<E>>> onSuccess) where E : Error
    {
        var res = await task;
        return res.Success
            ? await onSuccess(res.Value)
            : res.Error;
    }

    [OverloadResolutionPriority(5)] public static async ValueTask<Result<OE>> BindAsync<T, E, OE>(this ValueTask<Result<T, E>> task, Func<T, Result<OE>> onSuccess, Func<E, Result<OE>> onFail) where E : Error where OE : Error
        => (await task).Bind(onSuccess, onFail);
    [OverloadResolutionPriority(5)] public static async ValueTask<Result<OE>> BindAsync<T, E, OE>(this ValueTask<Result<T, E>> task, Func<T, ValueTask<Result<OE>>> onSuccess, Func<E, ValueTask<Result<OE>>> onFail) where E : Error where OE : Error
    {
        var res = await task;
        return res.Success
            ? await onSuccess(res.Value)
            : await onFail(res.Error);
    }
    [OverloadResolutionPriority(4)] public static async ValueTask<Result<OE>> BindAsync<T, E, OE>(this ValueTask<Result<T, E>> task, Func<T, Task<Result<OE>>> onSuccess, Func<E, Task<Result<OE>>> onFail) where E : Error where OE : Error
    {
        var res = await task;
        return res.Success
            ? await onSuccess(res.Value)
            : await onFail(res.Error);
    }

    [OverloadResolutionPriority(9)] public static async ValueTask<Result<OT, E>> MapAsync<T, E, OT>(this ValueTask<Result<T, E>> task, Func<T, OT> onSuccess, Func<E, OT>? onFail = null) where E : Error
        => (await task).Map(onSuccess, onFail);
    [OverloadResolutionPriority(9)] public static async ValueTask<Result<OT, E>> MapAsync<T, E, OT>(this ValueTask<Result<T, E>> task, Func<T, ValueTask<OT>> onSuccess, Func<E, ValueTask<OT>>? onFail = null) where E : Error
    {
        var res = await task;
        return res.Success ? await onSuccess(res.Value)
            : onFail is { } ? await onFail(res.Error)
            : res.Error;
    }
    [OverloadResolutionPriority(8)] public static async ValueTask<Result<OT, E>> MapAsync<T, E, OT>(this ValueTask<Result<T, E>> task, Func<T, Task<OT>> onSuccess, Func<E, Task<OT>>? onFail = null) where E : Error
    {
        var res = await task;
        return res.Success ? await onSuccess(res.Value)
            : onFail is { } ? await onFail(res.Error)
            : res.Error;
    }

    [OverloadResolutionPriority(9)] public static async ValueTask<R> MatchAsync<T, E, R>(this ValueTask<Result<T, E>> task, Func<T, R> onSuccess, Func<E, R> onFail) where E : Error
        => (await task).Match(onSuccess, onFail);
    [OverloadResolutionPriority(9)] public static async ValueTask<R> MatchAsync<T, E, R>(this ValueTask<Result<T, E>> task, Func<T, ValueTask<R>> onSuccess, Func<E, ValueTask<R>> onFail) where E : Error
    {
        var res = await task;
        return res.Success
            ? await onSuccess(res.Value)
            : await onFail(res.Error);
    }
    [OverloadResolutionPriority(8)] public static async ValueTask<R> MatchAsync<T, E, R>(this ValueTask<Result<T, E>> task, Func<T, Task<R>> onSuccess, Func<E, Task<R>> onFail) where E : Error
    {
        var res = await task;
        return res.Success
            ? await onSuccess(res.Value)
            : await onFail(res.Error);
    }
}