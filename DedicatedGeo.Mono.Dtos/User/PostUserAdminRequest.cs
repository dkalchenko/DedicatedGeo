using MediatR;

namespace DedicatedGeo.Mono.Dtos.User;

public class PostUserAdminRequest: IRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}