using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace WebApplication_Actionfilter.Filters
{
    public class LogSentitiveActionAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Debug.WriteLine("Sensitive action executed !!!!!!!");
        }
    }
}
