using Bookstore.Application.DTO;
using Bookstore.Application.Interfaces;
using Bookstore.Domain.Entities;
using Bookstore.Domain.Repositories;
using BookStore.BookStore.Application.Utils;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly ReportGenerator _reportGenerator;
    private readonly ILogger<BookService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookService"/> class.
    /// </summary>
    /// <param name="bookRepository">The book repository instance.</param>
    /// <param name="reportGenerator">The report generator instance.</param>
    /// <param name="logger">The logger instance.</param>
    public BookService(IBookRepository bookRepository, ReportGenerator reportGenerator, ILogger<BookService> logger)
    {
        _bookRepository = bookRepository;
        _reportGenerator = reportGenerator;
        _logger = logger;
    }

    /// <summary>
    /// Retrieves a book by its ISBN.
    /// </summary>
    /// <param name="isbn">The ISBN of the book to retrieve.</param>
    /// <returns>A <see cref="BookDTO"/> representing the book.</returns>
    /// <exception cref="BookServiceException">Thrown when the book is not found.</exception>
    public BookDTO GetBookByIsbn(string isbn)
    {
        var book = _bookRepository.GetByIsbn(isbn);
        if (book == null || string.IsNullOrEmpty(isbn))
        {
            _logger.LogError($"Failed to retrieve book with ISBN: {isbn}. Book not found.");
            throw new BookServiceException("The book does not exist.", StatusCodes.Status404NotFound);
        }
        _logger.LogInformation($"Retrieved book with ISBN: {isbn}.");
        return book.ToDTO();
    }

    /// <summary>
    /// Adds a new book record.
    /// </summary>
    /// <param name="bookDto">The <see cref="BookDTO"/> representing the book to add.</param>
    /// <returns>A <see cref="BookDTO"/> representing the added book.</returns>
    /// <exception cref="BookServiceException">Thrown when the ISBN is invalid or the book already exists.</exception>
    public BookDTO AddBook(BookDTO bookDto)
    {
        if (bookDto.Isbn == null)
        {
            _logger.LogError("A book cannot be tracked with no ISBN code.");
            throw new BookServiceException("A book cannot be tracked with no ISBN code.", StatusCodes.Status400BadRequest);
        }

        if (bookDto.Isbn.Length != 10 && bookDto.Isbn.Length != 13)
        {
            _logger.LogError($"ISBN '{bookDto.Isbn}' is not valid.");
            throw new BookServiceException("ISBN code is not valid.", StatusCodes.Status400BadRequest);
        }

        var isExist = _bookRepository.GetByIsbn(bookDto.Isbn);
        if (isExist != null)
        {
            _logger.LogInformation($"Book with ISBN '{bookDto.Isbn}' already exists.");
            return isExist.ToDTO();
        }

        var book = new Book
        {
            Isbn = bookDto.Isbn,
            Title = bookDto.Title,
            Authors = [.. bookDto.Authors.Split(", ")],
            Year = bookDto.Year,
            Price = bookDto.Price,
            Category = bookDto.Category,
            Cover = bookDto.Cover
        };
        _bookRepository.Add(book);
        _logger.LogInformation($"Added new book with ISBN: {bookDto.Isbn}");

        return book.ToDTO();
    }

    /// <summary>
    /// Edits an existing book record identified by ISBN.
    /// </summary>
    /// <param name="isbn">The ISBN of the book to edit.</param>
    /// <param name="bookDto">The <see cref="BookDTO"/> representing the updated book details.</param>
    /// <exception cref="BookServiceException">Thrown when the book is not found.</exception>
    public void EditBook(string isbn, BookDTO bookDto)
    {
        var book = _bookRepository.GetByIsbn(isbn);
        if (book == null || string.IsNullOrEmpty(isbn))
        {
            _logger.LogError($"Failed to retrieve book with ISBN: {isbn}. Book not found.");
            throw new BookServiceException("The book does not exist.", StatusCodes.Status404NotFound);
        }

        book = new Book
        {
            Isbn = isbn,
            Title = bookDto.Title,
            Authors = [.. bookDto.Authors.Split(", ")],
            Year = bookDto.Year,
            Price = bookDto.Price,
            Category = bookDto.Category,
            Cover = bookDto.Cover
        };
        _bookRepository.Update(isbn, book);
        _logger.LogInformation($"Updated book with ISBN: {isbn}");
    }

    /// <summary>
    /// Deletes a book record by its ISBN.
    /// </summary>
    /// <param name="isbn">The ISBN of the book to delete.</param>
    public void DeleteBook(string isbn)
    {
        _bookRepository.Delete(isbn);
        _logger.LogInformation($"Deleted book with ISBN: {isbn}");
    }

    /// <summary>
    /// Generates an HTML report of all books.
    /// </summary>
    /// <returns>A string containing the HTML report.</returns>
    public string GenerateReport()
    {
        var books = _bookRepository.GetAll();
        var report = _reportGenerator.GenerateHtmlReport(books);
        _logger.LogInformation("Generated report");
        return report;
    }
}
