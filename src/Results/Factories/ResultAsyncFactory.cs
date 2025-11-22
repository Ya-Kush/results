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
}

public static class ResultAsyncX
{
    public static async ValueTask<Result<E>> Async<E>(this Result<E> r) where E : Error => r;
    public static async ValueTask AsValue(this Task t) => await t;
}