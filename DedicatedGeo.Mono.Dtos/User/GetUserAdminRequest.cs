using MediatR;

namespace DedicatedGeo.Mono.Dtos.User;

public class GetUserAdminRequest: IRequest<UserAdminResponse>
{
    public string UserId { get; set; }
}