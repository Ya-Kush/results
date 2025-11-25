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

    public static Result<T, E> Try<X1, X2>(Func<T> func, Func<X1, E> catcher1, Func<X2, E> catcher2) where X1 : Exception where X2 : Exception
    {
        try { return func(); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
    }
    public static Result<T, E> Try<X1, X2>(Func<T?> func, Func<E> onNull, Func<X1, E> catcher1, Func<X2, E> catcher2) where X1 : Exception where X2 : Exception
    {
        try { return New(func(), onNull); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
    }

    public static Result<T, E> Try<X1, X2, X3>(Func<T> func, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3) where X1 : Exception where X2 : Exception where X3 : Exception
    {
        try { return func(); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
    }
    public static Result<T, E> Try<X1, X2, X3>(Func<T?> func, Func<E> onNull, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3) where X1 : Exception where X2 : Exception where X3 : Exception
    {
        try { return New(func(), onNull); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
    }

    public static Result<T, E> Try<X1, X2, X3, X4>(Func<T> func, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3, Func<X4, E> catcher4) where X1 : Exception where X2 : Exception where X3 : Exception where X4 : Exception
    {
        try { return func(); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
        catch (X4 e) { return catcher4(e); }
    }
    public static Result<T, E> Try<X1, X2, X3, X4>(Func<T?> func, Func<E> onNull, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3, Func<X4, E> catcher4) where X1 : Exception where X2 : Exception where X3 : Exception where X4 : Exception
    {
        try { return New(func(), onNull); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
        catch (X4 e) { return catcher4(e); }
    }

    public static Result<T, E> Try<X1, X2, X3, X4, X5>(Func<T> func, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3, Func<X4, E> catcher4, Func<X5, E> catcher5) where X1 : Exception where X2 : Exception where X3 : Exception where X4 : Exception where X5 : Exception
    {
        try { return func(); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
        catch (X4 e) { return catcher4(e); }
        catch (X5 e) { return catcher5(e); }
    }
    public static Result<T, E> Try<X1, X2, X3, X4, X5>(Func<T?> func, Func<E> onNull, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3, Func<X4, E> catcher4, Func<X5, E> catcher5) where X1 : Exception where X2 : Exception where X3 : Exception where X4 : Exception where X5 : Exception
    {
        try { return New(func(), onNull); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
        catch (X4 e) { return catcher4(e); }
        catch (X5 e) { return catcher5(e); }
    }
}

public static class ResultTX
{
    public static Result<T, E> ToResult<T, E>(this T value) where E : Error => Result<T, E>.Ok(value);
    public static Result<T, E> ToResult<T, E>(this T? value, Func<E> onNull) where E : Error => Result<T, E>.New(value, onNull);
    public static Result<T, E> ToResult<T, E>(this E error) where E : Error => error;
}