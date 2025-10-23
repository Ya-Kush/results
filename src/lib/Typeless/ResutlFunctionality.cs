namespace Results;

public static class ResultFunctionality
{
    public static Result Peek(this Result res, Action? onSuccess = null, Action<Exception>? onFail = null)
    {
        if (res.Success) onSuccess?.Invoke();
        else onFail?.Invoke(res.Exception);
        return res;
    }

    public static Result Bind(this Result res, Func<Result> onSuccess, Func<Exception, Result>? onFail = null)
        => res.Success ? onSuccess() : (onFail?.Invoke(res.Exception) ?? res.Exception);
    public static Result<T> Bind<T>(this Result res, Func<Result<T>> onSuccess, Func<Exception, Result<T>>? onFail = null)
        => res.Success ? onSuccess() : (onFail?.Invoke(res.Exception) ?? res.Exception);

    public static Result Map(this Result res, Action onSuccess, Action<Exception>? onFail = null)
    {
        if (res.Success) { onSuccess(); return true; }
        else if (onFail is { }) { onFail(res.Exception); return true; }
        return res.Exception;
    }
    public static Result<T> Map<T>(this Result res, Func<T> onSuccess, Func<Exception, T>? onFail = null)
        => res.Success ? onSuccess() : (onFail is { } ? onFail(res.Exception) : res.Exception);

    public static R Match<R>(this Result res, Func<R> onSuccess, Func<Exception, R> onFail)
        => res.Success ? onSuccess() : onFail(res.Exception);
}