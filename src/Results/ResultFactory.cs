namespace Results;

public static partial class Result
{
    public static Result<E> Ok<E>() where E : Exception => new();
    public static Result<E> New<E>(bool success, Func<E> onFalse) where E : Exception => success ? Ok<E>() : onFalse();
    public static Result<E> Fail<E>(E fail) where E : Exception => fail;

    public static Result<E> Try<E>(Action action) where E : Exception
    {
        try { action(); return Ok<E>(); }
        catch (E e) { return e; }
    }
    public static Result<E> Try<E>(Func<bool> action, Func<E> onFalse) where E : Exception
    {
        try { return New(action(), onFalse); }
        catch (E e) { return e; }
    }
}

public static class ResultX
{
    public static Result<E> ToResult<E>(this bool success, Func<E> onFalse) where E : Exception => Result.New(success, onFalse);
    public static Result<E> ToResult<E>(this E exception) where E : Exception => exception;
}