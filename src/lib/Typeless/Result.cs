using Results.Exceptions;

namespace Results;

public readonly partial record struct Result(Exception? Exception)
{
    readonly static UninitializedResultException UninitedException = new();
    readonly Exception? _ex = Exception;
    readonly bool _inited = true;

    public Exception Exception => _ex is { } ? _ex
        : _inited is false ? UninitedException
        : throw new SuccessfulResultException();
    public bool Success => _inited && _ex is null;
    public bool Failure => _ex is { } || _inited is false;

    public static implicit operator Result(bool success) => success ? new() : new(new FailureResultException());
    public static implicit operator Result(Exception exception) => new(exception);
}