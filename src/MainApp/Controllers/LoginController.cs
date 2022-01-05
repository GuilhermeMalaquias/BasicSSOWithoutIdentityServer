using System.Text;
using Microsoft.AspNetCore.Mvc;
using MainApp.Models;
using System.Text.Json;
using MainApp.Services;
using Microsoft.AspNetCore.Authentication;

namespace MainApp.Controllers;

public class LoginController : Controller
{
    public async Task<ActionResult> Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<ActionResult> Login(Login login)
    {
        if(!ModelState.IsValid) return View();
        var handler = new HttpClientHandler();
        handler.ClientCertificateOptions = ClientCertificateOption.Manual;
        handler.ServerCertificateCustomValidationCallback = 
            (httpRequestMessage, cert, cetChain, policyErrors) => {return true;};

        var client = new HttpClient(handler);
        var response = await client.PostAsync("https://localhost:7069/api/auth",
                new StringContent(JsonSerializer.Serialize(login), Encoding.UTF8, "application/json"));
        if ((int)response.StatusCode != 200)
        {
            return View();
        }
        var model = JsonSerializer.Deserialize<ResultSuccessLogin>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        await AuthenticationServices.RealizarLogin(HttpContext, model.AccessToken, model.Name);
        return RedirectToAction("Index", "Home");
    }
    public async Task<ActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    
}