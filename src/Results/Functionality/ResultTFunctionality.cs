namespace Results;

public partial record struct Result<T, E>
{
    public Result<T, E> Peek(Action<T>? onSuccess = null, Action<E>? onFail = null)
    {
        if (Success) onSuccess?.Invoke(Value);
        else onFail?.Invoke(Error);
        return this;
    }

    public Result<OT, E> Bind<OT>(Func<T, Result<OT, E>> onSuccess)
        => Success ? onSuccess(Value) : Error;
    public Result<OT, OE> Bind<OT, OE>(Func<T, Result<OT, OE>> onSuccess, Func<E, Result<OT, OE>> onFail) where OE : Error
        => Success ? onSuccess(Value) : onFail(Error);

    public Result<E> Bind(Func<T, Result<E>> onSuccess)
        => Success ? onSuccess(Value) : Error;
    public Result<OE> Bind<OE>(Func<T, Result<OE>> onSuccess, Func<E, Result<OE>> onFail) where OE: Error
        => Success ? onSuccess(Value) : onFail(Error);

    public Result<OT, E> Map<OT>(Func<T, OT> onSuccess, Func<E, OT>? onFail = null)
        => Success ? onSuccess(Value) : onFail is { } ? onFail(Error) : Error;

    public OT Match<OT>(Func<T, OT> onSuccess, Func<E, OT> onFail)
        => Success ? onSuccess(Value) : onFail(Error);

    public T ValueOr(T @default) => Success ? Value : @default;
    public T ValueOr(Func<E, T> getter) => Success ? Value : getter(Error);

    public void Deconstruct(out T? value, out E? error)
        => (value, error) = Success ? (Value, default(E)) : (default, Error);
}

public static class ResultTNullableClass
{
    public static T? ToNullable<T, E>(this Result<T, E> r) where T : class where E : Error => r.Success ? r.Value : null;
}
public static class ResultTNullableStruct
{
    public static T? ToNullable<T, E>(this Result<T, E> r) where T : struct where E : Error => r.Success ? r.Value : null;
}