using Common;
using Xunit;

namespace Common.Tests.Errors;

public class ErrorListTests
{
    [Fact]
    public void IsEmpty_True_WhenNoErrors()
    {
        var errors = new ErrorList();

        Assert.True(errors.IsEmpty);
    }

    [Fact]
    public void ToString_FormatsItems()
    {
        var errors = new ErrorList { "First", "Second" };

        var text = errors.ToString();

        Assert.Equal("- First\n- Second", text);
    }

    [Fact]
    public void AsException_ReturnsBusinessRuleException()
    {
        var errors = new ErrorList { "Invalid" };

        var ex = errors.AsException();

        Assert.IsType<BusinessRuleException>(ex);
        Assert.Equal("- Invalid", ex.Message);
    }
}
