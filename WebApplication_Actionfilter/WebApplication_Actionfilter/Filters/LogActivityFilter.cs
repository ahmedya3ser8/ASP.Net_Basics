using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace WebApplication_Actionfilter.Filters
{
    public class LogActivityFilter : IActionFilter, IAsyncActionFilter
    {
        private readonly ILogger<LogActivityFilter> _logger;

        public LogActivityFilter(ILogger<LogActivityFilter> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // context.Result = new NotFoundResult();
            _logger.LogInformation($"Executing action {context.ActionDescriptor.DisplayName} on controller {context.Controller} with arguments {JsonSerializer.Serialize(context.ActionArguments)}");
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"action {context.ActionDescriptor.DisplayName} Executed on controller {context.Controller}");
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation($"Executing action {context.ActionDescriptor.DisplayName} on controller {context.Controller} with arguments {JsonSerializer.Serialize(context.ActionArguments)}");
            await next();
            _logger.LogInformation($"action {context.ActionDescriptor.DisplayName} Executed on controller {context.Controller}");
        }
    }
}
