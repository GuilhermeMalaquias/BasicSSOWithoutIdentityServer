using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using App1.Models;
using Auhorization;

namespace App1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    [CustomAuthorization("Home","Admin")]
    public ActionResult Admin()
    {
        return View();
    }
    [CustomAuthorization("Home","Gestor")]
    public ActionResult Gestor()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}