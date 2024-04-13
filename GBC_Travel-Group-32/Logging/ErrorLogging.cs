using Serilog;

namespace GBC_Travel_Group_32.Logging {
    public class ErrorLogging {

        private readonly RequestDelegate _next;

        public ErrorLogging(RequestDelegate next) {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext context) {

           

            try {
              
                await _next(context);
            }

            catch (Exception ex) {

             

                Log.Error("{Message} An error occurred: {ErrorMessage}\n{StackTrace}", "[Error Middleware]", ex.Message, ex.StackTrace);

                Log.Error("{Message} User: {UserId}", "[Error Middleware]", context.User.Identity?.Name);

                throw;
               
            }

        }


    }
}
