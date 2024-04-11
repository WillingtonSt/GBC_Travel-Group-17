using Serilog;

namespace GBC_Travel_Group_32.Logging {
    public class ErrorLogging {

        private readonly RequestDelegate _next;

        public ErrorLogging(RequestDelegate next) {
            _next = next;
        }


        public async Task Invoke(HttpContext context) {

           

            try {
                await _next(context);
            }

            catch (Exception ex) {

                Log.Error(ex, "An error occurred: {ErrorMessage}\n{StackTrace}", ex.Message, ex.StackTrace);

                Log.Error("User: {UserId}", context.User.Identity?.Name);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(ex.Message);

            }

        }


    }
}
