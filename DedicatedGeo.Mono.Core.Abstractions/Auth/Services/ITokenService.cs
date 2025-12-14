using System.Security.Claims;
using DedicatedGeo.Mono.Entities.Auth;

namespace DedicatedGeo.Mono.Core.Abstractions.Auth.Services;

public interface ITokenService
{
    public JwtToken GenerateToken(IEnumerable<Claim> claims, bool includeRefreshToken);

    public Task<IDictionary<string, string>> ReadTokenAsync(string token, IEnumerable<string> requiredClaims,
        CancellationToken cancellationToken);
}