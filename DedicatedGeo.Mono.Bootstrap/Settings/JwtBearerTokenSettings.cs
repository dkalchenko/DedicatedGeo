using System.Text;
using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Core.Abstractions.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DedicatedGeo.Mono.Bootstrap.Settings;

public class JwtBearerTokenSettings : IJwtBearerTokenSettings
{
    private const string Position = "JwtBearerOptions";

    public JwtBearerTokenSettings(IConfiguration configuration)
    {
        configuration.GetSection(Position).Bind(this);
        SecretKey = Environment.GetEnvironmentVariable(OwnConstants.EnvironmentKeys.JwtSecretKey) ?? SecretKey!;
    }

    public string Issuer { get; set; }

    public string Audience { get; set; }

    public bool ValidateIssuerSigningKey { get; set; }

    public bool ValidateLifetime { get; set; }

    public string SecretKey { get; set; }

    public int LifeTime { get; set; }


    public TokenValidationParameters GenerateTokenValidationParameters()
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = ValidateIssuerSigningKey,
            ValidIssuer = Issuer,
            ValidAudience = Audience,
            IssuerSigningKey = GetSymmetricSecurityKey()
        };
    }

    private SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
    }
}