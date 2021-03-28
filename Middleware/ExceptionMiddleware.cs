using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Persistence.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Middleware
{
    // Excepetion Middleware specific dbcontext e.g SpecificContext
    // called directly in Middleware
    // if the database context is diff. change the context class 
    public static partial class ExceptionMiddleware
    {
        public static IApplicationBuilder UseExceptions(this IApplicationBuilder app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            return app.UseMiddleware<UseExceptionHandling>();
        }
    }

    public partial class UseExceptionHandling
    {
        private readonly RequestDelegate next;
        public UseExceptionHandling(RequestDelegate next) => this.next = next;

        /// <summary>
        /// Invoked whern global application exception happens 
        /// </summary>
        /// <param name="httpContext">current request context</param>
        /// <param name="specificontext">database context in which the exceptions will be logged </param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext, SpecificContext specificontext)
        {

            ///get the body of request 
            string requestBody = null;
            httpContext.Request.Body.Position = 0;

            using (var reader = new StreamReader(httpContext.Request.Body, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false,
                     bufferSize: 8192, leaveOpen: true))
                requestBody = await reader.ReadToEndAsync();

            httpContext.Request.Body.Position = 0;

            // Try and retrieve the error from the ExceptionHandler middleware
            var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();

            var ex = exceptionDetails?.Error;
            // Should always exist, but best to be safe!
            if (ex == null)
                return;

            // ProblemDetails has it's own content type
            httpContext.Response.ContentType = "application/problem+json";

            var errorList = new List<string>
                {
                    "Request failed"
                };

            var errors = new Errors()
            {
                ErrorList = errorList
            };

            var errorsMessage = new GenericMessage()
            {
                type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                title = "Internal Server Error",
                status = 500,
                errors = errors
            };

            // This is often very handy information for tracing the specific request
            var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
            if (traceId != null)
                errorsMessage.traceId = traceId;

            await Task.Factory.StartNew(() =>
            {
                LogErrorToDatabase(specificontext, httpContext, errorsMessage, requestBody);
            });

            //Serialize the problem details object to the Response as JSON (using System.Text.Json)
            string jsonString = JsonConvert.SerializeObject(errorsMessage);

            await httpContext.Response.WriteAsync(jsonString, Encoding.UTF8);

            // to stop futher pipeline execution 
            return;

        }

        /// <summary>
        /// Logs data to database(Exception  table )
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="httpContext">Current request context</param>
        /// <param name="errorMessage">Custom Erromessages </param>
        private static void LogErrorToDatabase(SpecificContext context, HttpContext httpContext, GenericMessage errorMessage, string requestBody)
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
                    Response = JsonConvert.SerializeObject(errorMessage), // error response 
                    IPAddress = httpContext.Connection.RemoteIpAddress.ToString(),
                    RequestBody = requestBody,
                    Host = httpContext.Request.Host.ToString(),
                };

                context.Add(exception);
                context.SaveChanges();
            }
        }
    }
}