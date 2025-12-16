using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Common.Extensions;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.User;
using MediatR;

namespace DedicatedGeo.Mono.Core.User.Handlers.Admin;

public class PostUserAdminRequestHandler: IRequestHandler<PostUserAdminRequest, UserAdminResponse>
{
    private readonly IDatabaseRepository _databaseRepository;
    
    public PostUserAdminRequestHandler(IDatabaseRepository databaseRepository)
    {
        _databaseRepository = databaseRepository.ThrowIfNull();
    }    
    
    public async Task<UserAdminResponse> Handle(PostUserAdminRequest request, CancellationToken cancellationToken)
    {
        var user = new Models.User.User
        {
            UserId = Guid.NewGuid(),
            Email = request.Email,
            Role = request.Role,
            Password = request.Password.ToPasswordHash()
        };
        
        _databaseRepository.Users.Add(user);
        await _databaseRepository.SaveChangesAsync(cancellationToken);

        return new UserAdminResponse
        {
            Email = user.Email,
            Role = user.Role,
            UserId = user.UserId
        };
    }
}