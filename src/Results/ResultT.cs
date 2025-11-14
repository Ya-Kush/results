using Results.Exceptions;

namespace Results;

public readonly record struct Result<T, E> : IResult<E> where E : Exception
{
    readonly E? _ex;
    readonly T? _val;

    public bool Success => _ex is null && _val is { };
    public E Exception => _ex is { } ? _ex : throw (_val is null ? new InvalidConditionResultException() : new SuccessfulResultException());
    public T Value => Success ? _val! : throw Exception;

    internal Result(T? value, E? exception = null) => (_val, _ex) = (value, exception) switch
    {
        (null, null) => throw new InvalidConditionResultException(),
        (_, { })     => (default, exception),
        _            => (value, exception)
    };

    public static implicit operator Result<T, E>(T value) => new(value);
    public static implicit operator Result<T, E>(E exception) => new(default, exception);
    public static implicit operator Result<E>(Result<T, E> result) => result.Success ? new() : new(result.Exception);
}