namespace Results;

public static partial class Result
{
    public static Result<T, E> Ok<T, E>(T value) where T : notnull where E : Error => value;
    public static Result<T, E> New<T, E>(T? value, Func<E> onNull) where E : Error => value is { } ? value : onNull();
    public static Result<T, E> Fail<T, E>(E error) where E : Error => error;
}

public static class ResultTX
{
    public static Result<T, E> ToResult<T, E>(this T value) where T : notnull where E : Error => Result.Ok<T, E>(value);
    public static Result<T, E> ToResult<T, E>(this T? value, Func<E> onNull) where E : Error => Result.New(value, onNull);
    public static Result<T, E> ToResult<T, E>(this E error) where E : Error => error;
}