using Results.Exceptions;

namespace Results;

public readonly record struct Result<T, E> : IResult<E> where E : Error
{
    readonly E? _err;
    readonly T? _val;

    public bool Success => _err is null && _val is { };
    public E Error => _err is { } ? _err : throw (_val is null ? new InvalidConditionResultException() : new SuccessfulResultException());
    public T Value => Success ? _val! : throw new FailureResultException(_err!);

    internal Result(T? value, E? error = null) => (_val, _err) = (value, error) switch
    {
        (null, null) => throw new InvalidConditionResultException(),
        (_, { })     => (default, error),
        _            => (value, error)
    };

    public static implicit operator Result<T, E>(T value) => new(value);
    public static implicit operator Result<T, E>(E error) => new(default, error);
    public static implicit operator Result<E>(Result<T, E> result) => result.Success ? new() : new(result.Error);
}