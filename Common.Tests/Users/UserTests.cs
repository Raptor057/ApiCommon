using System;
using Common.Users;
using Xunit;

namespace Common.Tests.Users;

public class UserTests
{
    [Fact]
    public void ApplyPatch_UpdatesFields()
    {
        var user = new User("user", "hash") { FullName = "Old", Email = "old@example.com" };
        var patch = new UserPatch { FullName = "New" };

        var updated = user.ApplyPatch(patch);

        Assert.Equal("New", updated.FullName);
        Assert.Equal("old@example.com", updated.Email);
        Assert.Equal("hash", updated.PasswordHash);
    }

    [Fact]
    public void Disable_SetsDisabledFields()
    {
        var user = new User("user", "hash");

        var disabled = user.Disable();

        Assert.True(disabled.Disabled);
        Assert.NotNull(disabled.DisabledAt);
    }

    [Fact]
    public void Disable_WhenAlreadyDisabled_ReturnsSameInstance()
    {
        var disabledAt = DateTimeOffset.UtcNow.AddDays(-1);
        var user = new User("user", "hash") { Disabled = true, DisabledAt = disabledAt };

        var disabledAgain = user.Disable();

        Assert.Same(user, disabledAgain);
        Assert.Equal(disabledAt, disabledAgain.DisabledAt);
    }
}
