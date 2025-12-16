using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Dtos.User;
using HybridModelBinding;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DedicatedGeo.Mono.Api.Controllers.User;

[Route("/v1/admin/users")]
//[Authorize(Roles = $"{OwnConstants.Roles.SuperAdmin}")]
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
        var resp = await _sender.Send(request);
        return Ok(resp);
    }
    
    
    [HttpPut("{userId}")]
    public async Task<IActionResult> PutUser([FromHybrid] PutUserAdminRequest request)
    {
        var resp = await _sender.Send(request);
        return Ok(resp);
    }
    
    
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser([FromHybrid] DeleteUserAdminRequest request)
    {
        await _sender.Send(request);
        return Ok();
    }
    
    
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser([FromHybrid] GetUserAdminRequest request)
    {
        var resp = await _sender.Send(request);
        return Ok(resp);
    }
    
    
    [HttpGet]
    public async Task<IActionResult> GetUsers([FromHybrid] GetUsersAdminRequest request)
    {
        var resp = await _sender.Send(request);
        return Ok(resp);
    }
}