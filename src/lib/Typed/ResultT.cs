using Results.Exceptions;

namespace Results;

public readonly record struct Result<T>
{
    readonly static UninitializedResultException UninitedException = new();
    readonly Exception? _ex;
    readonly T? _val;

    public T Value => Success ? _val! : throw Exception;
    public Exception Exception => _ex is { } ? _ex
        : _val is null ? UninitedException
        : throw new SuccessfulResultException();
    public bool Success => _ex is null && _val is { };
    public bool Failure => _ex is { };

    Result(T? value, Exception? exception = null) => (_val, _ex) = (value, exception) switch
    {
        (null, null) => (value, new NullValueResultException()),
        (_, { })     => (default, exception),
        _            => (value, exception)
    };

    public static implicit operator Result<T>(T? value) => new(value);
    public static implicit operator Result<T>(Exception exception) => new(default, exception);
    public static implicit operator Result(Result<T> result) => result.Success ? new() : new(result.Exception);
}