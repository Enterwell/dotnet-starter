using System;

namespace Enterwell.Exceptions
{
    /// <summary>
    /// Enterwell entity not found exception class
    /// </summary>
    public class EnterwellEntityNotFoundException : EnterwellException
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public EnterwellEntityNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnterwellEntityNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public EnterwellEntityNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnterwellEntityNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public EnterwellEntityNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnterwellEntityNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="key">The key.</param>
        /// <param name="innerException">The inner exception.</param>
        public EnterwellEntityNotFoundException(string message, string key = null, Exception innerException = null)
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
            return GenerateDefaultKey(base.GetDefaultKey(), "EntityNotFound");
        }
    }
}
