using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Common.Extensions;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.User.Handlers.Admin;

public class PutUserAdminRequestHandler: IRequestHandler<PutUserAdminRequest, UserAdminResponse>
{
    private readonly IDatabaseRepository _databaseRepository;
    
    public PutUserAdminRequestHandler(IDatabaseRepository databaseRepository)
    {
        _databaseRepository = databaseRepository.ThrowIfNull();
    }

    public async Task<UserAdminResponse> Handle(PutUserAdminRequest request, CancellationToken cancellationToken)
    {
        var user = await _databaseRepository.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken: cancellationToken);

        if (user is null)
        {
            throw OwnConstants.ErrorTemplates.ResourceNotFound.FormatMessage("user").GetException();
        }
        
        user.Email = request.Email;
        user.Role = request.Role;

        if (request.Password is not null)
        {
            user.Password = request.Password.ToPasswordHash();
        }
        
        await _databaseRepository.SaveChangesAsync(cancellationToken);

        return new UserAdminResponse
        {
            Email = user.Email,
            Role = user.Role,
            UserId = user.UserId
        };
    }
}