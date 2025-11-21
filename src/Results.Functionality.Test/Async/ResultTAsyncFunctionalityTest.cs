namespace Results.Functionality.Test;

public class ResultTAsyncFunctionalityTest
{
    [Fact] public async Task PeekAsync()
    {
        (int val, TestError err) expected = (1, new("fail"));
        var s = Result.Ok<int, TestError>(expected.val);
        var f = Result.Fail(expected.err);

        (int val, TestError err) actual = default;
        var actualS = await s.Async().PeekAsync(async i => actual.val = i);
        var actualF = await f.Async().PeekAsync(onFail: async e => actual.err = e);

        Assert.Equal(expected, actual);
        Assert.Equal(s, actualS);
        Assert.Equal(f, actualF);
    }

    [Fact] public async Task BindAsync()
    {
        var exp = Result.Ok<int, TestError>(1);
        var res = await Result
            .Ok<int, TestError>(0).Async()
            .BindAsync(async _ => exp);

        Assert.True(res.Success);
        Assert.Equal(exp, res);
    }
    [Fact] public async Task BindAsync_Fail()
    {
        var exp = Result.Ok<int, TestError>(1);
        var res = await Result
            .Fail<int, TestError>(new()).Async()
            .BindAsync(async _ => exp);

        Assert.False(res.Success);
        Assert.NotEqual(exp, res);
    }
    [Fact] public async Task BindAsync_OnFail()
    {
        var exp = Result.Ok<int, TestError>(69);
        var res = await Result
            .Fail<int, TestError>(new("i'm fine")).Async()
            .BindAsync(
                async _ => 1,
                async _ => exp);

        Assert.True(res.Success);
        Assert.Equal(exp, res);
    }
    [Fact] public async Task BindAsync_Result()
    {
        var succ = Result.Ok<TestError>();
        var fail = Result.Fail<TestError>(new("actually i'm fine"));
        var st = Result.Ok<int, TestError>(0);
        var ft = Result.Fail<int, TestError>(new());
        async ValueTask<Result<TestError>> onSuccess(int _) => succ;
        async ValueTask<Result<TestError>> onFail(TestError _) => fail;

        var s = await st.Async().BindAsync(onSuccess, onFail);
        var f = await ft.Async().BindAsync(onSuccess, onFail);

        Assert.True(s.Success);
        Assert.False(f.Success);
        Assert.Equal(succ, s);
        Assert.Equal(fail, f);
    }

    [Fact] public async Task MapAsync_ResultT()
    {
        var st = Result.Ok<int, TestError>(0);
        var ft = Result.Fail<int, TestError>(new());
        async ValueTask<int> onSuccess(int _) => 1;
        async ValueTask<int> onFail(TestError _) => 0;

        var s = await st.Async().MapAsync(onSuccess, onFail);
        var f = await ft.Async().MapAsync(onSuccess, onFail);

        Assert.True(s.Success);
        Assert.True(f.Success);
        Assert.Equal(1, s.Value);
        Assert.Equal(0, f.Value);
    }

    [Fact] public async Task MatchAsync_Full()
    {
        async ValueTask<int> onSuccess(int i) => 1;
        async ValueTask<int> onFail(TestError e) => 0;

        var s = await Result.Ok<int, TestError>(7777777).Async().MatchAsync(onSuccess, onFail);
        var f = await Result.Fail<int, TestError>(new()).Async().MatchAsync(onSuccess, onFail);

        Assert.Equal(1, s);
        Assert.Equal(0, f);
    }
}