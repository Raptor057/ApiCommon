namespace Common.Users;

/// <summary>
/// Data transfer object used to patch mutable user fields.
/// </summary>
public sealed class UserPatch
{
    /// <summary>
    /// New password hash. Leave <c>null</c> to keep the current one.
    /// </summary>
    public string? PasswordHash { get; init; }

    /// <summary>
    /// Updated display name. Leave <c>null</c> to keep the current one.
    /// </summary>
    public string? FullName { get; init; }

    /// <summary>
    /// Updated contact email. Leave <c>null</c> to keep the current one.
    /// </summary>
    public string? Email { get; init; }

    /// <summary>
    /// Indicates whether there is any change to apply.
    /// </summary>
    public bool HasChanges => PasswordHash is not null || FullName is not null || Email is not null;
}
