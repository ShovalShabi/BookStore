namespace Bookstore.Application.DTO
{
    public class BookDTO
    {
        private string _isbn;
        private string _title;
        private string _authors; // Comma-separated authors.
        private int _year;
        private decimal _price;
        private string _category;
        private string _cover; // Optional

        // Full constructor that initializes all properties
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

        public string Authors
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

    }
}
