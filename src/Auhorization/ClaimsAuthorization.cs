using System.Security.Claims;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using IAuthorizationFilter = Microsoft.AspNetCore.Mvc.Filters.IAuthorizationFilter;
using RedirectResult = System.Web.Mvc.RedirectResult;

namespace Auhorization;

public static class ClaimsAuthorization
{
    public static bool ValidCLaimType(HttpContext context, string claimType, string claimValue)
    {
        return context.User.Identity != null &&
               context.User.Identity.IsAuthenticated &&
               context.User.Claims.Any(a => a.Type == claimType && a.Value.Contains(claimValue));
    }
}

public class CustomAuthorizationFilter : IAuthorizationFilter
{
    public Claim _claim { get; set; }
    public CustomAuthorizationFilter(Claim claim)
    {
        _claim = claim;
    }
    

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.Identity != null && 
            !context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new Microsoft.AspNetCore.Mvc.RedirectResult("");
            return;
        }

        if (!ClaimsAuthorization.ValidCLaimType(context.HttpContext, _claim.Type, _claim.Value))
        {
            context.Result = new ForbidResult();
            return;
        }
    }
}

public class CustomAuthorizationAttribute : TypeFilterAttribute
{
    public CustomAuthorizationAttribute(string claimType, string claimValue) : base(typeof(CustomAuthorizationFilter))
    {
        Arguments = new object[] { new Claim(claimType, claimValue) };
    }
}