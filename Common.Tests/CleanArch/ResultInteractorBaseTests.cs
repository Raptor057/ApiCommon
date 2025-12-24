using System;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.CleanArch;
using Xunit;

namespace Common.Tests.CleanArch;

public class ResultInteractorBaseTests
{
    [Fact]
    public void OK_ReturnsSuccessResult()
    {
        var interactor = new TestInteractor();

        var result = interactor.CreateOk("data");

        var success = Assert.IsType<SuccessResult<string>>(result);
        Assert.Equal("data", success.Data);
    }

    [Fact]
    public void Fail_WithMessage_ReturnsFailureResult()
    {
        var interactor = new TestInteractor();

        var result = interactor.CreateFail("error");

        var failure = Assert.IsType<FailureResult<string>>(result);
        Assert.Equal("error", failure.Message);
    }

    [Fact]
    public void Fail_WithException_ReturnsFailureResult()
    {
        var interactor = new TestInteractor();
        var ex = new InvalidOperationException("bad");

        var result = interactor.CreateFail(ex);

        var failure = Assert.IsType<FailureResult<string>>(result);
        Assert.Same(ex, failure.Exception);
    }

    private sealed class TestInteractor : ResultInteractorBase<TestRequest, string>
    {
        public Result<string> CreateOk(string value) => OK(value);
        public Result<string> CreateFail(string message) => Fail(message);
        public Result<string> CreateFail(Exception ex) => Fail(ex);

        public override Task<Result<string>> Handle(TestRequest request, CancellationToken cancellationToken)
            => Task.FromResult(OK(request.Value));
    }

    private sealed record TestRequest(string Value) : IResultRequest<string>;
}
