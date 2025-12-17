using DedicatedGeo.Mono.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DedicatedGeo.Mono.Api.Controllers.Web;

[Authorize]
[Route("admin")]
public class WebAdminController: Controller
{
    
    [HttpGet("users")]
    [Authorize(Roles = $"{OwnConstants.Roles.SuperAdmin}")]
    public IActionResult Users()
    {
        return View();
    }
    
    [HttpGet("devices")]
    [Authorize(Roles = $"{OwnConstants.Roles.SuperAdmin},{OwnConstants.Roles.Admin}")]
    public IActionResult Devices()
    {
        return View();
    }    
}