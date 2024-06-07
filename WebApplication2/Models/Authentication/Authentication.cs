using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication2.Models.Authentication
{
    public class Authentication: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("cusEmail") == null)
            {
                context.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    {"Controller", "Access" },
                    {"Action", "Login" }
                });
            }
        }
    }
}
