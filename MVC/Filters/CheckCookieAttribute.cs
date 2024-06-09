using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

public class Authorize : ActionFilterAttribute
{
    private readonly string _loginUrl;

    public Authorize(string loginUrl = "/auth/login")
    {
        _loginUrl = loginUrl;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var httpContext = context.HttpContext;
        var cookieValue = httpContext.Request.Cookies["jwt"];

        if (string.IsNullOrEmpty(cookieValue))
        {
            context.Result = new RedirectResult(_loginUrl);
        }

        base.OnActionExecuting(context);
    }
}
