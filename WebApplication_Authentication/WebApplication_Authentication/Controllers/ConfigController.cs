using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApplication_Authentication.Data;

namespace WebApplication_Authentication.Controllers
{
    [ApiController]
    [Route("")]
    public class ConfigController : ControllerBase
    {
        // appsettings.json
        // Environment variables
        // User Secrets

        private readonly IConfiguration _configuration;
        // private readonly IOptions<AttachmentOptions> _attachments; // read one time only => Singleton Scoped
        // private readonly IOptionsSnapshot<AttachmentOptions> _attachments; // Update on every Req only => Singleton Scoped
        private readonly IOptionsMonitor<AttachmentOptions> _attachments; // Update when config changes

        public ConfigController(IConfiguration configuration, IOptionsMonitor<AttachmentOptions> attachments)
        {
            _configuration = configuration;
            _attachments = attachments;
            //var value = attachments.Value; // IOptionsSnapshot
            var value = attachments.CurrentValue; // IOptionsMonitor
        }

        [HttpGet]
        [Route("")]
        public ActionResult GetConfig()
        {
            Thread.Sleep(10000);
            var config = new
            {
                AllowedHosts = _configuration["AllowedHosts"],
                ConnectionStrings = _configuration["ConnectionStrings:DefaultConnection"],
                LoggingLevel = _configuration["Logging:LogLevel:Default"],
                //AttachmentOptions = _attachments.Value // IOptionsSnapshot
                AttachmentOptions = _attachments.CurrentValue // IOptionsMonitor
            };

            return Ok(config);
        }
    }
}
