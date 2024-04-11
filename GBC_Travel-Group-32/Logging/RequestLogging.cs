using Serilog;
using ILogger = Serilog.ILogger;




namespace GBC_Travel_Group_32.Logging {
    public class RequestLogging {


        private readonly RequestDelegate _next;
        
       


        public RequestLogging(RequestDelegate next) {
            _next = next;
       
        }


        public async Task Invoke(HttpContext context) {

            Log.Error("Starting web application");

            var request = context.Request;
            var response = context.Response;



           Log.Information($"Request: " +
                $"{request.Method} {request.Path} {request.QueryString}");

            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream()) {

                context.Response.Body = responseBody;

                await _next(context);

               Log.Information($"Response Status: {response.StatusCode}");


                responseBody.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);

            }


        }


    }
}
