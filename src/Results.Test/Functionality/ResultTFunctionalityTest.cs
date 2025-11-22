namespace Results.Functionality.Test;

public class ResultTFunctionalityTest
{
    [Fact] public void Peek()
    {
        (int val, TestError ex) expected = (10, new TestError("fail"));
        var success = Result<int, TestError>.Ok(expected.val);
        var fail = Result<int, TestError>.Fail(expected.ex);

        (int val, TestError ex) actual = default;
        var actualS = success.Peek(v => actual.val = v);
        var actualF = fail.Peek(onFail: e => actual.ex = e);

        Assert.Equal(expected, actual);
        Assert.Equal(success, actualS);
        Assert.Equal(fail, actualF);
    }

    [Fact] public void Bind()
    {
        var res = Result<int, TestError>.Ok(5);
        var a = res.Bind(i => Result<int, TestError>.Ok(i * 2));

        Assert.True(a.Success);
        Assert.Equal(10, a.Value);
    }
    [Fact] public void Bind_Fail()
    {
        var exception = new TestError("fail");
        var res = Result<int, TestError>.Fail(exception);
        var a = res.Bind(i => Result<int, TestError>.Ok(i * 2));

        Assert.False(a.Success);
        Assert.Equal(exception, a.Error);
    }
    [Fact] public void Bind_OnFail()
    {
        var exception = new TestError("fail");
        var expected = 0;
        var res = Result<int, TestError>.Fail(exception);

        var a = res.Bind(i => Result<int, TestError>.Ok(i * 2), e => Result<int, TestError>.Ok(expected));

        Assert.True(a.Success);
        Assert.Equal(expected, a.Value);
    }
    [Fact] public void Bind_Result()
    {
        var success = Result<int, TestError>.Ok(5);
        var failure = Result<int, TestError>.Fail(new TestError("fail"));
        static Result<TestError> onSuccess(int _) => Result<TestError>.Ok();

        var expectedError = new TestError("wrapper");
        var s = success.Bind(onSuccess);
        var f = failure.Bind(onSuccess, e => expectedError);

        Assert.True(s.Success);
        Assert.False(f.Success);
        Assert.Equal(expectedError, f.Error);
    }

    [Fact] public void Map()
    {
        var res = Result<int, TestError>.Ok(5);
        var a = res.Map(i => i * 2);

        Assert.True(a.Success);
        Assert.Equal(10, a.Value);
    }
    [Fact] public void Map_Fail()
    {
        var exception = new TestError("fail");
        var res = Result<int, TestError>.Fail(exception);
        var a = res.Map(i => i);

        Assert.False(a.Success);
        Assert.Equal(exception, a.Error);
    }
    [Fact] public void Map_OnFail()
    {
        var exception = new TestError("fail");
        var expected = 0;
        var res = Result<int, TestError>.Fail(exception);
        var a = res.Map(i => i, e => expected);

        Assert.True(a.Success);
        Assert.Equal(expected, a.Value);
    }

    [Fact] public void Match()
    {
        var res = Result<int, TestError>.Ok(5);
        var expected = 10;
        var a = res.Match(i => i * 2, e => 0);

        Assert.Equal(expected, a);
    }
    [Fact] public void Match_OnFail()
    {
        var exception = new TestError("fail");
        var res = Result<int, TestError>.Fail(exception);
        var expected = 0;
        var a = res.Match(i => i * 2, e => expected);

        Assert.Equal(expected, a);
    }

    [Fact] public void ValueOr()
    {
        var res = Result<int, TestError>.Ok(5);
        var a = res.ValueOr(10);
        var b = res.ValueOr(_ => 10);

        Assert.Equal(5, a);
        Assert.Equal(5, b);
    }
    [Fact] public void ValueOr_Default()
    {
        var res = Result<int, TestError>.Fail(new TestError("fail"));
        var a = res.ValueOr(10);
        var c = res.ValueOr(_ => 10);

        Assert.Equal(10, a);
        Assert.Equal(10, c);
    }

    [Fact] public void ToNullable()
    {
        var successClass = Result<string, TestError>.Ok(nameof(Results));
        var failureClass = Result<string, TestError>.Fail(new TestError("fail"));

        var sc = successClass.ToNullable();
        var fc = failureClass.ToNullable();

        Assert.Equal(nameof(Results), (string?)sc);
        Assert.Null(fc);
    }
    [Fact] public void ToNullable_Struct()
    {
        var success = Result<int, TestError>.Ok(10);
        var failure = Result<int, TestError>.Fail(new TestError("fail"));

        var s = success.ToNullable();
        var f = failure.ToNullable();

        Assert.NotNull(s);
        Assert.Null(f);
        Assert.Equal(10, s.Value);
    }

    [Fact] public void Deconstruct()
    {
        (string s, TestError fe) expected = ("success", new TestError("fail"));
        var sr = Result<string, TestError>.Ok(expected.s);
        var fr = Result<string, TestError>.Fail(expected.fe);

        var (s, se) = sr;
        var (f, fe) = fr;

        Assert.Equal(expected.s, s);
        Assert.Null(se);
        Assert.Null(f);
        Assert.Equal(expected.fe, fe);
    }
    [Fact] public void Deconstruct_Struct()
    {
        (int s, TestError fe) expected = (10, new TestError("fail"));
        var sr = Result<int, TestError>.Ok(expected.s);
        var fr = Result<int, TestError>.Fail(expected.fe);

        var (s, se) = sr;
        var (f, fe) = fr;

        Assert.Equal(expected.s, s);
        Assert.Null(se);
        Assert.Equal(default, f);
        Assert.Equal(expected.fe, fe);
    }
}