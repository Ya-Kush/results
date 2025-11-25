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

    public static async ValueTask<Result<T, E>> TryAsync<X1, X2>(Task<T> task, Func<X1, E> catcher1, Func<X2, E> catcher2) where X1 : Exception where X2 : Exception
    {
        try { return await task; }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
    }
    public static async ValueTask<Result<T, E>> TryAsync<X1, X2>(Func<Task<T>> func, Func<X1, E> catcher1, Func<X2, E> catcher2) where X1 : Exception where X2 : Exception
    {
        try { return await func(); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
    }
    public static async ValueTask<Result<T, E>> TryAsync<X1, X2>(Task<T?> task, Func<E> onNull, Func<X1, E> catcher1, Func<X2, E> catcher2) where X1 : Exception where X2 : Exception
    {
        try { return New(await task, onNull); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
    }
    public static async ValueTask<Result<T, E>> TryAsync<X1, X2>(Func<Task<T?>> func, Func<E> onNull, Func<X1, E> catcher1, Func<X2, E> catcher2) where X1 : Exception where X2 : Exception
    {
        try { return New(await func(), onNull); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
    }

    public static async ValueTask<Result<T, E>> TryAsync<X1, X2, X3>(Task<T> task, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3) where X1 : Exception where X2 : Exception where X3 : Exception
    {
        try { return await task; }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
    }
    public static async ValueTask<Result<T, E>> TryAsync<X1, X2, X3>(Func<Task<T>> func, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3) where X1 : Exception where X2 : Exception where X3 : Exception
    {
        try { return await func(); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
    }
    public static async ValueTask<Result<T, E>> TryAsync<X1, X2, X3>(Task<T?> task, Func<E> onNull, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3) where X1 : Exception where X2 : Exception where X3 : Exception
    {
        try { return New(await task, onNull); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
    }
    public static async ValueTask<Result<T, E>> TryAsync<X1, X2, X3>(Func<Task<T?>> func, Func<E> onNull, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3) where X1 : Exception where X2 : Exception where X3 : Exception
    {
        try { return New(await func(), onNull); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
    }

    public static async ValueTask<Result<T, E>> TryAsync<X1, X2, X3, X4>(Task<T> task, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3, Func<X4, E> catcher4) where X1 : Exception where X2 : Exception where X3 : Exception where X4 : Exception
    {
        try { return await task; }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
        catch (X4 e) { return catcher4(e); }
    }
    public static async ValueTask<Result<T, E>> TryAsync<X1, X2, X3, X4>(Func<Task<T>> func, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3, Func<X4, E> catcher4) where X1 : Exception where X2 : Exception where X3 : Exception where X4 : Exception
    {
        try { return await func(); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
        catch (X4 e) { return catcher4(e); }
    }
    public static async ValueTask<Result<T, E>> TryAsync<X1, X2, X3, X4>(Task<T?> task, Func<E> onNull, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3, Func<X4, E> catcher4) where X1 : Exception where X2 : Exception where X3 : Exception where X4 : Exception
    {
        try { return New(await task, onNull); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
        catch (X4 e) { return catcher4(e); }
    }
    public static async ValueTask<Result<T, E>> TryAsync<X1, X2, X3, X4>(Func<Task<T?>> func, Func<E> onNull, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3, Func<X4, E> catcher4) where X1 : Exception where X2 : Exception where X3 : Exception where X4 : Exception
    {
        try { return New(await func(), onNull); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
        catch (X4 e) { return catcher4(e); }
    }

    public static async ValueTask<Result<T, E>> TryAsync<X1, X2, X3, X4, X5>(Task<T> task, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3, Func<X4, E> catcher4, Func<X5, E> catcher5) where X1 : Exception where X2 : Exception where X3 : Exception where X4 : Exception where X5 : Exception
    {
        try { return await task; }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
        catch (X4 e) { return catcher4(e); }
        catch (X5 e) { return catcher5(e); }
    }
    public static async ValueTask<Result<T, E>> TryAsync<X1, X2, X3, X4, X5>(Func<Task<T>> func, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3, Func<X4, E> catcher4, Func<X5, E> catcher5) where X1 : Exception where X2 : Exception where X3 : Exception where X4 : Exception where X5 : Exception
    {
        try { return await func(); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
        catch (X4 e) { return catcher4(e); }
        catch (X5 e) { return catcher5(e); }
    }
    public static async ValueTask<Result<T, E>> TryAsync<X1, X2, X3, X4, X5>(Task<T?> task, Func<E> onNull, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3, Func<X4, E> catcher4, Func<X5, E> catcher5) where X1 : Exception where X2 : Exception where X3 : Exception where X4 : Exception where X5 : Exception
    {
        try { return New(await task, onNull); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
        catch (X4 e) { return catcher4(e); }
        catch (X5 e) { return catcher5(e); }
    }
    public static async ValueTask<Result<T, E>> TryAsync<X1, X2, X3, X4, X5>(Func<Task<T?>> func, Func<E> onNull, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3, Func<X4, E> catcher4, Func<X5, E> catcher5) where X1 : Exception where X2 : Exception where X3 : Exception where X4 : Exception where X5 : Exception
    {
        try { return New(await func(), onNull); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
        catch (X4 e) { return catcher4(e); }
        catch (X5 e) { return catcher5(e); }
    }
}

public static class ResultTAsyncX
{
    public static async ValueTask<Result<T, E>> Async<T, E>(this Result<T, E> r) where E : Error => r;
    public static async ValueTask<T> AsValue<T>(this Task<T> t) => await t;
}