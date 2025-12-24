using System;
using Common;
using Common.CleanArch;
using Xunit;

namespace Common.Tests.CleanArch;

public class ResultViewModelTests
{
    [Fact]
    public void Constructor_SetsDefaults()
    {
        var vm = new ResultViewModel<string>();

        Assert.Null(vm.Data);
        Assert.False(vm.IsSuccess);
        Assert.Equal(string.Empty, vm.Message);
    }

    [Fact]
    public void Set_Failure_UpdatesState()
    {
        var vm = new ResultViewModel<string>();
        var failure = new FailureResult<string>("error");

        vm.Set(failure);

        Assert.False(vm.IsSuccess);
        Assert.Equal("error", vm.Message);
        Assert.Null(vm.Data);
    }

    [Fact]
    public void Set_Success_UpdatesState()
    {
        var vm = new ResultViewModel<string>();
        var success = new SuccessResult<string>("data");

        vm.Set(success, value => value.ToUpperInvariant());

        Assert.True(vm.IsSuccess);
        Assert.Equal("DATA", vm.Data);
        Assert.Null(vm.Message);
    }

    [Fact]
    public void Fail_ReturnsInstanceWithMessage()
    {
        var vm = new ResultViewModel<string>();

        var returned = vm.Fail("failed");

        Assert.Same(vm, returned);
        Assert.False(vm.IsSuccess);
        Assert.Equal("failed", vm.Message);
        Assert.Null(vm.Data);
    }
}
