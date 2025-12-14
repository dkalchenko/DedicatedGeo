using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Dtos.Auth;
using HybridModelBinding;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DedicatedGeo.Mono.Api.Controllers.Auth;

[Route("/v1/public/auth")]
public class AuthPublicController: Controller
{
    private readonly ISender _sender;
    public AuthPublicController(ISender sender)
    {
        _sender = sender.ThrowIfNull();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> PostAuth([FromHybrid] PostAuthLoginPublicRequest request)
    {
        return Ok(await _sender.Send(request));
    }
}