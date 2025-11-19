using Results.Exceptions;

namespace Results;

public readonly record struct Result<E> : IResult<E> where E : Error
{
    readonly E? _ex;

    public bool Success => _ex is null;
    public E Error => _ex is { } ? _ex : throw new SuccessfulResultException();

    public Result() { }
    internal Result(E? exception) => _ex = exception;

    public static implicit operator Result<E>(E? exception) => new(exception);
}