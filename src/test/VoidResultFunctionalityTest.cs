namespace Results.Test;

public class VoidResultFunctionalityTest
{
    [Fact] public void Peek()
    {
        (int val, Exception ex) expected = (1, new("fail"));
        var success = Result.Ok();
        var fail = Result.Fail<int>(expected.ex);

        (int val, Exception ex) actual = default;
        var actualS = success.Peek(() => actual.val = expected.val);
        var actualF = fail.Peek(onFail: e => actual.ex = e);

        Assert.Equal(expected, actual);
        Assert.Equal(success, actualS);
        Assert.Equal(fail, actualF);
    }

    [Fact] public void Bind()
    {
        var res = Result.Ok();
        var invoked = false;
        var a = res.Bind(() => { invoked = true; return Result.Ok(); });

        Assert.True(invoked);
    }
    [Fact] public void Bind_Fail()
    {
        var res = Result.Fail(new());
        var invoked = false;
        var a = res.Bind(() => { invoked = true; return Result.Ok(); });

        Assert.False(invoked);
        Assert.False(a.Success);
        Assert.Equal(res, a);
    }
    [Fact] public void Bind_OnFail()
    {
        var res = Result.Fail(new());
        var ex = new Exception("fail");
        var a = res.Bind(Result.Ok, _ => ex);

        Assert.False(a.Success);
        Assert.Equal(ex, a.Exception);
    }
    [Fact] public void Bind_ResultT()
    {
        var ex = new Exception("fail");
        var s = Result.Ok();
        var f = Result.Fail(new());
        Result<int> onFail(Exception e) => Result.Fail<int>(ex);
        Result<int> onSuccess() => Result.New(1);

        var st = s.Bind(onSuccess, onFail);
        var ft = f.Bind(onSuccess, onFail);

        Assert.True(st.Success);
        Assert.False(ft.Success);
        Assert.Equal(1, st.Value);
        Assert.Equal(ex, ft.Exception);
    }

    [Fact] public void Map()
    {
        var res = Result.Ok();
        var invoked = false;
        var a = res.Map(() => invoked = true);

        Assert.True(invoked);
    }
    [Fact] public void Map_Fail()
    {
        var res = Result.Fail(new());
        var invoked = false;
        var a = res.Map(() => invoked = true);

        Assert.False(invoked);
        Assert.False(a.Success);
        Assert.Equal(res, a);
    }
    [Fact] public void Map_OnFail()
    {
        var res = Result.Fail(new());
        var ex = new Exception("fail");
        var exA = default(Exception);
        var a = res.Map(() => { }, _ => { exA = ex; });

        Assert.True(a.Success);
        Assert.Equal(ex, exA);
    }
    [Fact] public void Map_ResultT()
    {
        var ex = new Exception("fail");
        var s = Result.Ok();
        var f = Result.Fail(new());
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
        var res = Result.Ok();
        var a = res.Match(() => 1, _ => 0);

        Assert.Equal(1, a);
    }
    [Fact] public void Match_OnFail()
    {
        var res = Result.Fail(new());
        var a = res.Match(() => 1, _ => 0);

        Assert.Equal(0, a);
    }
}