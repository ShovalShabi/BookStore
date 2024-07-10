namespace BookStore.BookStore.Application.Utils
{
    public class BookServiceException : Exception
    {
        public int ErrorCode { get; }

        public BookServiceException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
