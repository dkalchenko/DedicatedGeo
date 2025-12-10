using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Dtos.Location;
using DedicatedGeo.Mono.Dtos.Web;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DedicatedGeo.Mono.Api.Controllers.Web;

public class LocationViewerController: Controller
{
    private readonly ISender _sender;

    public LocationViewerController(ISender sender)
    {
        _sender = sender.ThrowIfNull();
    }

    [Route("/")]
    public async Task<IActionResult> Index()
    {
        return View(new LocationPathViewDto
        {
            Title = "Location Viewer"
        });
    }    
}