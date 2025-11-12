using Results.Exceptions;

namespace Results;

public readonly record struct Result<T> : ITypedResult<T>
{
    readonly Exception? _ex;
    readonly T? _val;

    public bool Success => _val is { };
    public T Value => Success ? _val! : throw Exception;
    public Exception Exception => _ex is { } ? _ex
        : throw (_val is null ? new UninitializedResultException() : new SuccessfulResultException());

    internal Result(T? value, Exception? exception = null) => (_val, _ex) = (value, exception) switch
    {
        (null, null) => (value, new NullValueResultException()),
        (_, { })     => (default, exception),
        _            => (value, exception)
    };

    public static implicit operator Result<T>(T? value) => new(value);
    public static implicit operator Result<T>(Exception exception) => new(default, exception);
    public static implicit operator VoidResult(Result<T> result) => result.Success ? new() : new(result.Exception);
}