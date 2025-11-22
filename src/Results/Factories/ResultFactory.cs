namespace Results;

public partial record struct Result<E>
{
    public static Result<E> Ok() => new();
    public static Result<E> New(bool success, Func<E> onFalse) => success ? Ok() : onFalse();
    public static Result<E> Fail(E fail) => fail;

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
}

public static class ResultX
{
    public static Result<E> ToResult<E>(this bool success, Func<E> onFalse) where E : Error => Result<E>.New(success, onFalse);
    public static Result<E> ToResult<E>(this E error) where E : Error => error;
}