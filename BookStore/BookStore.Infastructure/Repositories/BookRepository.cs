using Bookstore.Domain.Entities;
using Bookstore.Domain.Repositories;
using System.Xml.Linq;

/// <summary>
/// Repository implementation for interacting with books stored in XML format.
/// </summary>
public class BookRepository : IBookRepository
{
    private readonly XmlDataContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookRepository"/> class.
    /// </summary>
    /// <param name="context">The XML data context to access XML storage.</param>
    public BookRepository(XmlDataContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves a book by its ISBN.
    /// </summary>
    /// <param name="isbn">The ISBN of the book to retrieve.</param>
    /// <returns>The <see cref="Book"/> object if found; otherwise, <c>null</c>.</returns>
    public Book GetByIsbn(string isbn)
    {
        var document = _context.LoadXml();
        var bookElement = document.Descendants("book")
            .FirstOrDefault(x => x.Element("isbn").Value == isbn);

        if (bookElement == null) return null;

        return new Book
        {
            Isbn = bookElement.Element("isbn").Value,
            Title = bookElement.Element("title").Value,
            Authors = bookElement.Elements("author").Select(x => x.Value).ToList(),
            Year = int.Parse(bookElement.Element("year").Value),
            Price = decimal.Parse(bookElement.Element("price").Value),
            Category = bookElement.Attribute("category")?.Value,
            Cover = bookElement.Attribute("cover")?.Value
        };
    }

    /// <summary>
    /// Adds a new book record.
    /// </summary>
    /// <param name="book">The <see cref="Book"/> object to add.</param>
    public void Add(Book book)
    {
        var document = _context.LoadXml();
        var bookstore = document.Element("bookstore");

        var bookElement = new XElement("book",
            new XAttribute("category", book.Category),
            book.Cover != null ? new XAttribute("cover", book.Cover) : null,
            new XElement("isbn", book.Isbn),
            new XElement("title", book.Title),
            book.Authors.Select(author => new XElement("author", author)),
            new XElement("year", book.Year),
            new XElement("price", book.Price));

        bookstore.Add(bookElement);
        _context.SaveXml(document);
    }

    /// <summary>
    /// Updates an existing book record identified by ISBN.
    /// </summary>
    /// <param name="isbn">The ISBN of the book to update.</param>
    /// <param name="book">The updated <see cref="Book"/> object.</param>
    public void Update(string isbn, Book book)
    {
        var document = _context.LoadXml();
        var bookElement = document.Descendants("book")
            .FirstOrDefault(x => x.Element("isbn").Value == isbn);

        if (bookElement != null)
        {
            bookElement.SetAttributeValue("category", book.Category);
            if (book.Cover != null)
                bookElement.SetAttributeValue("cover", book.Cover);
            bookElement.SetElementValue("isbn", book.Isbn);
            bookElement.SetElementValue("title", book.Title);
            bookElement.Elements("author").Remove();
            foreach (var author in book.Authors)
                bookElement.Add(new XElement("author", author));
            bookElement.SetElementValue("year", book.Year);
            bookElement.SetElementValue("price", book.Price);
            _context.SaveXml(document);
        }
    }

    /// <summary>
    /// Deletes a book record by its ISBN.
    /// </summary>
    /// <param name="isbn">The ISBN of the book to delete.</param>
    public void Delete(string isbn)
    {
        var document = _context.LoadXml();
        var bookElement = document.Descendants("book")
            .FirstOrDefault(x => x.Element("isbn").Value == isbn);

        if (bookElement != null)
        {
            bookElement.Remove();
            _context.SaveXml(document);
        }
    }

    /// <summary>
    /// Retrieves all book records.
    /// </summary>
    /// <returns>An enumerable collection of all <see cref="Book"/> objects.</returns>
    public IEnumerable<Book> GetAll()
    {
        var document = _context.LoadXml();
        return document.Descendants("book").Select(bookElement => new Book
        {
            Isbn = bookElement.Element("isbn").Value,
            Title = bookElement.Element("title").Value,
            Authors = bookElement.Elements("author").Select(x => x.Value).ToList(),
            Year = int.Parse(bookElement.Element("year").Value),
            Price = decimal.Parse(bookElement.Element("price").Value),
            Category = bookElement.Attribute("category")?.Value,
            Cover = bookElement.Attribute("cover")?.Value
        });
    }
}
