namespace Enterwell.Exceptions.Web
{
    /// <summary>
    /// Enterwell exception
    /// </summary>
    public class EnterwellHttpException
    {
        /// <summary>
        /// Gets the error code.
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        public EnterwellException Exception { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnterwellHttpException"/> class.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="httpCode">The HTTP code.</param>
        /// <param name="errorDescription">The error description.</param>
        public EnterwellHttpException(string error, int httpCode, string errorDescription)
        {
            this.Message = errorDescription;
            this.StatusCode = httpCode;
            this.Error = error;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnterwellHttpException"/> class.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="errorDescription">The error description.</param>
        /// <param name="exception">The exception.</param>
        public EnterwellHttpException(string error, string errorDescription, EnterwellException exception)
        {
            this.Message = errorDescription;
            this.Error = error;
            this.Exception = exception;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnterwellHttpException"/> class.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="httpCode">The HTTP code.</param>
        /// <param name="errorDescription">The error description.</param>
        /// <param name="exception">The exception.</param>
        public EnterwellHttpException(string error, int httpCode, string errorDescription, EnterwellException exception)
        {
            this.Message = errorDescription;
            this.StatusCode = httpCode;
            this.Error = error;
            this.Exception = exception;
        }
    }
}
