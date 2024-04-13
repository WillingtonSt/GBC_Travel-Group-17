using Microsoft.AspNetCore.Mvc.Filters;

namespace GBC_Travel_Group_32.Logging {
    public class LoggingFilter : IActionFilter {

        private readonly ILogger _logger;




        public LoggingFilter(ILoggerFactory loggerFactory) {

            _logger = loggerFactory.CreateLogger("LoggingFilter");

        }

        public void OnActionExecuting(ActionExecutingContext context) {

            _logger.LogInformation("{Message} ACTION STARTED: {ActionName}", "(LOGGING FILTER)", context.ActionDescriptor.DisplayName);
        }


        public void OnActionExecuted(ActionExecutedContext context) {

            _logger.LogInformation("{Message} ACTION COMPLETED: {ActionName}", "(LOGGING FILTER)" , context.ActionDescriptor.DisplayName);
        }

    }
}
