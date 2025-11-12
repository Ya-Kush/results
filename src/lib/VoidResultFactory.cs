using Results.Exceptions;

namespace Results;

public static partial class Result
{
    public static VoidResult Ok() => new();
    public static VoidResult New(bool success, Func<Exception>? onFail = null) => success ? Ok() : (onFail ?? (() => new FailureResultException()))();
    public static VoidResult Fail(Exception fail) => fail;

    public static VoidResult Try(Action action)
    {
        try { action(); return Ok(); }
        catch (Exception e) { return e; }
    }
    public static VoidResult Try(Func<bool> action, Func<Exception>? onFail = null)
    {
        try { return New(action(), onFail); }
        catch (Exception e) { return e; }
    }
}

public static class ResultX
{
    public static VoidResult ToResult(this bool success) => Result.New(success);
    public static VoidResult ToResult(this Exception exception) => exception;
}