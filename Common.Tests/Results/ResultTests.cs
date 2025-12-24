using System;
using Common;
using Xunit;

namespace Common.Tests.Results;

public class ResultTests
{
    [Fact]
    public void Ok_ReturnsSuccessResultWithData()
    {
        var result = Result.OK("value");

        var success = Assert.IsType<SuccessResult<string>>(result);
        Assert.Equal("value", success.Data);
    }

    [Fact]
    public void Fail_WithMessage_ReturnsFailureResult()
    {
        var result = Result.Fail<string>("boom");

        var failure = Assert.IsType<FailureResult<string>>(result);
        Assert.Equal("boom", failure.Message);
    }

    [Fact]
    public void Fail_WithException_PreservesException()
    {
        var ex = new InvalidOperationException("invalid");

        var result = Result.Fail<string>(ex);

        var failure = Assert.IsType<FailureResult<string>>(result);
        Assert.Same(ex, failure.Exception);
    }

    [Fact]
    public void SuccessResult_ImplicitConversion_AssignsData()
    {
        SuccessResult<int> result = 10;

        Assert.Equal(10, result.Data);
    }
}
