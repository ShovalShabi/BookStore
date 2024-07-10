using Bookstore.Domain.Entities;

namespace Bookstore.Domain.Repositories
{
    public interface IBookRepository
    {
        // Retrieves a book by its ISBN.
        Book GetByIsbn(string isbn);

        // Adds a new book record.
        void Add(Book book);

        // Updates an existing book record identified by ISBN.
        void Update(string isbn, Book book);

        // Deletes a book record by its ISBN.
        void Delete(string isbn);

        // Retrieves all book records.
        IEnumerable<Book> GetAll();
    }
}
