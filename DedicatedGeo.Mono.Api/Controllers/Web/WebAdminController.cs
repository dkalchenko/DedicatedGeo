using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DedicatedGeo.Mono.Api.Controllers.Web;

[Authorize]
[Route("admin")]
public class WebAdminController: Controller
{
    [HttpGet("map")]
    public IActionResult Map()
    {
        return View();
    }    
}