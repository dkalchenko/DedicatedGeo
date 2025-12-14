using Microsoft.IdentityModel.Tokens;

namespace DedicatedGeo.Mono.Core.Abstractions.Settings;

public interface IJwtBearerTokenSettings
{
    public int LifeTime { get; }
    TokenValidationParameters GenerateTokenValidationParameters();
}