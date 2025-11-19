namespace Results;

public static class ResultTFunctionality
{
    public static Result<T, E> Peek<T, E>(this Result<T, E> res, Action<T>? onSuccess = null, Action<E>? onFail = null) where E : Error
    {
        if (res.Success) onSuccess?.Invoke(res.Value);
        else onFail?.Invoke(res.Error);
        return res;
    }

    public static Result<OT, E> Bind<T, E, OT>(this Result<T, E> res, Func<T, Result<OT, E>> onSuccess) where E : Error
        => res.Success ? onSuccess(res.Value) : res.Error;
    public static Result<OT, OE> Bind<T, E, OT, OE>(this Result<T, E> res, Func<T, Result<OT, OE>> onSuccess, Func<E, Result<OT, OE>> onFail) where E : Error where OE : Error
        => res.Success ? onSuccess(res.Value) : onFail(res.Error);

    public static Result<E> Bind<T, E>(this Result<T, E> res, Func<T, Result<E>> onSuccess) where E : Error
        => res.Success ? onSuccess(res.Value) : res.Error;
    public static Result<OE> Bind<T, E, OE>(this Result<T, E> res, Func<T, Result<OE>> onSuccess, Func<E, Result<OE>> onFail) where E : Error where OE: Error
        => res.Success ? onSuccess(res.Value) : onFail(res.Error);

    public static Result<OT, E> Map<T, E, OT>(this Result<T, E> res, Func<T, OT> onSuccess, Func<E, OT>? onFail = null) where E : Error
        => res.Success ? onSuccess(res.Value) : (onFail is { } ? onFail(res.Error) : res.Error);

    public static OT Match<T, E, OT>(this Result<T, E> res, Func<T, OT> onSuccess, Func<E, OT> onFail) where E : Error
        => res.Success ? onSuccess(res.Value) : onFail(res.Error);
}

public static class ResultTDeconstruction
{
    public static T ValueOr<T, E>(this Result<T, E> res, T @default) where E : Error => res.Success ? res.Value : @default;
    public static T ValueOr<T, E>(this Result<T, E> res, Func<T> getter) where E : Error => res.Success ? res.Value : getter();
    public static T ValueOr<T, E>(this Result<T, E> res, Func<E, T> getter) where E : Error => res.Success ? res.Value : getter(res.Error);

    public static T? ToNullable<T, E>(this Result<T, E> res) where T : class where E : Error => res.Success ? res.Value : null;

    public static void Deconstruct<T, E>(this Result<T, E> res, out T? value, out E? exception) where E : Error
        => (value, exception) = res.Success ? (res.Value, default(E)) : (default, res.Error);
}
public static class ResultTStructDeconstruction
{
    public static T? ToNullable<T, E>(this Result<T, E> res) where T : struct where E : Error => res.Success ? res.Value : null;
}