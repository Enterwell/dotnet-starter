using System;
using System.Collections.Generic;

namespace Enterwell.Exceptions
{
    /// <summary>
    /// Enterwell validation exception class
    /// </summary>
    /// <seealso cref="EnterwellException" />
    public class EnterwellValidationException : EnterwellException
    {
        /// <summary>
        /// Gets the field errors.
        /// </summary>
        /// <value>
        /// The field errors.
        /// </value>
        public Dictionary<string, string> FieldErrors { get; private set; } = new Dictionary<string, string>();

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EnterwellValidationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnterwellValidationException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public EnterwellValidationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnterwellValidationException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="fieldErrors">The field errors.</param>
        public EnterwellValidationException(string message, Exception innerException, Dictionary<string, string> fieldErrors = null)
            : base(message, innerException)
        {
            if (fieldErrors != null)
            {
                this.FieldErrors = fieldErrors;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnterwellValidationException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="fieldErrors">The field errors.</param>
        /// <param name="key">The key.</param>
        /// <param name="innerException">The inner exception.</param>
        public EnterwellValidationException(string message, Dictionary<string, string> fieldErrors = null, string key = null, Exception innerException = null)
            : base(message, key, innerException)
        {
            if (fieldErrors != null)
            {
                this.FieldErrors = fieldErrors;
            }
        }

        /// <summary>
        /// Gets the default key.
        /// </summary>
        /// <returns>
        /// Returns default key
        /// </returns>
        protected override string GetDefaultKey()
        {
            return GenerateDefaultKey(base.GetDefaultKey(), "Validation");
        }

        /// <summary>
        /// Adds the error.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="error">The error.</param>
        /// <returns>Returns enterwell validation exception with new error added</returns>
        public EnterwellValidationException AddError(string field, string error)
        {
            this.FieldErrors.Add(field, error);
            return this;
        }
    }
}