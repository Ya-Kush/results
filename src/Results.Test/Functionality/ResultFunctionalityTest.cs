namespace Results.Functionality.Test;

public class ResultFunctionalityTest
{
    [Fact] public void Peek()
    {
        (int val, TestError ex) expected = (1, new("fail"));
        var success = Result<TestError>.Ok();
        var fail = Result<TestError>.Fail(expected.ex);

        (int val, TestError ex) actual = default;
        var actualS = success.Peek(() => actual.val = expected.val);
        var actualF = fail.Peek(onFail: e => actual.ex = e);

        Assert.Equal(expected, actual);
        Assert.Equal(success, actualS);
        Assert.Equal(fail, actualF);
    }

    [Fact] public void Bind()
    {
        var res = Result<TestError>.Ok();
        var invoked = false;
        var a = res.Bind(() => { invoked = true; return Result<TestError>.Ok(); });

        Assert.True(invoked);
    }
    [Fact] public void Bind_Fail()
    {
        var res = Result<TestError>.Fail(new());
        var invoked = false;
        var a = res.Bind(() => { invoked = true; return Result<TestError>.Ok(); });

        Assert.False(invoked);
        Assert.False(a.Success);
        Assert.Equal(res, a);
    }
    [Fact] public void Bind_OnFail()
    {
        var res = Result<TestError>.Fail(new());
        var ex = new TestError("fail");
        var a = res.Bind(Result<TestError>.Ok, _ => ex);

        Assert.False(a.Success);
        Assert.Equal(ex, a.Error);
    }
    [Fact] public void Bind_ResultT()
    {
        var ex = new TestError("fail");
        var s = Result<TestError>.Ok();
        var f = Result<TestError>.Fail(new());
        Result<int, TestError> onFail(TestError e) => Result<int, TestError>.Fail(ex);
        Result<int, TestError> onSuccess() => Result<int, TestError>.Ok(1);

        var st = s.Bind(onSuccess, onFail);
        var ft = f.Bind(onSuccess, onFail);

        Assert.True(st.Success);
        Assert.False(ft.Success);
        Assert.Equal(1, st.Value);
        Assert.Equal(ex, ft.Error);
    }

    [Fact] public void Map_ResultT()
    {
        var ex = new TestError("fail");
        var s = Result<TestError>.Ok();
        var f = Result<TestError>.Fail(new());
        int onFail(TestError e) => 0;
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
        var res = Result<TestError>.Ok();
        var a = res.Match(() => 1, _ => 0);

        Assert.Equal(1, a);
    }
    [Fact] public void Match_OnFail()
    {
        var res = Result<TestError>.Fail(new());
        var a = res.Match(() => 1, _ => 0);

        Assert.Equal(0, a);
    }
}