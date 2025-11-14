namespace Results.Test;

public class ResultTFunctionalityTest
{
    [Fact] public void Peek()
    {
        (int val, Exception ex) expected = (10, new Exception("fail"));
        var success = Result.Ok<int, Exception>(expected.val);
        var fail = Result.Fail<int, Exception>(expected.ex);

        (int val, Exception ex) actual = default;
        var actualS = success.Peek(v => actual.val = v);
        var actualF = fail.Peek(onFail: e => actual.ex = e);

        Assert.Equal(expected, actual);
        Assert.Equal(success, actualS);
        Assert.Equal(fail, actualF);
    }

    [Fact] public void Bind()
    {
        var res = Result.Ok<int, Exception>(5);
        var a = res.Bind(i => Result.Ok<int, Exception>(i * 2));

        Assert.True(a.Success);
        Assert.Equal(10, a.Value);
    }
    [Fact] public void Bind_Fail()
    {
        var exception = new Exception("fail");
        var res = Result.Fail<int, Exception>(exception);
        var a = res.Bind(i => Result.Ok<int, Exception>(i * 2));

        Assert.False(a.Success);
        Assert.Equal(exception, a.Exception);
    }
    [Fact] public void Bind_OnFail()
    {
        var exception = new Exception("fail");
        var expected = 0;
        var res = Result.Fail<int, Exception>(exception);

        var a = res.Bind(i => Result.Ok<int, Exception>(i * 2), e => Result.Ok<int, Exception>(expected));

        Assert.True(a.Success);
        Assert.Equal(expected, a.Value);
    }
    [Fact] public void Bind_Result()
    {
        var success = Result.Ok<int, Exception>(5);
        var failure = Result.Fail<int, Exception>(new Exception("fail"));
        static Result<Exception> onSuccess(int _) => Result.Ok<Exception>();

        var s = success.Bind(onSuccess);
        var f = failure.Bind(onSuccess, e => Result.Fail(new Exception ("wrapper", failure.Exception)));

        Assert.True(s.Success);
        Assert.False(f.Success);
        Assert.Equal(failure.Exception, f.Exception.InnerException);
    }

    [Fact] public void Map()
    {
        var res = Result.Ok<int, Exception>(5);
        var a = res.Map(i => i * 2);

        Assert.True(a.Success);
        Assert.Equal(10, a.Value);
    }
    [Fact] public void Map_Fail()
    {
        var exception = new Exception("fail");
        var res = Result.Fail<int, Exception>(exception);
        var a = res.Map(i => i);

        Assert.False(a.Success);
        Assert.Equal(exception, a.Exception);
    }
    [Fact] public void Map_OnFail()
    {
        var exception = new Exception("fail");
        var expected = 0;
        var res = Result.Fail<int, Exception>(exception);
        var a = res.Map(i => i, e => expected);

        Assert.True(a.Success);
        Assert.Equal(expected, a.Value);
    }
    [Fact] public void Map_Result()
    {
        var success = Result.Ok<int, Exception>(5);
        var failure = Result.Fail<int, Exception>(new Exception("fail"));
        static void onSuccess(int _) { }
        Exception? ex = null;

        var s = success.Map(onSuccess);
        var f = failure.Map(onSuccess, e => ex = e);

        Assert.True(s.Success);
        Assert.True(f.Success);
        Assert.Equal(failure.Exception, ex);
    }

    [Fact] public void Match()
    {
        var res = Result.Ok<int, Exception>(5);
        var expected = 10;
        var a = res.Match(i => i * 2, e => 0);

        Assert.Equal(expected, a);
    }
    [Fact] public void Match_OnFail()
    {
        var exception = new Exception("fail");
        var res = Result.Fail<int, Exception>(exception);
        var expected = 0;
        var a = res.Match(i => i * 2, e => expected);

        Assert.Equal(expected, a);
    }

    [Fact] public void ValueOr()
    {
        var res = Result.Ok<int, Exception>(5);
        var a = res.ValueOr(10);
        var b = res.ValueOr(() => 10);
        var c = res.ValueOr(e => 10);

        Assert.Equal(5, a);
        Assert.Equal(5, b);
        Assert.Equal(5, c);
    }
    [Fact] public void ValueOr_Default()
    {
        var res = Result.Fail<int, Exception>(new Exception("fail"));
        var a = res.ValueOr(10);
        var b = res.ValueOr(() => 10);
        var c = res.ValueOr(e => 10);

        Assert.Equal(10, a);
        Assert.Equal(10, b);
        Assert.Equal(10, c);
    }

    [Fact] public void ToNullable()
    {
        var successClass = Result.Ok<string, Exception>(nameof(Result));
        var failureClass = Result.Fail<string, Exception>(new Exception("fail"));

        var sc = successClass.ToNullable();
        var fc = failureClass.ToNullable();

        Assert.Equal(nameof(Result), sc);
        Assert.Null(fc);
    }
    [Fact] public void ToNullable_Struct()
    {
        var success = Result.Ok<int, Exception>(10);
        var failure = Result.Fail<int, Exception>(new Exception("fail"));

        var s = success.ToNullable();
        var f = failure.ToNullable();

        Assert.NotNull(s);
        Assert.Null(f);
        Assert.Equal(10, s.Value);
    }

    [Fact] public void Deconstruct()
    {
        (string s, Exception fe) expected = ("success", new Exception("fail"));
        var sr = Result.Ok<string, Exception>(expected.s);
        var fr = Result.Fail<string, Exception>(expected.fe);

        var (s, se) = sr;
        var (f, fe) = fr;

        Assert.Equal(expected.s, s);
        Assert.Null(se);
        Assert.Null(f);
        Assert.Equal(expected.fe, fe);
    }
    [Fact] public void Deconstruct_Struct()
    {
        (int s, Exception fe) expected = (10, new Exception("fail"));
        var sr = Result.Ok<int, Exception>(expected.s);
        var fr = Result.Fail<int, Exception>(expected.fe);

        var (s, se) = sr;
        var (f, fe) = fr;

        Assert.Equal(expected.s, s);
        Assert.Null(se);
        Assert.Equal(default, f);
        Assert.Equal(expected.fe, fe);
    }
}