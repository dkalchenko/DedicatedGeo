using MediatR;

namespace DedicatedGeo.Mono.Dtos.User;

public class PutUserAdminRequest: IRequest<UserAdminResponse>
{
    public string Email { get; set; }
    public string? Password { get; set; }
    public string Role { get; set; }
}