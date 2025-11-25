using System.Runtime.CompilerServices;

namespace Results;

public partial record struct Result<E>
{
    public Result<E> Peek(Action? onSuccess = null, Action<E>? onFail = null)
    {
        if (Success) onSuccess?.Invoke();
        else onFail?.Invoke(Error);
        return this;
    }

    public Result<E> Bind(Func<Result<E>> onSuccess)
        => Success ? onSuccess() : this;
    public Result<OE> Bind<OE>(Func<Result<OE>> onSuccess, Func<E, Result<OE>> onFail) where OE : Error
        => Success ? onSuccess() : onFail(Error);

    [OverloadResolutionPriority(1)]
    public Result<R, E> Bind<R>(Func<Result<R, E>> onSuccess)
        => Success ? onSuccess() : Error;
    [OverloadResolutionPriority(1)]
    public Result<R, OE> Bind<R, OE>(Func<Result<R, OE>> onSuccess, Func<E, Result<R, OE>> onFail) where OE : Error
        => Success ? onSuccess() : onFail(Error);

    public Result<R, E> Map<R>(Func<R> onSuccess, Func<E, R>? onFail = null)
        => Success ? onSuccess() : onFail is { } ? onFail(Error) : Error;

    public R Match<R>(Func<R> onSuccess, Func<E, R> onFail)
        => Success ? onSuccess() : onFail(Error);
}