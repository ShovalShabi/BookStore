using Bookstore.Domain.Entities;
using Bookstore.Domain.Repositories;
using System.Xml.Linq;

public class BookRepository : IBookRepository
{
    private readonly XmlDataContext _context;

    public BookRepository(XmlDataContext context)
    {
        _context = context;
    }

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
