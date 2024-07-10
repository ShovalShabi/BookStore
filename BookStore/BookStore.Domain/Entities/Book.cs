using Bookstore.Application.DTO;

namespace Bookstore.Domain.Entities
{
    public class Book
    {
        private string _isbn;
        private string _title;
        private List<string> _authors;
        private int _year;
        private decimal _price;
        private string _category;
        private string _cover;

        // Constructor for easier initialization
        public Book()
        {
            _authors = new List<string>();
        }

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

        public string Isbn
        {
            get { return _isbn; }
            set { _isbn = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public List<string> Authors
        {
            get { return _authors; }
            set { _authors = value; }
        }

        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }

        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }

        public string Cover
        {
            get { return _cover; }
            set { _cover = value; }
        }

        // Method to convert Book to BookDto
        public BookDTO ToDTO()
        {
            return new BookDTO(_isbn, _title, string.Join(", ", _authors), _year, _price, _category, _cover);
        }
    }
}
