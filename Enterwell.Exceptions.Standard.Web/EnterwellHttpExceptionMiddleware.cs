using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Enterwell.Exceptions.Web
{
    /// <summary>
    /// An Enterwell HTTP exception middleware.
    /// </summary>
    public class EnterwellHttpExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<EnterwellHttpExceptionMiddleware> logger;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="next">The next.</param>
        public EnterwellHttpExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this.next = next;
            this.logger = loggerFactory?.CreateLogger<EnterwellHttpExceptionMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));

        }


        /// <summary>
        /// Executes the given operation on a different thread, and waits for the result.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// An asynchronous result.
        /// </returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next.Invoke(context);
            }
            catch (EnterwellException httpException)
            {
                if (context.Response.HasStarted)
                {
                    this.logger.LogWarning("The response has already started, the http status code middleware will not be executed.");
                    throw;
                }

                var result = httpException
                    .AsHttpException();

                context.Response.StatusCode = result.StatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
            }
        }
    }
}