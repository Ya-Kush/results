using Results.Exceptions;

namespace Results.Test;

public class ResultTest
{
    [Fact] public void Success_EmptyCtor()
    {
        Result<Error> r = new();
        Assert.True(r.Success);
    }

    [Fact] public void Success_GetException()
    {
        Result<Error> r = new();
        Assert.True(r.Success);
        Assert.Throws<SuccessfulResultException>(() => r.Error);
    }

    [Fact] public void Failure_ConvertedFromException()
    {
        var e = new Error();
        Result<Error> r = e;
        Assert.False(r.Success);
        Assert.Equal(e, r.Error);
    }
}