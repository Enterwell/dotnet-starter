using System;

namespace Enterwell.Exceptions
{
    /// <summary>
    /// Enterwell unauthorized exception class
    /// </summary>
    /// <seealso cref="EnterwellException" />
    public class EnterwellUnauthorizedException : EnterwellException
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public EnterwellUnauthorizedException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnterwellUnauthorizedException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public EnterwellUnauthorizedException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnterwellUnauthorizedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public EnterwellUnauthorizedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnterwellUnauthorizedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="key">The key.</param>
        /// <param name="innerException">The inner exception.</param>
        public EnterwellUnauthorizedException(string message, string key = null, Exception innerException = null)
            : base(message, key, innerException)
        {
        }

        /// <summary>
        /// Gets the default key.
        /// </summary>
        /// <returns>
        /// Returns default key
        /// </returns>
        protected override string GetDefaultKey()
        {
            return GenerateDefaultKey(base.GetDefaultKey(), "Unauthorized");
        }
    }
}
