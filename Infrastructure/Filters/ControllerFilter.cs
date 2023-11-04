namespace Corporate.Application.Services.Infrastructure.Filters;

using Microsoft.AspNetCore.Mvc.Filters;

public class ControllerFilter : ActionFilterAttribute
{
    private readonly ILogger<ControllerFilter> _logger;

    public ControllerFilter(ILogger<ControllerFilter> logger)
    {
        _logger = logger;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation($"OnActionExecuting => RouteData-controller: {context.RouteData.Values["controller"]}, RouteData-action: {context.RouteData.Values["action"]}");
    }

    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
        _logger.LogInformation("OnActionExecuted");
    }

    public override void OnResultExecuting(ResultExecutingContext filterContext)
    {
        _logger.LogInformation("OnResultExecuting");
    }

    public override void OnResultExecuted(ResultExecutedContext context)
    {
        _logger.LogInformation($"OnResultExecuted => RouteData-controller: {context.RouteData.Values["controller"]}, RouteData-action: {context.RouteData.Values["action"]}");
    }
}