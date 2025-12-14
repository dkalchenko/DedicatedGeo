using Microsoft.AspNetCore.Mvc;

namespace DedicatedGeo.Mono.Api.Controllers.Web;

public class WebPublicController : Controller
{
    [HttpGet("/")]
    public IActionResult Login()
    {
        // If the user is authenticated/authorized, redirect them to the admin map.
        if (User.Identity?.IsAuthenticated == true)
        {
            return Redirect("/admin/map");
        }

        return View();
    }
}