using MediatR;

namespace DedicatedGeo.Mono.Dtos.Auth;

public class PostAuthLoginPublicRequest: IRequest<PostAuthLoginPublicResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}