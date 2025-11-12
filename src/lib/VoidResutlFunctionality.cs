namespace Results;

public static class VoidResultFunctionality
{
    public static VoidResult Peek(this VoidResult res, Action? onSuccess = null, Action<Exception>? onFail = null)
    {
        if (res.Success) onSuccess?.Invoke();
        else onFail?.Invoke(res.Exception);
        return res;
    }

    public static VoidResult Bind(this VoidResult res, Func<VoidResult> onSuccess, Func<Exception, VoidResult>? onFail = null)
        => res.Success ? onSuccess() : (onFail?.Invoke(res.Exception) ?? res.Exception);
    public static Result<T> Bind<T>(this VoidResult res, Func<Result<T>> onSuccess, Func<Exception, Result<T>>? onFail = null)
        => res.Success ? onSuccess() : (onFail?.Invoke(res.Exception) ?? res.Exception);

    public static VoidResult Map(this VoidResult res, Action onSuccess, Action<Exception>? onFail = null)
    {
        if (res.Success) { onSuccess(); return Result.Ok(); }
        else if (onFail is { }) { onFail(res.Exception); return Result.Ok(); }
        return res.Exception;
    }
    public static Result<T> Map<T>(this VoidResult res, Func<T> onSuccess, Func<Exception, T>? onFail = null)
        => res.Success ? onSuccess() : (onFail is { } ? onFail(res.Exception) : res.Exception);

    public static R Match<R>(this VoidResult res, Func<R> onSuccess, Func<Exception, R> onFail)
        => res.Success ? onSuccess() : onFail(res.Exception);
}