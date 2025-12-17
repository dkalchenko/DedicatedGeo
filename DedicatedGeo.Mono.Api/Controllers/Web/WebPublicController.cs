using DedicatedGeo.Mono.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DedicatedGeo.Mono.Api.Controllers.Web;

public class WebPublicController : Controller
{
    [HttpGet("/map")]
    [Authorize(Roles = $"{OwnConstants.Roles.SuperAdmin},{OwnConstants.Roles.Admin},{OwnConstants.Roles.DeviceUser}")]
    public IActionResult Map()
    {
        return View();
    }   
    
    [HttpGet("/")]
    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return Redirect("/map");
        }

        return View();
    }
}