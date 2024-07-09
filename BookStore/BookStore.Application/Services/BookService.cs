using Bookstore.Application.DTO;
using Bookstore.Application.Interfaces;
using Bookstore.Domain.Entities;
using Bookstore.Domain.Repositories;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly ReportGenerator _reportGenerator;

    public BookService(IBookRepository bookRepository, ReportGenerator reportGenerator)
    {
        _bookRepository = bookRepository;
        _reportGenerator = reportGenerator;
    }

    public BookDto GetBookByIsbn(string isbn)
    {
        var book = _bookRepository.GetByIsbn(isbn);
        if (book == null) return null;

        return new BookDto
        {
            Isbn = book.Isbn,
            Title = book.Title,
            Authors = string.Join(", ", book.Authors),
            Year = book.Year,
            Price = book.Price,
            Category = book.Category,
            Cover = book.Cover
        };
    }

    public void AddBook(BookDto bookDto)
    {
        var book = new Book
        {
            Isbn = bookDto.Isbn,
            Title = bookDto.Title,
            Authors = bookDto.Authors.Split(", ").ToList(),
            Year = bookDto.Year,
            Price = bookDto.Price,
            Category = bookDto.Category,
            Cover = bookDto.Cover
        };
        _bookRepository.Add(book);
    }

    public void EditBook(string isbn, BookDto bookDto)
    {
        var book = new Book
        {
            Isbn = bookDto.Isbn,
            Title = bookDto.Title,
            Authors = bookDto.Authors.Split(", ").ToList(),
            Year = bookDto.Year,
            Price = bookDto.Price,
            Category = bookDto.Category,
            Cover = bookDto.Cover
        };
        _bookRepository.Update(isbn, book);
    }

    public void DeleteBook(string isbn)
    {
        _bookRepository.Delete(isbn);
    }

    public string GenerateReport()
    {
        var books = _bookRepository.GetAll();
        return _reportGenerator.GenerateHtmlReport(books);
    }
}
