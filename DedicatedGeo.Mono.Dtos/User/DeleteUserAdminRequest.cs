using MediatR;

namespace DedicatedGeo.Mono.Dtos.User;

public class DeleteUserAdminRequest: IRequest
{
    public string UserId { get; set; }
}