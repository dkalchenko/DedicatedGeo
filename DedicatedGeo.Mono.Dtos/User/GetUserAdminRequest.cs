using MediatR;

namespace DedicatedGeo.Mono.Dtos.User;

public class GetUserAdminRequest: IRequest<UserAdminResponse>
{
    public Guid UserId { get; set; }
}