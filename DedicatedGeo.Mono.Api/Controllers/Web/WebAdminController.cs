using DedicatedGeo.Mono.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DedicatedGeo.Mono.Api.Controllers.Web;

[Authorize]
[Route("admin")]
public class WebAdminController: Controller
{
    [HttpGet("map")]
    [Authorize(Roles = $"{OwnConstants.Roles.SuperAdmin},{OwnConstants.Roles.Admin},{OwnConstants.Roles.DeviceUser}")]
    public IActionResult Map()
    {
        return View();
    }    
}