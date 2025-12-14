using MediatR;

namespace DedicatedGeo.Mono.Dtos.User;

public class GetUsersAdminRequest: IRequest<GetUsersAdminResponse>
{
    public int Offset { get; set; }
    public int Limit { get; set; }
}