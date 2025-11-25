namespace Results;

public partial record struct Result<E>
{
    public static async ValueTask<Result<E>> TryAsync<X>(Task task, Func<X, E> catcher) where X : Exception
    {
        try { await task; return Ok(); }
        catch (X e) { return catcher(e); }
    }
    public static async ValueTask<Result<E>> TryAsync<X>(Func<Task> func, Func<X, E> catcher) where X : Exception
    {
        try { await func(); return Ok(); }
        catch (X e) { return catcher(e); }
    }
    public static async ValueTask<Result<E>> TryAsync<X>(Task<bool> task, Func<E> onFalse, Func<X, E> catcher) where X : Exception
    {
        try { return New(await task, onFalse); }
        catch (X e) { return catcher(e); }
    }
    public static async ValueTask<Result<E>> TryAsync<X>(Func<Task<bool>> func, Func<E> onFalse, Func<X, E> catcher) where X : Exception
    {
        try { return New(await func(), onFalse); }
        catch (X e) { return catcher(e); }
    }

    public static async ValueTask<Result<E>> TryAsync<X1, X2>(Task task, Func<X1, E> catcher1, Func<X2, E> catcher2) where X1 : Exception where X2 : Exception
    {
        try { await task; return Ok(); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
    }
    public static async ValueTask<Result<E>> TryAsync<X1, X2>(Func<Task> func, Func<X1, E> catcher1, Func<X2, E> catcher2) where X1 : Exception where X2 : Exception
    {
        try { await func(); return Ok(); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
    }
    public static async ValueTask<Result<E>> TryAsync<X1, X2>(Task<bool> task, Func<E> onFalse, Func<X1, E> catcher1, Func<X2, E> catcher2) where X1 : Exception where X2 : Exception
    {
        try { return New(await task, onFalse); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
    }
    public static async ValueTask<Result<E>> TryAsync<X1, X2>(Func<Task<bool>> func, Func<E> onFalse, Func<X1, E> catcher1, Func<X2, E> catcher2) where X1 : Exception where X2 : Exception
    {
        try { return New(await func(), onFalse); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
    }

    public static async ValueTask<Result<E>> TryAsync<X1, X2, X3>(Task task, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3) where X1 : Exception where X2 : Exception where X3 : Exception
    {
        try { await task; return Ok(); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
    }
    public static async ValueTask<Result<E>> TryAsync<X1, X2, X3>(Func<Task> func, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3) where X1 : Exception where X2 : Exception where X3 : Exception
    {
        try { await func(); return Ok(); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
    }
    public static async ValueTask<Result<E>> TryAsync<X1, X2, X3>(Task<bool> task, Func<E> onFalse, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3) where X1 : Exception where X2 : Exception where X3 : Exception
    {
        try { return New(await task, onFalse); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
    }
    public static async ValueTask<Result<E>> TryAsync<X1, X2, X3>(Func<Task<bool>> func, Func<E> onFalse, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3) where X1 : Exception where X2 : Exception where X3 : Exception
    {
        try { return New(await func(), onFalse); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
    }

    public static async ValueTask<Result<E>> TryAsync<X1, X2, X3, X4>(Task task, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3, Func<X4, E> catcher4) where X1 : Exception where X2 : Exception where X3 : Exception where X4 : Exception
    {
        try { await task; return Ok(); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
        catch (X4 e) { return catcher4(e); }
    }
    public static async ValueTask<Result<E>> TryAsync<X1, X2, X3, X4>(Func<Task> func, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3, Func<X4, E> catcher4) where X1 : Exception where X2 : Exception where X3 : Exception where X4 : Exception
    {
        try { await func(); return Ok(); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
        catch (X4 e) { return catcher4(e); }
    }
    public static async ValueTask<Result<E>> TryAsync<X1, X2, X3, X4>(Task<bool> task, Func<E> onFalse, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3, Func<X4, E> catcher4) where X1 : Exception where X2 : Exception where X3 : Exception where X4 : Exception
    {
        try { return New(await task, onFalse); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
        catch (X4 e) { return catcher4(e); }
    }
    public static async ValueTask<Result<E>> TryAsync<X1, X2, X3, X4>(Func<Task<bool>> func, Func<E> onFalse, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3, Func<X4, E> catcher4) where X1 : Exception where X2 : Exception where X3 : Exception where X4 : Exception
    {
        try { return New(await func(), onFalse); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
        catch (X4 e) { return catcher4(e); }
    }

    public static async ValueTask<Result<E>> TryAsync<X1, X2, X3, X4, X5>(Task task, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3, Func<X4, E> catcher4, Func<X5, E> catcher5) where X1 : Exception where X2 : Exception where X3 : Exception where X4 : Exception where X5 : Exception
    {
        try { await task; return Ok(); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
        catch (X4 e) { return catcher4(e); }
        catch (X5 e) { return catcher5(e); }
    }
    public static async ValueTask<Result<E>> TryAsync<X1, X2, X3, X4, X5>(Func<Task> func, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3, Func<X4, E> catcher4, Func<X5, E> catcher5) where X1 : Exception where X2 : Exception where X3 : Exception where X4 : Exception where X5 : Exception
    {
        try { await func(); return Ok(); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
        catch (X4 e) { return catcher4(e); }
        catch (X5 e) { return catcher5(e); }
    }
    public static async ValueTask<Result<E>> TryAsync<X1, X2, X3, X4, X5>(Task<bool> task, Func<E> onFalse, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3, Func<X4, E> catcher4, Func<X5, E> catcher5) where X1 : Exception where X2 : Exception where X3 : Exception where X4 : Exception where X5 : Exception
    {
        try { return New(await task, onFalse); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
        catch (X4 e) { return catcher4(e); }
        catch (X5 e) { return catcher5(e); }
    }
    public static async ValueTask<Result<E>> TryAsync<X1, X2, X3, X4, X5>(Func<Task<bool>> func, Func<E> onFalse, Func<X1, E> catcher1, Func<X2, E> catcher2, Func<X3, E> catcher3, Func<X4, E> catcher4, Func<X5, E> catcher5) where X1 : Exception where X2 : Exception where X3 : Exception where X4 : Exception where X5 : Exception
    {
        try { return New(await func(), onFalse); }
        catch (X1 e) { return catcher1(e); }
        catch (X2 e) { return catcher2(e); }
        catch (X3 e) { return catcher3(e); }
        catch (X4 e) { return catcher4(e); }
        catch (X5 e) { return catcher5(e); }
    }
}

public static class ResultAsyncX
{
    public static async ValueTask<Result<E>> Async<E>(this Result<E> r) where E : Error => r;
    public static async ValueTask AsValue(this Task t) => await t;
}