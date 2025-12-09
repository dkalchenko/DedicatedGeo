namespace DedicatedGeo.Mono.Models;

public class User
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = null!;
    public ulong NationalNumber { get; set; }
    public int CountryCode { get; set; }
    public string Password { get; set; } = null!;
    public string Login { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string SecondName { get; set; } = null!;
    public DateTime? EmailVerifiedAt { get; set; }
    public DateTime? PhoneVerifiedAt { get; set; }
}