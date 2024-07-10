using Bookstore.Application.DTO;
using Bookstore.Application.Interfaces;
using BookStore.BookStore.Application.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Api.Controllers
{
    /// <summary>
    /// API Controller for managing books.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookController"/> class.
        /// </summary>
        /// <param name="bookService">The book service.</param>
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Gets a book by its ISBN.
        /// </summary>
        /// <param name="isbn">The ISBN of the book.</param>
        /// <returns>A <see cref="BookDTO"/> representing the book.</returns>
        [HttpGet("{isbn}")]
        public ActionResult<BookDTO> GetBook(string isbn)
        {
            try
            {
                var book = _bookService.GetBookByIsbn(isbn);
                return Ok(book);
            }
            catch (BookServiceException ex)
            {
                switch (ex.ErrorCode)
                {
                    case StatusCodes.Status404NotFound:
                        return NotFound(new ProblemDetails
                        {
                            Status = StatusCodes.Status404NotFound,
                            Title = "Not Found",
                            Detail = ex.Message
                        });
                    default:
                        return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
                }
            }
        }

        /// <summary>
        /// Adds a new book.
        /// </summary>
        /// <param name="bookDto">The book data transfer object.</param>
        /// <returns>A <see cref="BookDTO"/> representing the added book.</returns>
        [HttpPost]
        public ActionResult<BookDTO> AddBook(BookDTO bookDto)
        {
            try
            {
                var book = _bookService.AddBook(bookDto);
                return CreatedAtAction(nameof(AddBook), new { isbn = book.Isbn }, book);
            }
            catch (BookServiceException ex)
            {
                switch (ex.ErrorCode)
                {
                    case StatusCodes.Status400BadRequest:
                        return BadRequest(new ProblemDetails
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Title = "Bad Request",
                            Detail = ex.Message
                        });
                    default:
                        return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
                }
            }
        }

        /// <summary>
        /// Edits an existing book.
        /// </summary>
        /// <param name="isbn">The ISBN of the book to edit.</param>
        /// <param name="bookDto">The updated book data transfer object.</param>
        /// <returns>An <see cref="ActionResult"/> indicating the result of the operation.</returns>
        [HttpPut("{isbn}")]
        public ActionResult EditBook(string isbn, [FromBody] BookDTO bookDto)
        {
            try
            {
                _bookService.EditBook(isbn, bookDto);
                return NoContent();
            }
            catch (BookServiceException ex)
            {
                switch (ex.ErrorCode)
                {
                    case StatusCodes.Status400BadRequest:
                        return BadRequest(new ProblemDetails
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Title = "Bad Request",
                            Detail = ex.Message
                        });
                    default:
                        return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
                }
            }
        }

        /// <summary>
        /// Deletes a book by its ISBN.
        /// </summary>
        /// <param name="isbn">The ISBN of the book to delete.</param>
        /// <returns>An <see cref="ActionResult"/> indicating the result of the operation.</returns>
        [HttpDelete("{isbn}")]
        public ActionResult DeleteBook(string isbn)
        {
            _bookService.DeleteBook(isbn);
            return NoContent();
        }

        /// <summary>
        /// Generates a report of all books.
        /// </summary>
        /// <returns>An HTML report of all books.</returns>
        [HttpGet("report")]
        public ActionResult<string> GetReport()
        {
            var report = _bookService.GenerateReport();
            return Content(report, "text/html");
        }
    }
}
