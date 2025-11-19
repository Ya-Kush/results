namespace Results.Functionality.Test;

public class ResultFunctionalityTest
{
    [Fact] public void Peek()
    {
        (int val, Error ex) expected = (1, new("fail"));
        var success = Result.Ok<Error>();
        var fail = Result.Fail(expected.ex);

        (int val, Error ex) actual = default;
        var actualS = success.Peek(() => actual.val = expected.val);
        var actualF = fail.Peek(onFail: e => actual.ex = e);

        Assert.Equal(expected, actual);
        Assert.Equal(success, actualS);
        Assert.Equal(fail, actualF);
    }

    [Fact] public void Bind()
    {
        var res = Result.Ok<Error>();
        var invoked = false;
        var a = res.Bind(() => { invoked = true; return Result.Ok<Error>(); });

        Assert.True(invoked);
    }
    [Fact] public void Bind_Fail()
    {
        var res = Result.Fail<Error>(new());
        var invoked = false;
        var a = res.Bind(() => { invoked = true; return Result.Ok<Error>(); });

        Assert.False(invoked);
        Assert.False(a.Success);
        Assert.Equal(res, a);
    }
    [Fact] public void Bind_OnFail()
    {
        var res = Result.Fail<Error>(new());
        var ex = new Error("fail");
        var a = res.Bind(Result.Ok<Error>, _ => ex);

        Assert.False(a.Success);
        Assert.Equal(ex, a.Error);
    }
    [Fact] public void Bind_ResultT()
    {
        var ex = new Error("fail");
        var s = Result.Ok<Error>();
        var f = Result.Fail<Error>(new());
        Result<int, Error> onFail(Error e) => Result.Fail<int, Error>(ex);
        Result<int, Error> onSuccess() => Result.Ok<int, Error>(1);

        var st = s.Bind(onSuccess, onFail);
        var ft = f.Bind(onSuccess, onFail);

        Assert.True(st.Success);
        Assert.False(ft.Success);
        Assert.Equal(1, st.Value);
        Assert.Equal(ex, ft.Error);
    }

    [Fact] public void Map_ResultT()
    {
        var ex = new Error("fail");
        var s = Result.Ok<Error>();
        var f = Result.Fail<Error>(new());
        int onFail(Error e) => 0;
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
        var res = Result.Ok<Error>();
        var a = res.Match(() => 1, _ => 0);

        Assert.Equal(1, a);
    }
    [Fact] public void Match_OnFail()
    {
        var res = Result.Fail<Error>(new());
        var a = res.Match(() => 1, _ => 0);

        Assert.Equal(0, a);
    }
}