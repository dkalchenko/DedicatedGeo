namespace DedicatedGeo.Mono.Entities.Auth;

public class JwtToken
{
    public string Token { get; set; }
    public DateTime ExpireAt { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpireAt { get; set; }
}