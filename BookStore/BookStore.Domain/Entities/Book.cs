using System.Collections.Generic;

namespace Bookstore.Domain.Entities
{
    public class Book
    {
        public string Isbn { get; set; }
        public string Title { get; set; }
        public List<string> Authors { get; set; } = new List<string>();
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Cover { get; set; } // Optional

        // Constructor for easier initialization
        public Book() { }

        public Book(string isbn, string title, List<string> authors, int year, decimal price, string category, string cover = null)
        {
            Isbn = isbn;
            Title = title;
            Authors = authors;
            Year = year;
            Price = price;
            Category = category;
            Cover = cover;
        }
    }
}
