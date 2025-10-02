namespace Results;

public readonly partial record struct Result
{
    public static Result<T> New<T>(T? value, Func<Exception>? onNull = null) => value is { } || onNull is null ? value : onNull;
    public static Result<T> Fail<T>(Exception exception) => exception;
    public static Result<T> Fail<T>(Func<Exception> exceptor) => exceptor;

    public static Result<T> Try<T>(Func<T?> producer, Func<Exception>? onNull = null)
    {
        try { return New(producer(), onNull); }
        catch (Exception e) { return e; }
    }
}

public static class ResultOfTX
{
    public static Result<T> ToResult<T>(this T? value, Func<Exception>? onNull = null) => Result.New(value, onNull);
    public static Result<T> ToResult<T>(this Exception exception) => Result.Fail<T>(exception);
}