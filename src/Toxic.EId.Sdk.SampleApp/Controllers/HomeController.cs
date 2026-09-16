using Microsoft.AspNetCore.Mvc;

namespace Toxic.EId.Sdk.SampleApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [Route("sandbox")]
    public IActionResult Sandbox()
    {
        return View();
    }

    [Route("real")]
    public IActionResult Real()
    {
        return View();
    }
}
