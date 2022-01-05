using Auhorization;
using Microsoft.AspNetCore.Mvc;

namespace MainApp.Controllers;

public class HomeController : Controller
{
    public ActionResult Index()
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
}