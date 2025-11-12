using Results.Exceptions;

namespace Results;

public readonly record struct VoidResult(Exception? Exception) : IVoidResult
{
    readonly Exception? _ex = Exception;
    readonly bool _inited = true;

    public bool Success => _inited && _ex is null;
    public Exception Exception => _ex is { } ? _ex
        : throw (_inited is false ? new UninitializedResultException() : new SuccessfulResultException());

    public static implicit operator VoidResult(Exception exception) => new(exception);
}