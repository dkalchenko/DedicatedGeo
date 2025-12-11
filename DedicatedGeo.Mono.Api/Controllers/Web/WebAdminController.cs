using DedicatedGeo.Mono.Dtos.Web;
using Microsoft.AspNetCore.Mvc;

namespace DedicatedGeo.Mono.Api.Controllers.Web;

public class WebAdminController: Controller
{


    [Route("/")]
    public async Task<IActionResult> Index()
    {
        return View(new LocationPathViewDto
        {
            Title = "Location Viewer"
        });
    }    
}