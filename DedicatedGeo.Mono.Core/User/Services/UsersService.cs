using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Core.Abstractions.User.Services;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Entities.User.Requests;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.User.Services;

public class UsersService: IUsersServices
{
    private IDatabaseRepository _databaseRepository;
    
    public UsersService(IDatabaseRepository databaseRepository)
    {
        _databaseRepository = databaseRepository.ThrowIfNull();
    }
    
    public async Task<UserEntity?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _databaseRepository.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken: cancellationToken);

        if (user is null)
        {
            return null;
        }
        
        return new UserEntity
        {
            Email =  user.Email,
            Password = user.Password,
            UserId = user.UserId,
            Role =  user.Role,
        };
    }
    
    public async Task<UserEntity?> GetUserById(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _databaseRepository.Users.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken: cancellationToken);

        if (user is null)
        {
            return null;
        }
        
        return new UserEntity
        {
            Email =  user.Email,
            Password = user.Password,
            UserId = user.UserId,
            Role =  user.Role,
        };
    }
}