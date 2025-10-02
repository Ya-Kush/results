using Results.Exceptions;

namespace Results;

public readonly record struct Result<T>
{
    readonly T? _value;
    readonly internal Func<Exception>? _exceptor;

    public T Value => Success ? _value! : throw Exception;
    public Exception Exception => Failure ? _exceptor!() : throw new SuccessfulException();
    public bool Success => _exceptor is null;
    public bool Failure => _exceptor is { };

    public Result() => _exceptor = () => new UninitailizedException();
    Result(T? value, Func<Exception>? exceptor = null) => (_value, _exceptor)
        = (value, exceptor) is (null, null) ? (default, () => new NullValueException())
        : (value, exceptor) is (_, { }) ? (default, exceptor)
        : (value, default(Func<Exception>));

    public static implicit operator Result<T>(T? value) => new(value);
    public static implicit operator Result<T>(Exception exception) => new(default, () => exception);
    public static implicit operator Result<T>(Func<Exception> exceptor) => new(default, exceptor);
    public static implicit operator Result(Result<T> result) => result.Success ? true : result.Exception;
}