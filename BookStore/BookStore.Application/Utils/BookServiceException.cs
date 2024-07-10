namespace BookStore.BookStore.Application.Utils
{
    /// <summary>
    /// Exception thrown when there is an error related to book service operations.
    /// </summary>
    public class BookServiceException : Exception
    {
        /// <summary>
        /// Gets the error code associated with the exception.
        /// </summary>
        public int ErrorCode { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BookServiceException"/> class with a specified error message and error code.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="errorCode">The error code associated with the exception.</param>
        public BookServiceException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
