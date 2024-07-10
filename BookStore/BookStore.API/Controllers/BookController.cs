using Bookstore.Application.DTO;
using Bookstore.Application.Interfaces;
using BookStore.BookStore.Application.Utils;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

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

    [HttpDelete("{isbn}")]
    public ActionResult DeleteBook(string isbn)
    {
        _bookService.DeleteBook(isbn);
        return NoContent();
    }

    [HttpGet("report")]
    public ActionResult<string> GetReport()
    {
        var report = _bookService.GenerateReport();
        return Content(report, "text/html");
    }
}
