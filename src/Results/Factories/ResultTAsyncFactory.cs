namespace Results;

public partial record struct Result<T, E>
{
    public static async ValueTask<Result<T, E>> TryAsync<X>(Task<T> task, Func<X, E> catcher) where X : Exception
    {
        try { return await task; }
        catch (X e) { return catcher(e); }
    }
    public static async ValueTask<Result<T, E>> TryAsync<X>(Func<Task<T>> func, Func<X, E> catcher) where X : Exception
    {
        try { return await func(); }
        catch (X e) { return catcher(e); }
    }
    public static async ValueTask<Result<T, E>> TryAsync<X>(Task<T?> task, Func<E> onNull, Func<X, E> catcher) where X : Exception
    {
        try { return New(await task, onNull); }
        catch (X e) { return catcher(e); }
    }
    public static async ValueTask<Result<T, E>> TryAsync<X>(Func<Task<T?>> func, Func<E> onNull, Func<X, E> catcher) where X : Exception
    {
        try { return New(await func(), onNull); }
        catch (X e) { return catcher(e); }
    }
}

public static class ResultTAsyncX
{
    public static async ValueTask<Result<T, E>> Async<T, E>(this Result<T, E> r) where E : Error => r;
    public static async ValueTask<T> AsValue<T>(this Task<T> t) => await t;
}