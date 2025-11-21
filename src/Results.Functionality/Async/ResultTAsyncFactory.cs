namespace Results;

public static class ResultTAsyncX
{
    public static async ValueTask<Result<T, E>> Async<T, E>(this Result<T, E> r) where E : Error => r;
    public static async ValueTask<T> AsValue<T>(this Task<T> t) => await t;
}