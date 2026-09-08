using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Toxic.GrandId.Sdk.SampleApp.Models;

namespace Toxic.GrandId.Sdk.SampleApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
