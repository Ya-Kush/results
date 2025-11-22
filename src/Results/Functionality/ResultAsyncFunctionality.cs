using System.Runtime.CompilerServices;

namespace Results;

public static class ResultAsyncFunctionality
{
    [OverloadResolutionPriority(1)]
    public static async ValueTask<Result<E>> PeekAsync<E>(this ValueTask<Result<E>> task, Func<ValueTask>? onSuccess = null, Func<E, ValueTask>? onFail = null) where E : Error
    {
        var res = await task;
        if (res.Success && onSuccess is { }) await onSuccess();
        else if (onFail is { }) await onFail(res.Error);
        return res;
    }
    public static async ValueTask<Result<E>> PeekAsync<E>(this ValueTask<Result<E>> task, Func<Task>? onSuccess = null, Func<E, Task>? onFail = null) where E : Error
    {
        var res = await task;
        if (res.Success && onSuccess is { }) await onSuccess();
        else if (onFail is { }) await onFail(res.Error);
        return res;
    }
    public static async ValueTask<Result<E>> PeekAsync<E>(this ValueTask<Result<E>> task, Action? onSuccess = null, Action<E>? onFail = null) where E : Error
        => (await task).Peek(onSuccess, onFail);

    [OverloadResolutionPriority(1)]
    public static async ValueTask<Result<E>> BindAsync<E>(this ValueTask<Result<E>> task, Func<ValueTask<Result<E>>> onSuccess) where E : Error
    {
        var res = await task;
        return res.Success
            ? await onSuccess()
            : res;
    }
    public static async ValueTask<Result<E>> BindAsync<E>(this ValueTask<Result<E>> task, Func<Task<Result<E>>> onSuccess) where E : Error
    {
        var res = await task;
        return res.Success
            ? await onSuccess()
            : res;
    }
    public static async ValueTask<Result<E>> BindAsync<E>(this ValueTask<Result<E>> task, Func<Result<E>> onSuccess) where E : Error
        => (await task).Bind(onSuccess);

    [OverloadResolutionPriority(1)]
    public static async ValueTask<Result<OE>> BindAsync<E, OE>(this ValueTask<Result<E>> task, Func<Task<Result<OE>>> onSuccess, Func<E, Task<Result<OE>>> onFail) where E : Error where OE : Error
    {
        var res = await task;
        return res.Success
            ? await onSuccess()
            : await onFail(res.Error);
    }
    public static async ValueTask<Result<OE>> BindAsync<E, OE>(this ValueTask<Result<E>> task, Func<ValueTask<Result<OE>>> onSuccess, Func<E, ValueTask<Result<OE>>> onFail) where E : Error where OE : Error
    {
        var res = await task;
        return res.Success
            ? await onSuccess()
            : await onFail(res.Error);
    }
    public static async ValueTask<Result<OE>> BindAsync<E, OE>(this ValueTask<Result<E>> task, Func<Result<OE>> onSuccess, Func<E, Result<OE>> onFail) where E : Error where OE : Error
        => (await task).Bind(onSuccess, onFail);

    [OverloadResolutionPriority(1)]
    public static async ValueTask<Result<T, E>> BindAsync<T, E>(this ValueTask<Result<E>> task, Func<ValueTask<Result<T, E>>> onSuccess) where E : Error
    {
        var res = await task;
        return res.Success
            ? await onSuccess()
            : res.Error;
    }
    public static async ValueTask<Result<T, E>> BindAsync<T, E>(this ValueTask<Result<E>> task, Func<Task<Result<T, E>>> onSuccess) where E : Error
    {
        var res = await task;
        return res.Success
            ? await onSuccess()
            : res.Error;
    }
    public static async ValueTask<Result<T, E>> BindAsync<T, E>(this ValueTask<Result<E>> task, Func<Result<T, E>> onSuccess) where E : Error
        => (await task).Bind(onSuccess);

    [OverloadResolutionPriority(1)]
    public static async ValueTask<Result<T, OE>> BindAsync<T, E, OE>(this ValueTask<Result<E>> task, Func<ValueTask<Result<T, OE>>> onSuccess, Func<E, ValueTask<Result<T, OE>>> onFail) where E : Error where OE : Error
    {
        var res = await task;
        return res.Success
            ? await onSuccess()
            : await onFail(res.Error);
    }
    public static async ValueTask<Result<T, OE>> BindAsync<T, E, OE>(this ValueTask<Result<E>> task, Func<Task<Result<T, OE>>> onSuccess, Func<E, Task<Result<T, OE>>> onFail) where E : Error where OE : Error
    {
        var res = await task;
        return res.Success
            ? await onSuccess()
            : await onFail(res.Error);
    }
    public static async ValueTask<Result<T, OE>> BindAsync<T, E, OE>(this ValueTask<Result<E>> task, Func<Result<T, OE>> onSuccess, Func<E, Result<T, OE>> onFail) where E : Error where OE : Error
        => (await task).Bind(onSuccess, onFail);

    [OverloadResolutionPriority(1)]
    public static async ValueTask<Result<T, E>> MapAsync<T, E>(this ValueTask<Result<E>> task, Func<ValueTask<T>> onSuccess, Func<E, ValueTask<T>>? onFail = null) where E : Error
    {
        var res = await task;
        return res.Success ? await onSuccess()
            : onFail is { } ? await onFail(res.Error)
            : res.Error;
    }
    public static async ValueTask<Result<T, E>> MapAsync<T, E>(this ValueTask<Result<E>> task, Func<Task<T>> onSuccess, Func<E, Task<T>>? onFail = null) where E : Error
    {
        var res = await task;
        return res.Success ? await onSuccess()
            : onFail is { } ? await onFail(res.Error)
            : res.Error;
    }
    public static async ValueTask<Result<T, E>> MapAsync<T, E>(this ValueTask<Result<E>> task, Func<T> onSuccess, Func<E, T>? onFail = null) where E : Error
        => (await task).Map(onSuccess, onFail);

    [OverloadResolutionPriority(1)]
    public static async ValueTask<R> MatchAsync<R, E>(this ValueTask<Result<E>> task, Func<ValueTask<R>> onSuccess, Func<E, ValueTask<R>> onFail) where E : Error
    {
        var res = await task;
        return res.Success
            ? await onSuccess()
            : await onFail(res.Error);
    }
    public static async ValueTask<R> MatchAsync<E, R>(this ValueTask<Result<E>> task, Func<Task<R>> onSuccess, Func<E, Task<R>> onFail) where E : Error
    {
        var res = await task;
        return res.Success
            ? await onSuccess()
            : await onFail(res.Error);
    }
    public static async ValueTask<R> MatchAsync<E, R>(this ValueTask<Result<E>> task, Func<R> onSuccess, Func<E, R> onFail) where E : Error
        => (await task).Match(onSuccess, onFail);
}