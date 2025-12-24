using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Users;
using Xunit;

namespace Common.Tests.Users;

public class InMemoryUserRepositoryTests
{
    [Fact]
    public async Task InsertAsync_AddsUser()
    {
        var repo = new InMemoryUserRepository();
        var user = new User("user", "hash");

        var result = await repo.InsertAsync(user);

        var success = Assert.IsType<SuccessResult<User>>(result);
        Assert.Equal("user", success.Data.Username);
    }

    [Fact]
    public async Task InsertAsync_ReturnsFailure_OnDuplicate()
    {
        var repo = new InMemoryUserRepository();
        var user = new User("user", "hash");

        await repo.InsertAsync(user);
        var result = await repo.InsertAsync(user);

        var failure = Assert.IsType<FailureResult<User>>(result);
        Assert.Contains("already exists", failure.Message);
    }

    [Fact]
    public async Task PatchAsync_ReturnsFailure_WhenMissing()
    {
        var repo = new InMemoryUserRepository();

        var result = await repo.PatchAsync("missing", new UserPatch { FullName = "Name" });

        var failure = Assert.IsType<FailureResult<User>>(result);
        Assert.Contains("was not found", failure.Message);
    }

    [Fact]
    public async Task PatchAsync_ReturnsExisting_WhenNoChanges()
    {
        var repo = new InMemoryUserRepository();
        var user = new User("user", "hash");

        await repo.InsertAsync(user);
        var result = await repo.PatchAsync("user", new UserPatch());

        var success = Assert.IsType<SuccessResult<User>>(result);
        Assert.Same(user, success.Data);
    }

    [Fact]
    public async Task DisableAsync_DisablesUser()
    {
        var repo = new InMemoryUserRepository();
        var user = new User("user", "hash");

        await repo.InsertAsync(user);
        var result = await repo.DisableAsync("user");

        var success = Assert.IsType<SuccessResult<User>>(result);
        Assert.True(success.Data.Disabled);
    }

    [Fact]
    public async Task DisableAsync_ReturnsFailure_WhenMissing()
    {
        var repo = new InMemoryUserRepository();

        var result = await repo.DisableAsync("missing");

        var failure = Assert.IsType<FailureResult<User>>(result);
        Assert.Contains("was not found", failure.Message);
    }

    [Fact]
    public async Task GetAsync_ReturnsNull_WhenMissing()
    {
        var repo = new InMemoryUserRepository();

        var user = await repo.GetAsync("missing");

        Assert.Null(user);
    }

    [Fact]
    public async Task Operations_HonorCancellation()
    {
        var repo = new InMemoryUserRepository();
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        await Assert.ThrowsAsync<OperationCanceledException>(() => repo.GetAsync("user", cts.Token));
    }
}
