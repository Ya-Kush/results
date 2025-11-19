using Results.Exceptions;

namespace Results.Test;

public class ResultTTest
{
    [Fact] public void Success_EmptyCtorWithStruct()
    {
        var r = new Result<int, Error>();
        Assert.True(r.Success);
        Assert.Throws<SuccessfulResultException>(() => r.Error);
    }

    [Fact] public void Success_ConvertedFromValue()
    {
        var v = 1;
        Result<int, Error> r = v;
        Assert.True(r.Success);
        Assert.Equal(v, r.Value);
    }

    [Fact] public void Success_GetException()
    {
        var v = 1;
        Result<int, Error> r = v;
        Assert.True(r.Success);
        Assert.Throws<SuccessfulResultException>(() => r.Error);
    }

    [Fact] public void Success_ConvertToTypelessResult()
    {
        Result<int, Error> tr = 1;
        Result<Error> r = tr;
        Assert.True(r.Success);
    }

    [Fact] public void Failure_EmptyCtorWithClass()
    {
        var r = new Result<string, Error>();
        Assert.False(r.Success);
        Assert.Throws<InvalidConditionResultException>(() => r.Error);
    }

    [Fact] public void Failure_ConvertedFromException()
    {
        var e = new TestError();
        Result<int, Error> r = e;
        Assert.False(r.Success);
        Assert.Equal(e, r.Error);
    }

    [Fact] public void Failure_GetValue()
    {
        Result<int, Error> r = new TestError();
        Assert.False(r.Success);
        Assert.Throws<FailureResultException>(() => r.Value);
    }

    [Fact] public void Failure_ConvertToTypelessResult()
    {
        var e = new TestError();
        Result<int, Error> tr = e;
        Result<Error> r = tr;
        Assert.False(r.Success);
        Assert.Equal(e, r.Error);
    }

    [Fact] public void ThrowException_ConvertedFromNull()
    {
        Assert.Throws<InvalidConditionResultException>(
            () => (Result<string, Error>)default(string)!);
    }
}