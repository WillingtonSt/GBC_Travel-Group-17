using Serilog;
using Serilog.Context;
using ILogger = Serilog.ILogger;




namespace GBC_Travel_Group_32.Logging {
    public class RequestLogging {


        private readonly RequestDelegate _next;
    
        
       


        public RequestLogging(RequestDelegate next) {
            _next = next;
          
        }


        public async Task Invoke(HttpContext context) {

            
         

            var request = context.Request;
            var response = context.Response;




            Log.Information("{Message} Request: {Method}: {Path}{QueryString}", "[Request Middleware]", request.Method, request.Path, request.QueryString);

                var originalBodyStream = context.Response.Body;
                using (var responseBody = new MemoryStream()) {

                    context.Response.Body = responseBody;



                    await _next(context);

                Log.Information("{Message} Response Status: {StatusCode}", "[Request Middleware]", response.StatusCode);


                    responseBody.Seek(0, SeekOrigin.Begin);
                    await responseBody.CopyToAsync(originalBodyStream);

                


            }


        }


    }
}
