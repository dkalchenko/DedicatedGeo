using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Common.Extensions;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.User.Handlers.Admin;

public class GetUserAdminRequestHandler: IRequestHandler<GetUserAdminRequest, UserAdminResponse>
{
    
    private readonly IDatabaseRepository _databaseRepository;
    
    public GetUserAdminRequestHandler(IDatabaseRepository databaseRepository)
    {
        _databaseRepository = databaseRepository.ThrowIfNull();
    }
    
    public async Task<UserAdminResponse> Handle(GetUserAdminRequest request, CancellationToken cancellationToken)
    {
        var user = await _databaseRepository.Users.FirstOrDefaultAsync(x => x.UserId == request.UserId.ToGuid(), cancellationToken: cancellationToken);

        if (user is null)
        {
            throw OwnConstants.ErrorTemplates.ResourceNotFound.FormatMessage("user").GetException();
        }

        return new UserAdminResponse
        {
            UserId = user.UserId,
            Email = user.Email,
            Role = user.Role,
        };
    }
}