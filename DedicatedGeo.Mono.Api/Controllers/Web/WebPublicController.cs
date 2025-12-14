using Microsoft.AspNetCore.Mvc;

namespace DedicatedGeo.Mono.Api.Controllers.Web;

public class WebPublicController: Controller
{
    [HttpGet("/")]
    public IActionResult Login()
    {
        return View();
    }   
}