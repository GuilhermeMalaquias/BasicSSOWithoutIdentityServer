using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication;

public static class AutheticationBasedCookieForMvcApp
{
    public static IServiceCollection AddSSOConfig(this IServiceCollection services)
    {
        services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo("/home/guilherme/Keys"))
            .SetApplicationName("SharedCookieApp");
        
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie("Cookies", options =>
            {
                options.Cookie.Name = ".AspNet.SharedCookie";
            });
        return services;
    }

}