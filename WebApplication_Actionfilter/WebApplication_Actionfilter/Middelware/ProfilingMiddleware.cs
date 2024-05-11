using System.Diagnostics;

namespace WebApplication_Actionfilter.Middelware
{
    public class ProfilingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ProfilingMiddleware> _logger;
        public ProfilingMiddleware(RequestDelegate next, ILogger<ProfilingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            await _next(context);
            stopWatch.Stop();
            _logger.LogInformation($"Request '{context.Request.Path}' took '{stopWatch.ElapsedMilliseconds}ms'");
        }
    }
}
