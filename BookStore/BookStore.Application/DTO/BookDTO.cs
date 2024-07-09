namespace Bookstore.Application.DTO
{
    public class BookDto
    {
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; } // Comma-separated authors.
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Cover { get; set; } // Optional
    }
}
