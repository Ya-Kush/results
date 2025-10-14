using Results;

namespace Test.Results;

public class ResultOfTFunctionalityTest
{
    [Fact] public void Bind()
    {
        var res = Result.New(5);
        var a = res.Bind(i => Result.New(i * 2));

        Assert.True(a.Success);
        Assert.Equal(10, a.Value);
    }
    [Fact] public void Bind_Fail()
    {
        var exception = new Exception("fail");
        var res = Result.Fail<int>(exception);
        var a = res.Bind(i => Result.New(i * 2));

        Assert.True(a.Failure);
        Assert.Equal(exception, a.Exception);
    }
    [Fact] public void Bind_OnFail()
    {
        var exception = new Exception("fail");
        var expected = 0;
        var res = Result.Fail<int>(exception);

        var a = res.Bind(i => Result.New(i * 2), e => Result.New(expected));

        Assert.True(a.Success);
        Assert.Equal(expected, a.Value);
    }

    [Fact] public void Map()
    {
        var res = Result.New(5);
        var a = res.Map(i => i * 2);

        Assert.True(a.Success);
        Assert.Equal(10, a.Value);
    }
    [Fact] public void Map_Fail()
    {
        var exception = new Exception("fail");
        var res = Result.Fail<int>(exception);
        var a = res.Map(i => i);

        Assert.True(a.Failure);
        Assert.Equal(exception, a.Exception);
    }
    [Fact] public void Map_OnFail()
    {
        var exception = new Exception("fail");
        var expected = 0;
        var res = Result.Fail<int>(exception);
        var a = res.Map(i => i, e => expected);

        Assert.True(a.Success);
        Assert.Equal(expected, a.Value);
    }


    [Fact] public void Match()
    {
        var res = Result.New(5);
        var expected = 10;
        var a = res.Match(i => i * 2, e => 0);

        Assert.Equal(expected, a);
    }
    [Fact] public void Match_OnFail()
    {
        var exception = new Exception("fail");
        var res = Result.Fail<int>(exception);
        var expected = 0;
        var a = res.Match(i => i * 2, e => expected);

        Assert.Equal(expected, a);
    }

    [Fact] public void ValueOr()
    {
        var res = Result.New(5);
        var a = res.ValueOr(10);
        var b = res.ValueOr(() => 10);
        var c = res.ValueOr(e => 10);

        Assert.Equal(5, a);
        Assert.Equal(5, b);
        Assert.Equal(5, c);
    }
    [Fact] public void ValueOr_Default()
    {
        var res = Result.Fail<int>(new Exception("fail"));
        var a = res.ValueOr(10);
        var b = res.ValueOr(() => 10);
        var c = res.ValueOr(e => 10);

        Assert.Equal(10, a);
        Assert.Equal(10, b);
        Assert.Equal(10, c);
    }
}