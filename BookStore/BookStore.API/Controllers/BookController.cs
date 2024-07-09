using Bookstore.Application.DTO;
using Bookstore.Application.Interfaces;
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
    public ActionResult<BookDto> GetBook(string isbn)
    {
        var book = _bookService.GetBookByIsbn(isbn);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    [HttpPost]
    public ActionResult AddBook([FromBody] BookDto bookDto)
    {
        _bookService.AddBook(bookDto);
        return CreatedAtAction(nameof(GetBook), new { isbn = bookDto.Isbn }, bookDto);
    }

    [HttpPut("{isbn}")]
    public ActionResult EditBook(string isbn, [FromBody] BookDto bookDto)
    {
        _bookService.EditBook(isbn, bookDto);
        return NoContent();
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
