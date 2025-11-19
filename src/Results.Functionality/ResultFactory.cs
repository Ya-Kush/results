namespace Results;

public static partial class Result
{
    public static Result<E> Ok<E>() where E : Error => new();
    public static Result<E> New<E>(bool success, Func<E> onFalse) where E : Error => success ? Ok<E>() : onFalse();
    public static Result<E> Fail<E>(E fail) where E : Error => fail;
}

public static class ResultX
{
    public static Result<E> ToResult<E>(this bool success, Func<E> onFalse) where E : Error => Result.New(success, onFalse);
    public static Result<E> ToResult<E>(this E error) where E : Error => error;
}