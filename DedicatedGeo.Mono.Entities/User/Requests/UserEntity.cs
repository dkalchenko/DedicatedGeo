namespace DedicatedGeo.Mono.Entities.User.Requests;

public class UserEntity
{
    public string Password { get; set; }
    public string Email { get; set; }
    public Guid UserId { get; set; }
    public string Role { get; set; }
}