namespace Results;

public static class ResultOfTFunctionality
{
    public static Result<T> Peek<T>(this Result<T> res, Action<T>? onSuccess = null, Action<Exception>? onFail = null)
    {
        if (res.Success) onSuccess?.Invoke(res.Value);
        else onFail?.Invoke(res.Exception);
        return res;
    }

    public static Result<R> Bind<T, R>(this Result<T> res, Func<T, Result<R>> onSuccess, Func<Exception, Result<R>>? onFail = null)
        => res.Success ? onSuccess(res.Value) : (onFail?.Invoke(res.Exception) ?? res._exceptor!);

    public static Result<R> Map<T, R>(this Result<T> res, Func<T, R> onSuccess, Func<Exception, R>? onFail = null)
        => res.Success ? onSuccess(res.Value) : (onFail is { } ? onFail(res.Exception) : res._exceptor!);

    public static R Match<T, R>(this Result<T> res, Func<T, R> onSuccess, Func<Exception, R> onFail)
        => res.Success ? onSuccess(res.Value) : onFail(res.Exception);
}

public static class ResultOfTDeconstruction
{
    public static T ValueOr<T>(this Result<T> res, T @default) => res.Success ? res.Value : @default;
    public static T ValueOr<T>(this Result<T> res, Func<T> getter) => res.Success ? res.Value : getter();
    public static T ValueOr<T>(this Result<T> res, Func<Exception, T> getter) => res.Success ? res.Value : getter(res.Exception);
    public static T? ToNullable<T>(this Result<T> res) => res.Success ? res.Value : default;

    public static void Deconstruct<T>(this Result<T> res, out T? value, out Exception? exception)
        => (value, exception) = res.Success ? (res.Value, default(Exception)) : (default(T?), res.Exception);
}