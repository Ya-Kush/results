namespace Results;

public static class ResultTFunctionality
{
    public static Result<T, E> Peek<T, E>(this Result<T, E> res, Action<T>? onSuccess = null, Action<Exception>? onFail = null) where E : Exception
    {
        if (res.Success) onSuccess?.Invoke(res.Value);
        else onFail?.Invoke(res.Exception);
        return res;
    }

    public static Result<R, E> Bind<T, E, R>(this Result<T, E> res, Func<T, Result<R, E>> onSuccess, Func<Exception, Result<R, E>>? onFail = null) where E : Exception
        => res.Success ? onSuccess(res.Value) : (onFail?.Invoke(res.Exception) ?? res.Exception);
    public static Result<E> Bind<T, E>(this Result<T, E> res, Func<T, Result<E>> onSuccess, Func<Exception, Result<E>>? onFail = null) where E : Exception
        => res.Success ? onSuccess(res.Value) : (onFail?.Invoke(res.Exception) ?? res.Exception);

    public static Result<R, E> Map<T, E, R>(this Result<T, E> res, Func<T, R> onSuccess, Func<E, R>? onFail = null) where E : Exception
        => res.Success ? onSuccess(res.Value) : (onFail is { } ? onFail(res.Exception) : res.Exception);
    public static Result<E> Map<T, E>(this Result<T, E> res, Action<T> onSuccess, Action<E>? onFail = null) where E : Exception
    {
        if (res.Success) { onSuccess(res.Value); return Result.Ok<E>(); }
        else if (onFail is { }) { onFail(res.Exception); return Result.Ok<E>(); }
        return res.Exception;
    }

    public static R Match<T, E, R>(this Result<T, E> res, Func<T, R> onSuccess, Func<E, R> onFail) where E : Exception
        => res.Success ? onSuccess(res.Value) : onFail(res.Exception);
}

public static class ResultTDeconstruction
{
    public static T ValueOr<T, E>(this Result<T, E> res, T @default) where E : Exception => res.Success ? res.Value : @default;
    public static T ValueOr<T, E>(this Result<T, E> res, Func<T> getter) where E : Exception => res.Success ? res.Value : getter();
    public static T ValueOr<T, E>(this Result<T, E> res, Func<E, T> getter) where E : Exception => res.Success ? res.Value : getter(res.Exception);

    public static T? ToNullable<T, E>(this Result<T, E> res) where T : class where E : Exception => res.Success ? res.Value : null;

    public static void Deconstruct<T, E>(this Result<T, E> res, out T? value, out E? exception) where E : Exception
        => (value, exception) = res.Success ? (res.Value, default(E)) : (default, res.Exception);
}
public static class ResultTStructDeconstruction
{
    public static T? ToNullable<T, E>(this Result<T, E> res) where T : struct where E : Exception => res.Success ? res.Value : null;
}