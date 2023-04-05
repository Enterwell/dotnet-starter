using Microsoft.AspNetCore.Builder;

namespace Enterwell.Exceptions.Web
{
    /// <summary>
    /// An Enterwell HTTP exception middleware extensions.
    /// </summary>
    public static class EnterwellHttpExceptionMiddlewareExtensions
    {
        /// <summary>
        /// An IApplicationBuilder extension method that will register Enterwell HTTP exception middleware.
        /// </summary>
        /// <param name="builder">The builder to act on.</param>
        /// <returns>
        /// An IApplicationBuilder.
        /// </returns>
        public static IApplicationBuilder UseEnterwellHttpExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<EnterwellHttpExceptionMiddleware>();
        }
    }
}