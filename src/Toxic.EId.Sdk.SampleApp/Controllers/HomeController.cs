using Microsoft.AspNetCore.Mvc;

namespace Toxic.EId.Sdk.SampleApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [Route("bankid-sandbox")]
    public IActionResult BankIdSandbox()
    {
        return View();
    }

    [Route("bankid-real")]
    public IActionResult BankIdReal()
    {
        return View();
    }

    [Route("freja-sandbox")]
    public IActionResult FrejaEIdSandbox()
    {
        return View();
    }

    [Route("freja-real")]
    public IActionResult FrejaEIdReal()
    {
        return View();
    }
}
