using System.Collections.Generic;
using Bookstore.Application.DTO;

namespace Bookstore.Domain.Entities
{
    /// <summary>
    /// Represents a book entity with properties like ISBN, title, authors, year, price, category, and cover.
    /// </summary>
    public class Book
    {
        private string _isbn;
        private string _title;
        private List<string> _authors;
        private int _year;
        private decimal _price;
        private string _category;
        private string _cover;

        /// <summary>
        /// Default constructor that initializes the authors list.
        /// </summary>
        public Book()
        {
            _authors = new List<string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Book"/> class with specified parameters.
        /// </summary>
        /// <param name="isbn">The ISBN of the book.</param>
        /// <param name="title">The title of the book.</param>
        /// <param name="authors">The list of authors of the book.</param>
        /// <param name="year">The publication year of the book.</param>
        /// <param name="price">The price of the book.</param>
        /// <param name="category">The category or genre of the book.</param>
        /// <param name="cover">The cover image URL of the book (optional).</param>
        public Book(string isbn, string title, List<string> authors, int year, decimal price, string category, string cover = null)
        {
            _isbn = isbn;
            _title = title;
            _authors = authors;
            _year = year;
            _price = price;
            _category = category;
            _cover = cover;
        }

        /// <summary>
        /// Gets or sets the ISBN of the book.
        /// </summary>
        public string Isbn
        {
            get { return _isbn; }
            set { _isbn = value; }
        }

        /// <summary>
        /// Gets or sets the title of the book.
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <summary>
        /// Gets or sets the list of authors of the book.
        /// </summary>
        public List<string> Authors
        {
            get { return _authors; }
            set { _authors = value; }
        }

        /// <summary>
        /// Gets or sets the publication year of the book.
        /// </summary>
        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }

        /// <summary>
        /// Gets or sets the price of the book.
        /// </summary>
        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        /// <summary>
        /// Gets or sets the category or genre of the book.
        /// </summary>
        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }

        /// <summary>
        /// Gets or sets the cover image URL of the book (optional).
        /// </summary>
        public string Cover
        {
            get { return _cover; }
            set { _cover = value; }
        }

        /// <summary>
        /// Converts the current instance of <see cref="Book"/> to its corresponding <see cref="BookDTO"/> object.
        /// </summary>
        /// <returns>A <see cref="BookDTO"/> object representing the current book instance.</returns>
        public BookDTO ToDTO()
        {
            return new BookDTO(_isbn, _title, string.Join(", ", _authors), _year, _price, _category, _cover);
        }
    }
}
