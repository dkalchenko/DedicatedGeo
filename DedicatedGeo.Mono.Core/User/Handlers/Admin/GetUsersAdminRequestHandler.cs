using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.User.Handlers.Admin;

public class GetUsersAdminRequestHandler: IRequestHandler<GetUsersAdminRequest, GetUsersAdminResponse>
{

    private readonly IDatabaseRepository _databaseRepository;
    
    public GetUsersAdminRequestHandler(IDatabaseRepository databaseRepository)
    {
        _databaseRepository = databaseRepository.ThrowIfNull();
    }
    
    public async Task<GetUsersAdminResponse> Handle(GetUsersAdminRequest request, CancellationToken cancellationToken)
    {
        var query = _databaseRepository.Users
            .AsQueryable();

        if (request.UserIds is not null)
        {
            var userIds = request.UserIds.Split(',').Select(Guid.Parse).ToList();
            query = query.Where(x => userIds.Contains(x.UserId));
        }
        else
        {
            if (request.Search is not null)
            {
                query = query.Where(x => x.Email.Contains(request.Search));
            }
            
            query = query.Skip(request.Offset).Take(request.Limit);
        }
        
        var users = await query.ToListAsync(cancellationToken: cancellationToken);

        return new GetUsersAdminResponse
        {
            Users = users.Select(x => new UserAdminResponse
            {
                Email = x.Email,
                UserId = x.UserId,
                Role = x.Role
            })
        };
    }
}