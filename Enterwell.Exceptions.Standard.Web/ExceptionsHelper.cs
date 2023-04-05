namespace Enterwell.Exceptions.Web
{
    /// <summary>
    /// Helper class for Enterwell exceptions
    /// </summary>
    public static class ExceptionsHelper
    {
        /// <summary>
        /// Converts the enterwell exception to enterwell HTTP exception.
        /// </summary>
        /// <param name="enterwellException">The enterwell exception.</param>
        /// <returns>Returns converted enterwell http exception</returns>
        public static EnterwellHttpException ToEnterwellHttpException(
            EnterwellException enterwellException)
        {
            var httpCode = (int) System.Net.HttpStatusCode.BadRequest;

            switch (enterwellException)
            {
                case EnterwellValidationException _:
                    httpCode = (int) System.Net.HttpStatusCode.BadRequest;
                    break;
                case EnterwellForbiddenException _:
                    httpCode = (int) System.Net.HttpStatusCode.Forbidden;
                    break;
                case EnterwellUnauthorizedException _:
                    httpCode = (int) System.Net.HttpStatusCode.Unauthorized;
                    break;
                case EnterwellEntityNotFoundException _:
                    httpCode = (int) System.Net.HttpStatusCode.NotFound;
                    break;
            }

            return new EnterwellHttpException(
                enterwellException.Key,
                httpCode,
                enterwellException.Message,
                enterwellException);
        }
    }
}