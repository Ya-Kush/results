namespace Results;

public partial record struct Result<T, E>
{
    public static Result<T, E> Ok(T value) => value;
    public static Result<T, E> New(T? value, Func<E> onNull) => value is { } ? value : onNull();
    public static Result<T, E> Fail(E error) => error;

    public static Result<T, E> Try<X>(Func<T> func, Func<X, E> catcher) where X : Exception
    {
        try { return func(); }
        catch (X e) { return catcher(e); }
    }
    public static Result<T, E> Try<X>(Func<T?> func, Func<E> onNull, Func<X, E> catcher) where X : Exception
    {
        try { return New(func(), onNull); }
        catch (X e) { return catcher(e); }
    }
}

public static class ResultTX
{
    public static Result<T, E> ToResult<T, E>(this T value) where E : Error => Result<T, E>.Ok(value);
    public static Result<T, E> ToResult<T, E>(this T? value, Func<E> onNull) where E : Error => Result<T, E>.New(value, onNull);
    public static Result<T, E> ToResult<T, E>(this E error) where E : Error => error;
}