using Common.Users;
using Xunit;

namespace Common.Tests.Users;

public class UserPatchTests
{
    [Fact]
    public void HasChanges_False_WhenNoValues()
    {
        var patch = new UserPatch();

        Assert.False(patch.HasChanges);
    }

    [Fact]
    public void HasChanges_True_WhenAnyValueProvided()
    {
        var patch = new UserPatch { FullName = "Name" };

        Assert.True(patch.HasChanges);
    }
}
