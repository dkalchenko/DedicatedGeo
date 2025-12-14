using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.User;
using MediatR;

namespace DedicatedGeo.Mono.Core.User.Handlers.Admin;

public class PostUserAdminRequestHandler: IRequestHandler<PostUserAdminRequest>
{
    private readonly IDatabaseRepository _databaseRepository;
    
    public PostUserAdminRequestHandler(IDatabaseRepository databaseRepository)
    {
        _databaseRepository = databaseRepository.ThrowIfNull();
    }    
    
    public async Task Handle(PostUserAdminRequest request, CancellationToken cancellationToken)
    {
        var user = new Models.User.User
        {
            Email = request.Email,
            Password = request.Password
        };
        
        _databaseRepository.Users.Add(user);
        await _databaseRepository.SaveChangesAsync(cancellationToken);
    }
}