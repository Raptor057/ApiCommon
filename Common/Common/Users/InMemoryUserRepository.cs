using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Common.Users;

/// <summary>
/// Simple in-memory repository useful for testing or lightweight scenarios.
/// </summary>
public sealed class InMemoryUserRepository : IUserRepository
{
    private readonly ConcurrentDictionary<string, User> _users;

    /// <summary>
    /// Initializes a new instance of the <see cref="InMemoryUserRepository"/> class.
    /// </summary>
    /// <param name="comparer">Optional comparer used to normalize usernames.</param>
    public InMemoryUserRepository(IEqualityComparer<string>? comparer = null)
    {
        _users = new ConcurrentDictionary<string, User>(comparer ?? StringComparer.OrdinalIgnoreCase);
    }

    /// <inheritdoc />
    public Task<Result<User>> InsertAsync(User user, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(user);

        cancellationToken.ThrowIfCancellationRequested();

        if (!_users.TryAdd(user.Username, user))
        {
            return Task.FromResult(Result.Fail<User>($"User '{user.Username}' already exists."));
        }

        return Task.FromResult(Result.OK(user));
    }

    /// <inheritdoc />
    public Task<Result<User>> PatchAsync(string username, UserPatch patch, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(username);
        ArgumentNullException.ThrowIfNull(patch);

        cancellationToken.ThrowIfCancellationRequested();

        if (!_users.TryGetValue(username, out var user))
        {
            return Task.FromResult(Result.Fail<User>($"User '{username}' was not found."));
        }

        if (!patch.HasChanges)
        {
            return Task.FromResult(Result.OK(user));
        }

        var updated = user.ApplyPatch(patch);
        _users[updated.Username] = updated;

        return Task.FromResult(Result.OK(updated));
    }

    /// <inheritdoc />
    public Task<Result<User>> DisableAsync(string username, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(username);

        cancellationToken.ThrowIfCancellationRequested();

        if (!_users.TryGetValue(username, out var user))
        {
            return Task.FromResult(Result.Fail<User>($"User '{username}' was not found."));
        }

        var disabledUser = user.Disable();
        _users[username] = disabledUser;

        return Task.FromResult(Result.OK(disabledUser));
    }

    /// <inheritdoc />
    public Task<User?> GetAsync(string username, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(username);

        cancellationToken.ThrowIfCancellationRequested();

        _users.TryGetValue(username, out var user);
        return Task.FromResult(user);
    }
}
