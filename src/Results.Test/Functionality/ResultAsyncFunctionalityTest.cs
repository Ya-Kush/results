namespace Results.Functionality.Test;

public class ResultAsyncFunctionalityTest
{
    [Fact] public async Task PeekAsync()
    {
        (int val, TestError err) expected = (1, new("fail"));
        var s = Result<TestError>.Ok();
        var f = Result<TestError>.Fail(expected.err);

        (int val, TestError err) actual = default;
        var actualS = await s.Async().PeekAsync(async () => actual.val = expected.val);
        var actualF = await f.Async().PeekAsync(onFail: async e => actual.err = e);

        Assert.Equal(expected, actual);
        Assert.Equal(s, actualS);
        Assert.Equal(f, actualF);
    }

    [Fact] public async Task BindAsync()
    {
        var err = new TestError("actually i'm fine");
        var res = await Result<TestError>
            .Ok().Async()
            .BindAsync(async () => err);

        Assert.False(res.Success);
        Assert.Equal(err, res.Error);
    }
    [Fact] public async Task BindAsync_Fail()
    {
        var err = new TestError("actually i'm fine");
        var res = await Result<TestError>
            .Fail(new("fail")).Async()
            .BindAsync(async () => err);

        Assert.False(res.Success);
        Assert.NotEqual(err, res.Error);
    }
    [Fact] public async Task BindAsync_OnFail()
    {
        var err = new TestError("new");
        var res = await Result<TestError>
            .Fail(new("old")).Async()
            .BindAsync(async () => Result<TestError>.Ok(), async _ => err);

        Assert.False(res.Success);
        Assert.Equal(err, res.Error);
    }
    [Fact] public async Task BindAsync_ResultT()
    {
        var err = new TestError("fail");
        var s = Result<TestError>.Ok();
        var f = Result<TestError>.Fail(new());
        async ValueTask<Result<int, TestError>> onSuccess() => Result<int, TestError>.Ok(1);
        async ValueTask<Result<int, TestError>> onFail(TestError e) => Result<int, TestError>.Fail(err);

        var st = await s.Async().BindAsync(onSuccess, onFail);
        var ft = await f.Async().BindAsync(onSuccess, onFail);

        Assert.True(st.Success);
        Assert.False(ft.Success);
        Assert.Equal(1, st.Value);
        Assert.Equal(err, ft.Error);
    }

    [Fact] public async Task MapAsync_ResultT()
    {
        var err = new TestError("fail");
        var s = Result<TestError>.Ok();
        var f = Result<TestError>.Fail(new());
        async ValueTask<int> onSuccess() => 1;
        async ValueTask<int> onFail(TestError e) => 0;

        var st = await s.Async().MapAsync(onSuccess, onFail);
        var ft = await f.Async().MapAsync(onSuccess, onFail);

        Assert.True(st.Success);
        Assert.True(ft.Success);
        Assert.Equal(1, st.Value);
        Assert.Equal(0, ft.Value);
    }

    [Fact] public async Task MatchAsync_Full()
    {
        async ValueTask<int> onSuccess() => 1;
        async ValueTask<int> onFail(TestError e) => 0;

        var s = await Result<TestError>.Ok().Async().MatchAsync(onSuccess, onFail);
        var f = await Result<TestError>.Fail(new()).Async().MatchAsync(onSuccess, onFail);

        Assert.Equal(1, s);
        Assert.Equal(0, f);
    }
}