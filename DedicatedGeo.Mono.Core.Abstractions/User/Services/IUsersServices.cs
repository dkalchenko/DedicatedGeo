using DedicatedGeo.Mono.Entities.User.Requests;

namespace DedicatedGeo.Mono.Core.Abstractions.User.Services;

public interface IUsersServices
{
    public Task<UserEntity?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    public Task<UserEntity?> GetUserById(Guid userId, CancellationToken cancellationToken);
}