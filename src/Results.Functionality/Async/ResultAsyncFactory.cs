namespace Results;

public static class ResultAsyncX
{
    public static async ValueTask<Result<E>> Async<E>(this Result<E> r) where E : Error => r;
    public static async ValueTask AsValue(this Task t) => await t;
}