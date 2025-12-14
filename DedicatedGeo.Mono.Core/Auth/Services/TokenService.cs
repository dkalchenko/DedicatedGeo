using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Core.Abstractions.Auth;
using DedicatedGeo.Mono.Core.Abstractions.Auth.Services;
using DedicatedGeo.Mono.Core.Abstractions.Settings;
using DedicatedGeo.Mono.Entities.Auth;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace DedicatedGeo.Mono.Core.Auth.Services;

public class TokenService : ITokenService
{
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
    private readonly ILogger<TokenService> _logger;
    private readonly IJwtBearerTokenSettings _settings;
    private readonly TokenValidationParameters _tokenValidationParameters;

    public TokenService(IJwtBearerTokenSettings settings, ILogger<TokenService> logger)
    {
        _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        _settings = settings.ThrowIfNull();
        _logger = logger.ThrowIfNull();
        _tokenValidationParameters = settings.GenerateTokenValidationParameters();
    }

    public JwtToken GenerateToken(IEnumerable<Claim> claims, bool includeRefreshToken)
    {
        var now = DateTime.UtcNow;
        var tokenTime = now.Add(TimeSpan.FromMinutes(_settings.LifeTime));
        var refreshTokenTime = now.Add(TimeSpan.FromMinutes(_settings.LifeTime * 2));

        var token = new JwtSecurityToken(
            _tokenValidationParameters.ValidIssuer,
            _tokenValidationParameters.ValidAudience,
            notBefore: now,
            claims: claims,
            expires: tokenTime,
            signingCredentials: new SigningCredentials(_tokenValidationParameters.IssuerSigningKey,
                SecurityAlgorithms.HmacSha256)
        );
        if (!includeRefreshToken)
            return new JwtToken
            {
                Token = _jwtSecurityTokenHandler.WriteToken(token),
                ExpireAt = tokenTime
            };
        var refreshClaims = claims.Append(new Claim(OwnConstants.Claims.IsRefreshClaim, true.ToString()));
        var refreshToken = new JwtSecurityToken(
            _tokenValidationParameters.ValidIssuer,
            _tokenValidationParameters.ValidAudience,
            notBefore: now,
            claims: refreshClaims,
            expires: refreshTokenTime,
            signingCredentials: new SigningCredentials(_tokenValidationParameters.IssuerSigningKey,
                SecurityAlgorithms.HmacSha256)
        );

        return new JwtToken
        {
            Token = _jwtSecurityTokenHandler.WriteToken(token),
            ExpireAt = tokenTime,
            RefreshToken = _jwtSecurityTokenHandler.WriteToken(refreshToken),
            RefreshTokenExpireAt = refreshTokenTime
        };
    }

    public async Task<IDictionary<string, string>> ReadTokenAsync(string token, IEnumerable<string> requiredTypes,
        CancellationToken cancellationToken)
    {
        var result = await _jwtSecurityTokenHandler.ValidateTokenAsync(token, _tokenValidationParameters);

        if (!result.IsValid)
            throw OwnConstants.ErrorTemplates
                .TokenIsNotValid
                .FormatMessage(result.Exception.Message)
                .WithInnerException(result.Exception)
                .GetException();

        foreach (var type in requiredTypes)
            if (!result.Claims.ContainsKey(type) &&
                !(result.Claims.TryGetValue(type, out var value) && value.ToString() is not null))
                throw OwnConstants.ErrorTemplates
                    .TokenIsNotValid
                    .FormatMessage($"Token doesnt have {type} or its value null")
                    .GetException();

        return result.Claims.ToDictionary(x => x.Key, x => x.Value.ToString()!);
    }
}