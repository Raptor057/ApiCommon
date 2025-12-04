using System;

namespace Common.Users;

/// <summary>
/// Represents a system user.
/// </summary>
/// <param name="Username">Unique username used to identify the user.</param>
/// <param name="PasswordHash">Password hash used for authentication.</param>
public record User(string Username, string PasswordHash)
{
    /// <summary>
    /// Display name for the user.
    /// </summary>
    public string? FullName { get; init; }

    /// <summary>
    /// Contact email for the user.
    /// </summary>
    public string? Email { get; init; }

    /// <summary>
    /// Indicates whether the user is disabled.
    /// </summary>
    public bool Disabled { get; init; }

    /// <summary>
    /// Date when the user was disabled.
    /// </summary>
    public DateTimeOffset? DisabledAt { get; init; }

    /// <summary>
    /// Applies a patch to update mutable user fields.
    /// </summary>
    /// <param name="patch">Patch data with allowed changes.</param>
    /// <returns>Updated user instance.</returns>
    public User ApplyPatch(UserPatch patch)
    {
        ArgumentNullException.ThrowIfNull(patch);

        return this with
        {
            PasswordHash = patch.PasswordHash ?? PasswordHash,
            FullName = patch.FullName ?? FullName,
            Email = patch.Email ?? Email
        };
    }

    /// <summary>
    /// Marks the user as disabled.
    /// </summary>
    /// <param name="disabledAt">Optional timestamp, defaults to <see cref="DateTimeOffset.UtcNow"/>.</param>
    /// <returns>Updated user instance.</returns>
    public User Disable(DateTimeOffset? disabledAt = null)
    {
        if (Disabled)
        {
            return this;
        }

        return this with
        {
            Disabled = true,
            DisabledAt = disabledAt ?? DateTimeOffset.UtcNow
        };
    }
}
