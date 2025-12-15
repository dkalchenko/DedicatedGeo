using System.Security.Claims;
using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Common.Extensions;
using DedicatedGeo.Mono.Core.Abstractions.Auth.Services;
using DedicatedGeo.Mono.Core.Abstractions.User.Services;
using DedicatedGeo.Mono.Dtos.Auth;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DedicatedGeo.Mono.Core.Auth.Handlers;

public class PostAuthLoginPublicRequestHandler: IRequestHandler<PostAuthLoginPublicRequest, PostAuthLoginPublicResponse>
{
    private readonly ILogger<PostAuthLoginPublicRequestHandler> _logger;
    private readonly ITokenService _tokenService;
    private readonly IUsersServices _usersService;

    public PostAuthLoginPublicRequestHandler(ITokenService tokenService,
        ILogger<PostAuthLoginPublicRequestHandler> logger, IUsersServices usersService)
    {
        _tokenService = tokenService.ThrowIfNull();
        _logger = logger.ThrowIfNull();
        _usersService = usersService.ThrowIfNull();
    }

    public async Task<PostAuthLoginPublicResponse> Handle(PostAuthLoginPublicRequest request,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        request.ThrowIfNull();

        var claims = new List<Claim>();
        
        var result = await _usersService.GetUserByEmailAsync(request.Email, cancellationToken);
        if (result is null || !request.Password.VerifyPasswordHash(result.Password))
            throw OwnConstants.ErrorTemplates.LoginFailedException.GetException();
        
        claims.Add(new Claim(OwnConstants.Claims.UserIdClaim, result.UserId.ToString()));
        claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, result.Role));

        var token = _tokenService.GenerateToken(claims, false);

        _logger.LogDebug("User has successful logged. User have following claims: {Claims}",
            claims.Aggregate("", (s, claim) => s + claim.ValueType + ": " + claim.Value));

        return new PostAuthLoginPublicResponse
        {
            Token = token.Token
        };
    }
}