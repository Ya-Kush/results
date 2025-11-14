using Results.Exceptions;

namespace Results;

public readonly record struct Result<E>(E? Exception) : IResult<E> where E : Exception
{
    readonly E? _ex = Exception;
    readonly bool _inited = true;

    public bool Success => _inited && _ex is null;
    public E Exception => _ex is { } ? _ex
        : throw (_inited is false ? new UninitializedResultException() : new SuccessfulResultException());

    public static implicit operator Result<E>(E? exception) => new(exception);
}