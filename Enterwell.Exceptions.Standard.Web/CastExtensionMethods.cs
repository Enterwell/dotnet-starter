using Microsoft.AspNetCore.Mvc;

namespace Enterwell.Exceptions.Web
{
    /// <summary>
    /// Extension methods class for converting enterwell exceptions
    /// </summary>
    public static class EnterwellHttpExceptionExtensions
    {
        /// <summary>
        /// Converts EnterwellException to EnterwellHttpException
        /// </summary>
        /// <param name="enterwellException">The enterwell exception.</param>
        /// <returns>Returns created enterwell http exception</returns>
        public static EnterwellHttpException AsHttpException(this EnterwellException enterwellException) => 
            ExceptionsHelper.ToEnterwellHttpException(enterwellException);

        /// <summary>
        /// An EnterwellHttpException extension method that converts an enterwellHttpException to an
        /// action result.
        /// </summary>
        /// <param name="enterwellHttpException">The enterwellHttpException to act on.</param>
        /// <returns>
        /// An IActionResult.
        /// </returns>
        public static IActionResult AsActionResult(this EnterwellHttpException enterwellHttpException) => 
            new EnterwellHttpExceptionResult(enterwellHttpException);
    }
}
