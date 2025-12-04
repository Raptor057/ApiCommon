using System;
using System.Threading.Tasks;
using Common;
using Common.Users;
using Xunit;

namespace Common.Tests.Users;

public class InMemoryUserRepositoryTests
{
    private readonly InMemoryUserRepository _repository = new();

    [Fact]
    public async Task InsertAsync_ShouldRejectDuplicateUsernames()
    {
        var user = new User("alice", "hash1");

        var firstInsert = await _repository.InsertAsync(user);
        var duplicateInsert = await _repository.InsertAsync(user);

        var failure = Assert.IsType<FailureResult<User>>(duplicateInsert);
        Assert.IsType<SuccessResult<User>>(firstInsert);
        Assert.Equal("User 'alice' already exists.", failure.Message);
    }

    [Fact]
    public async Task PatchAsync_ShouldUpdateMutableFieldsOnly()
    {
        var user = new User("bob", "hash1")
        {
            FullName = "Old Name",
            Email = "old@example.com"
        };

        await _repository.InsertAsync(user);

        var patch = new UserPatch
        {
            PasswordHash = "hash2",
            FullName = "New Name",
            Email = "new@example.com"
        };

        var result = await _repository.PatchAsync("bob", patch);

        var updated = Assert.IsType<SuccessResult<User>>(result).Data;
        Assert.Equal("bob", updated.Username);
        Assert.Equal("hash2", updated.PasswordHash);
        Assert.Equal("New Name", updated.FullName);
        Assert.Equal("new@example.com", updated.Email);
    }

    [Fact]
    public async Task DisableAsync_ShouldFlagUserAsDisabled()
    {
        var user = new User("carol", "hash1");
        await _repository.InsertAsync(user);

        var result = await _repository.DisableAsync("carol");

        var disabled = Assert.IsType<SuccessResult<User>>(result).Data;
        Assert.True(disabled.Disabled);
        Assert.NotNull(disabled.DisabledAt);
    }

    [Fact]
    public async Task PatchAsync_ShouldReturnFailureWhenUserMissing()
    {
        var patch = new UserPatch { Email = "missing@example.com" };

        var result = await _repository.PatchAsync("missing", patch);

        var failure = Assert.IsType<FailureResult<User>>(result);
        Assert.Equal("User 'missing' was not found.", failure.Message);
    }
}
