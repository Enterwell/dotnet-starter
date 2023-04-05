namespace Acme.Core.Exceptions;

/// <summary>
/// Application error codes class.
/// </summary>
public class ErrorCodes
{
    /// <summary>
    /// Authentication error codes.
    /// </summary>
    public class Authentication
    {
        /// <summary>
        /// Invalid email address or password.
        /// </summary>
        public const string InvalidEmailOrPassword = "authentication#invalidEmailOrPassword";
    }
}