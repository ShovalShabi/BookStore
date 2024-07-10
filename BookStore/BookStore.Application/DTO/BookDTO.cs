namespace Bookstore.Application.DTO
{
    /// <summary>
    /// Data Transfer Object for Book entity.
    /// </summary>
    public class BookDTO
    {
        private string _isbn;
        private string _title;
        private string _authors; // Comma-separated authors.
        private int _year;
        private decimal _price;
        private string _category;
        private string _cover; // Optional

        /// <summary>
        /// Initializes a new instance of the <see cref="BookDTO"/> class.
        /// </summary>
        public BookDTO()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BookDTO"/> class with specified properties.
        /// </summary>
        /// <param name="isbn">The ISBN of the book.</param>
        /// <param name="title">The title of the book.</param>
        /// <param name="authors">The authors of the book, comma-separated.</param>
        /// <param name="year">The publication year of the book.</param>
        /// <param name="price">The price of the book.</param>
        /// <param name="category">The category of the book.</param>
        /// <param name="cover">The cover image of the book (optional).</param>
        public BookDTO(string isbn, string title, string authors, int year, decimal price, string category, string cover = null)
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
        /// Gets or sets the authors of the book, comma-separated.
        /// </summary>
        public string Authors
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
        /// Gets or sets the category of the book.
        /// </summary>
        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }

        /// <summary>
        /// Gets or sets the cover image of the book.
        /// </summary>
        public string Cover
        {
            get { return _cover; }
            set { _cover = value; }
        }
    }
}
