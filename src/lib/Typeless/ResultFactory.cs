namespace Results;

public readonly partial record struct Result
{
    public static Result Ok() => new();
    public static Result New(bool success, Func<Exception>? onFail = null) => success || onFail is null ? success : onFail();
    public static Result Fail(Exception fail) => fail;

    public static Result Try(Action action)
    {
        try { action(); return new(); }
        catch (Exception e) { return e; }
    }
    public static Result Try(Func<bool> action, Func<Exception>? onFail = null)
    {
        try { return New(action(), onFail); }
        catch (Exception e) { return e; }
    }
}

public static class ResultX
{
    public static Result ToResult(this bool success) => success;
    public static Result ToResult(this Exception exception) => exception;
}