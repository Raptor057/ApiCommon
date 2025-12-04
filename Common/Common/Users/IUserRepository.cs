using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Common.Users;

/// <summary>
/// Contract for storing and mutating user records.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Inserts a new user if the username does not already exist.
    /// </summary>
    /// <param name="user">User to persist.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Result containing the persisted user or an error.</returns>
    Task<Result<User>> InsertAsync(User user, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates mutable fields for a user.
    /// </summary>
    /// <param name="username">User identifier to update.</param>
    /// <param name="patch">Patch data containing allowed changes.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Result containing the updated user or an error.</returns>
    Task<Result<User>> PatchAsync(string username, UserPatch patch, CancellationToken cancellationToken = default);

    /// <summary>
    /// Disables a user so they can no longer be used.
    /// </summary>
    /// <param name="username">User identifier to disable.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Result containing the disabled user or an error.</returns>
    Task<Result<User>> DisableAsync(string username, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a user by username.
    /// </summary>
    /// <param name="username">Username to find.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>User if found; otherwise, <c>null</c>.</returns>
    Task<User?> GetAsync(string username, CancellationToken cancellationToken = default);
}
