using System;
using Common;
using Xunit;

namespace Common.Tests.Errors;

public class BusinessRuleExceptionTests
{
    [Fact]
    public void Constructor_WithMessage_SetsMessage()
    {
        var ex = new BusinessRuleException("rule failed");

        Assert.Equal("rule failed", ex.Message);
    }

    [Fact]
    public void Constructor_WithInnerException_SetsInnerException()
    {
        var inner = new InvalidOperationException("inner");
        var ex = new BusinessRuleException("outer", inner);

        Assert.Equal("outer", ex.Message);
        Assert.Same(inner, ex.InnerException);
    }
}
