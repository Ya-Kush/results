using Results.Exceptions;

namespace Results.Test;

public class ResultTest
{
    [Fact] public void Success_EmptyCtor()
    {
        Result<Exception> r = new();
        Assert.True(r.Success);
    }

    [Fact] public void Success_GetException()
    {
        Result<Exception> r = new();
        Assert.True(r.Success);
        Assert.Throws<SuccessfulResultException>(() => r.Exception);
    }

    [Fact] public void Failure_ConvertedFromException()
    {
        var e = new Exception();
        Result<Exception> r = e;
        Assert.False(r.Success);
        Assert.Equal(e, r.Exception);
    }
}