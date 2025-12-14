using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Common.Extensions;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.User.Handlers.Admin;

public class DeleteUserAdminRequestHandler: IRequestHandler<DeleteUserAdminRequest>
{

    private readonly IDatabaseRepository _databaseRepository;
    
    public DeleteUserAdminRequestHandler(IDatabaseRepository databaseRepository)
    {
        _databaseRepository = databaseRepository.ThrowIfNull();
    }
    
    public async Task Handle(DeleteUserAdminRequest request, CancellationToken cancellationToken)
    {
        var user = await _databaseRepository.Users.FirstOrDefaultAsync(x => x.UserId == request.UserId.ToGuid(), cancellationToken: cancellationToken);

        if (user is null)
        {
            return;
        }
        
        _databaseRepository.Users.Remove(user);
        await _databaseRepository.SaveChangesAsync(cancellationToken);
    }
}