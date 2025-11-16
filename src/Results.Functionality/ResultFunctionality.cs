namespace Results;

public static class ResultFunctionality
{
    public static Result<E> Peek<E>(this Result<E> res, Action? onSuccess = null, Action<E>? onFail = null) where E : Exception
    {
        if (res.Success) onSuccess?.Invoke();
        else onFail?.Invoke(res.Exception);
        return res;
    }

    public static Result<E> Bind<E>(this Result<E> res, Func<Result<E>> onSuccess, Func<E, Result<E>>? onFail = null) where E : Exception
        => res.Success ? onSuccess() : (onFail?.Invoke(res.Exception) ?? res.Exception);
    public static Result<R, E> Bind<R, E>(this Result<E> res, Func<Result<R, E>> onSuccess, Func<E, Result<R, E>>? onFail = null) where E : Exception
        => res.Success ? onSuccess() : (onFail is { } ? onFail(res.Exception) : res.Exception);

    public static Result<E> Map<E>(this Result<E> res, Action onSuccess, Action<E>? onFail = null) where E : Exception
    {
        if (res.Success) { onSuccess(); return Result.Ok<E>(); }
        else if (onFail is { }) { onFail(res.Exception); return Result.Ok<E>(); }
        return res.Exception;
    }
    public static Result<R, E> Map<R, E>(this Result<E> res, Func<R> onSuccess, Func<E, R>? onFail = null) where E : Exception
        => res.Success ? onSuccess() : (onFail is { } ? onFail(res.Exception) : res.Exception);

    public static R Match<E, R>(this Result<E> res, Func<R> onSuccess, Func<E, R> onFail) where E : Exception
        => res.Success ? onSuccess() : onFail(res.Exception);
}