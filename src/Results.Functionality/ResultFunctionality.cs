namespace Results;

public static class ResultFunctionality
{
    public static Result<E> Peek<E>(this Result<E> res, Action? onSuccess = null, Action<E>? onFail = null) where E : Error
    {
        if (res.Success) onSuccess?.Invoke();
        else onFail?.Invoke(res.Error);
        return res;
    }

    public static Result<E> Bind<E>(this Result<E> res, Func<Result<E>> onSuccess) where E : Error
        => res.Success ? onSuccess() : res;
    public static Result<OE> Bind<E, OE>(this Result<E> res, Func<Result<OE>> onSuccess, Func<E, Result<OE>> onFail) where E : Error where OE : Error
        => res.Success ? onSuccess() : onFail(res.Error);

    public static Result<R, E> Bind<R, E>(this Result<E> res, Func<Result<R, E>> onSuccess) where E : Error
        => res.Success ? onSuccess() : res.Error;
    public static Result<R, OE> Bind<R, E, OE>(this Result<E> res, Func<Result<R, OE>> onSuccess, Func<E, Result<R, OE>> onFail) where E : Error where OE : Error
        => res.Success ? onSuccess() : onFail(res.Error);

    public static Result<R, E> Map<R, E>(this Result<E> res, Func<R> onSuccess, Func<E, R>? onFail = null) where E : Error
        => res.Success ? onSuccess() : (onFail is { } ? onFail(res.Error) : res.Error);

    public static R Match<E, R>(this Result<E> res, Func<R> onSuccess, Func<E, R> onFail) where E : Error
        => res.Success ? onSuccess() : onFail(res.Error);
}