using Results.Exceptions;

namespace Results.Test;

public class ResultTTest
{
    [Fact] public void Success_EmptyCtorWithStruct()
    {
        var r = new Result<int, Exception>();
        Assert.True(r.Success);
        Assert.Throws<SuccessfulResultException>(() => r.Exception);
    }

    [Fact] public void Success_ConvertedFromValue()
    {
        var v = 1;
        Result<int, Exception> r = v;
        Assert.True(r.Success);
        Assert.Equal(v, r.Value);
    }

    [Fact] public void Success_GetException()
    {
        var v = 1;
        Result<int, Exception> r = v;
        Assert.True(r.Success);
        Assert.Throws<SuccessfulResultException>(() => r.Exception);
    }

    [Fact] public void Success_ConvertToTypelessResult()
    {
        Result<int, Exception> tr = 1;
        Result<Exception> r = tr;
        Assert.True(r.Success);
    }

    [Fact] public void Failure_EmptyCtorWithClass()
    {
        var r = new Result<string, Exception>();
        Assert.False(r.Success);
        Assert.Throws<InvalidConditionResultException>(() => r.Exception);
    }

    [Fact] public void Failure_ConvertedFromException()
    {
        var e = new Exception();
        Result<int, Exception> r = e;
        Assert.False(r.Success);
        Assert.Equal(e, r.Exception);
    }

    [Fact] public void Failure_GetValue()
    {
        var e = new Exception();
        Result<int, Exception> r = e;
        Assert.False(r.Success);
        var te = Assert.Throws<Exception>(() => r.Value);
        Assert.Equal(e, te);
    }

    [Fact] public void Failure_ConvertToTypelessResult()
    {
        var e = new Exception();
        Result<int, Exception> tr = e;
        Result<Exception> r = tr;
        Assert.False(r.Success);
        Assert.Equal(e, r.Exception);
    }

    [Fact] public void ThrowException_ConvertedFromNull()
    {
        Assert.Throws<InvalidConditionResultException>(
            () => (Result<string, Exception>)default(string)!);
    }
}