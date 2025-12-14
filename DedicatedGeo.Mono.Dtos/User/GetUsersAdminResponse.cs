namespace DedicatedGeo.Mono.Dtos.User;

public class GetUsersAdminResponse
{
    public IEnumerable<UserAdminResponse> Users { get; set; }
}