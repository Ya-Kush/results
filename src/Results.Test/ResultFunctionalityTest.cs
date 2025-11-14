namespace Results.Test;

public class ResultFunctionalityTest
{
    [Fact] public void Peek()
    {
        (int val, Exception ex) expected = (1, new("fail"));
        var success = Result.Ok<Exception>();
        var fail = Result.Fail(expected.ex);

        (int val, Exception ex) actual = default;
        var actualS = success.Peek(() => actual.val = expected.val);
        var actualF = fail.Peek(onFail: e => actual.ex = e);

        Assert.Equal(expected, actual);
        Assert.Equal(success, actualS);
        Assert.Equal(fail, actualF);
    }

    [Fact] public void Bind()
    {
        var res = Result.Ok<Exception>();
        var invoked = false;
        var a = res.Bind(() => { invoked = true; return Result.Ok<Exception>(); });

        Assert.True(invoked);
    }
    [Fact] public void Bind_Fail()
    {
        var res = Result.Fail<Exception>(new());
        var invoked = false;
        var a = res.Bind(() => { invoked = true; return Result.Ok<Exception>(); });

        Assert.False(invoked);
        Assert.False(a.Success);
        Assert.Equal(res, a);
    }
    [Fact] public void Bind_OnFail()
    {
        var res = Result.Fail<Exception>(new());
        var ex = new Exception("fail");
        var a = res.Bind(Result.Ok<Exception>, _ => ex);

        Assert.False(a.Success);
        Assert.Equal(ex, a.Exception);
    }
    [Fact] public void Bind_ResultT()
    {
        var ex = new Exception("fail");
        var s = Result.Ok<Exception>();
        var f = Result.Fail<Exception>(new());
        Result<int, Exception> onFail(Exception e) => Result.Fail<int, Exception>(ex);
        Result<int, Exception> onSuccess() => Result.Ok<int, Exception>(1);

        var st = s.Bind(onSuccess, onFail);
        var ft = f.Bind(onSuccess, onFail);

        Assert.True(st.Success);
        Assert.False(ft.Success);
        Assert.Equal(1, st.Value);
        Assert.Equal(ex, ft.Exception);
    }

    [Fact] public void Map()
    {
        var res = Result.Ok<Exception>();
        var invoked = false;
        var a = res.Map(() => invoked = true);

        Assert.True(invoked);
    }
    [Fact] public void Map_Fail()
    {
        var res = Result.Fail<Exception>(new());
        var invoked = false;
        var a = res.Map(() => invoked = true);

        Assert.False(invoked);
        Assert.False(a.Success);
        Assert.Equal(res, a);
    }
    [Fact] public void Map_OnFail()
    {
        var res = Result.Fail<Exception>(new());
        var ex = new Exception("fail");
        var exA = default(Exception);
        var a = res.Map(() => { }, _ => { exA = ex; });

        Assert.True(a.Success);
        Assert.Equal(ex, exA);
    }
    [Fact] public void Map_ResultT()
    {
        var ex = new Exception("fail");
        var s = Result.Ok<Exception>();
        var f = Result.Fail<Exception>(new());
        int onFail(Exception e) => 0;
        int onSuccess() => 1;

        var st = s.Map(onSuccess, onFail);
        var ft = f.Map(onSuccess, onFail);

        Assert.True(st.Success);
        Assert.True(ft.Success);
        Assert.Equal(1, st.Value);
        Assert.Equal(0, ft.Value);
    }

    [Fact] public void Match()
    {
        var res = Result.Ok<Exception>();
        var a = res.Match(() => 1, _ => 0);

        Assert.Equal(1, a);
    }
    [Fact] public void Match_OnFail()
    {
        var res = Result.Fail<Exception>(new());
        var a = res.Match(() => 1, _ => 0);

        Assert.Equal(0, a);
    }
}