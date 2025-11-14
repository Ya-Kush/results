namespace Results;

public static partial class Result
{
    public static Result<T, E> Ok<T, E>(T value) where E : Exception => value;
    public static Result<T, E> New<T, E>(T? value, Func<E> onNull) where E : Exception => value is { } ? value : onNull();
    public static Result<T, E> Fail<T, E>(E exception) where E : Exception => exception;

    public static Result<T, E> Try<T, E>(Func<T?> producer, Func<E> onNull) where E : Exception
    {
        try { return New(producer(), onNull); }
        catch (E e) { return e; }
    }
}

public static class ResultTX
{
    public static Result<T, E> ToResult<T, E>(this T? value, Func<E> onNull) where E : Exception => Result.New(value, onNull);
    public static Result<T, E> ToResult<T, E>(this E exception) where E : Exception => exception;
}