using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Dtos.User;
using HybridModelBinding;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DedicatedGeo.Mono.Api.Controllers.User;

[Route("/v1/admin/user")]
public class UserAdminController: Controller
{
    private readonly ISender _sender;
    public UserAdminController(ISender sender)
    {
        _sender = sender.ThrowIfNull();
    }
    
    [HttpPost]
    public async Task<IActionResult> PostUser([FromHybrid] PostUserAdminRequest request)
    {
        await _sender.Send(request);
        return Ok();
    }
}