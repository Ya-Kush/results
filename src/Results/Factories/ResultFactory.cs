namespace Results;

public partial record struct Result<E>
{
    public static Result<E> Ok() => new();
    public static Result<E> New(bool success, Func<E> onFalse) => success ? Ok() : onFalse();
    public static Result<E> Fail(E fail) => fail;
}

public partial record struct Result<E>
{
    public static Result<E> Try<X>(Action action, Func<X, E> catcher) where X : Exception
    {
        try { action(); return Ok(); }
        catch (X e) { return catcher(e); }
    }
    public static Result<E> Try<X>(Func<bool> func, Func<E> onFalse, Func<X, E> catcher) where X : Exception
    {
        try { return New(func(), onFalse); }
        catch (X e) { return catcher(e); }
    }

    public static Result<E> Try<X1, X2>(Action action, Func<X1, E> catcher1, Func<X2, E> catcher2) where X1 : Exception where X2 : Exception
    {
        try { action(); return Ok(); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
    }
    public static Result<E> Try<X1, X2>(Func<bool> func, Func<E> onFalse, Func<X1, E> catcher1, Func<X2, E> catcher2) where X1 : Exception where X2 : Exception
    {
        try { return New(func(), onFalse); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
    }

    public static Result<E> Try<X1, X2, X3>(Action action, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3) where X1 : Exception where X2 : Exception where X3 : Exception
    {
        try { action(); return Ok(); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
    }
    public static Result<E> Try<X1, X2, X3>(Func<bool> func, Func<E> onFalse, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3) where X1 : Exception where X2 : Exception where X3 : Exception
    {
        try { return New(func(), onFalse); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
    }

    public static Result<E> Try<X1, X2, X3, X4>(Action action, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3, Func<X4, E> catcher4) where X1 : Exception where X2 : Exception where X3 : Exception where X4 : Exception
    {
        try { action(); return Ok(); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
        catch (X4 e) { return catcher4(e); }
    }
    public static Result<E> Try<X1, X2, X3, X4>(Func<bool> func, Func<E> onFalse, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3, Func<X4, E> catcher4) where X1 : Exception where X2 : Exception where X3 : Exception where X4 : Exception
    {
        try { return New(func(), onFalse); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
        catch (X4 e) { return catcher4(e); }
    }

    public static Result<E> Try<X1, X2, X3, X4, X5>(Action action, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3, Func<X4, E> catcher4, Func<X5, E> catcher5) where X1 : Exception where X2 : Exception where X3 : Exception where X4 : Exception where X5 : Exception
    {
        try { action(); return Ok(); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
        catch (X4 e) { return catcher4(e); }
        catch (X5 e) { return catcher5(e); }
    }
    public static Result<E> Try<X1, X2, X3, X4, X5>(Func<bool> func, Func<E> onFalse, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3, Func<X4, E> catcher4, Func<X5, E> catcher5) where X1 : Exception where X2 : Exception where X3 : Exception where X4 : Exception where X5 : Exception
    {
        try { return New(func(), onFalse); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
        catch (X4 e) { return catcher4(e); }
        catch (X5 e) { return catcher5(e); }
    }
}

public static class ResultX
{
    public static Result<E> ToResult<E>(this bool success, Func<E> onFalse) where E : Error => Result<E>.New(success, onFalse);
    public static Result<E> ToResult<E>(this E error) where E : Error => error;
}