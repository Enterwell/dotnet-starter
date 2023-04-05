using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Enterwell.Exceptions.Web
{
    /// <summary>
    /// Encapsulates the result of an Enterwell HTTP exception.
    /// </summary>
    public class EnterwellHttpExceptionResult : IActionResult
    {
        private readonly EnterwellHttpException exception;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="exception">Details of the exception.</param>
        public EnterwellHttpExceptionResult(EnterwellHttpException exception)
        {
            this.exception = exception;
        }

        /// <summary>
        /// Executes the result operation of the action method asynchronously. This method is called by
        /// MVC to process the result of an action method.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="context">The context in which the result is executed. The context information
        ///                       includes information about the action that was executed and request
        ///                       information.</param>
        /// <returns>
        /// A task that represents the asynchronous execute operation.
        /// </returns>
        public Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            // Log result
            var factory = context.HttpContext.RequestServices.GetService(typeof(ILoggerFactory)) as ILoggerFactory;
            factory.CreateLogger<EnterwellHttpException>().Log(
                LogLevel.Information, 
                new EventId(1, "EnterwellHttpExceptionResult"), 
                "Executing EnterwellHttpExceptionResult, setting HTTP status code {StatusCode}",
                this.exception.StatusCode);
            
            // Set status code
            context.HttpContext.Response.StatusCode = this.exception.StatusCode;

            // Return result executor task
            var executor = context.HttpContext.RequestServices.GetRequiredService<IActionResultExecutor<ObjectResult>>();
            return executor.ExecuteAsync(context, new ObjectResult(this.exception));
        }
    }
}