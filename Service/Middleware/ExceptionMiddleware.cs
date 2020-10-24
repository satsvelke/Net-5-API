using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Persistence.DatabaseContext;
using ViewModel;

namespace Service.Middleware
{
    // Excepetion Middleware specific dbcontext e.g SpecificContext
    // called directly in Middleware
    // if the database context is diff. change the context class 
    public static partial class ExceptionMiddleware
    {
        public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder app, IHostEnvironment environment)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));
            return app.UseMiddleware<UseExceptionHandling>();
        }
    }

    public partial class UseExceptionHandling
    {
        private readonly RequestDelegate next;
        public UseExceptionHandling(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext httpContext, SpecificContext specificontext)
        {
            // Try and retrieve the error from the ExceptionHandler middleware
            var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();
            var ex = exceptionDetails?.Error;
            // Should always exist, but best to be safe!
            if (ex != null)
            {

                // ProblemDetails has it's own content type
                httpContext.Response.ContentType = "application/problem+json";

                var errorList = new List<string>();
                errorList.Add("No Validation Error Occurred");

                var errors = new Errors()
                {
                    ErrorList = errorList
                };
                var errorsMessage = new ErrorMessage()
                {
                    type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                    title = "Server Error",
                    status = 500,
                    errors = errors
                };

                // This is often very handy information for tracing the specific request
                var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
                if (traceId != null)
                {
                    errorsMessage.traceId = traceId;
                }

                await Task.Factory.StartNew(() =>
              {
                  LogErrorToDatabase(specificontext, httpContext, errorsMessage);
              });
                //Serialize the problem details object to the Response as JSON (using System.Text.Json)
                string jsonString = JsonConvert.SerializeObject(errorsMessage);
                await httpContext.Response.WriteAsync(jsonString, Encoding.UTF8);
                // to stop futher pipeline execution 
                return;
            }

        }
        private void LogErrorToDatabase(SpecificContext context, HttpContext httpContext, ErrorMessage errorMessage)
        {
            using (context)
            {
                var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();
                var ex = exceptionDetails?.Error;

                var exception = new Model.Exception()
                {
                    ApplicationName = "Base Api Arch", // change whatever the name of app is 
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    HTTPMethod = httpContext.Request.Method,
                    StatusCode = "500",
                    Type = errorMessage.type,
                    TraceId = errorMessage.traceId,
                    Url = httpContext.Request.Path,
                    Response = JsonConvert.SerializeObject(errorMessage)
                };

                context.Add(exception);
                context.SaveChanges();
            }
        }
    }
}