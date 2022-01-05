using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MainApp.Services;

public static class AuthenticationServices
{
    public static async Task RealizarLogin(HttpContext context, string accessToken ,string name)
    {
        var token = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
        var claims = new List<Claim>();
        claims.Add(new Claim("JWT", accessToken));
        claims.Add(new Claim(ClaimTypes.Name, name));
        claims.AddRange(token.Claims);

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
            IsPersistent = true
        };
        await context.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties); 
    }
}