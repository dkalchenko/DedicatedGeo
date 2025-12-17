using MediatR;

namespace DedicatedGeo.Mono.Dtos.User;

public class GetUsersAdminRequest: IRequest<GetUsersAdminResponse>
{
    public string? UserIds { get; set; }
    public string? Role { get; set; }
    public string? Search { get; set; }
    public int Offset { get; set; }
    public int Limit { get; set; }
}