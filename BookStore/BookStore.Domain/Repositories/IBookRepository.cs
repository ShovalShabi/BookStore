using Bookstore.Domain.Entities;

namespace Bookstore.Domain.Repositories
{
    /// <summary>
    /// Interface for interacting with book data storage.
    /// </summary>
    public interface IBookRepository
    {
        /// <summary>
        /// Retrieves a book by its ISBN.
        /// </summary>
        /// <param name="isbn">The ISBN of the book to retrieve.</param>
        /// <returns>The <see cref="Book"/> object corresponding to the provided ISBN, or null if not found.</returns>
        Book GetByIsbn(string isbn);

        /// <summary>
        /// Adds a new book record.
        /// </summary>
        /// <param name="book">The <see cref="Book"/> object representing the book to add.</param>
        void Add(Book book);

        /// <summary>
        /// Updates an existing book record identified by ISBN.
        /// </summary>
        /// <param name="isbn">The ISBN of the book to update.</param>
        /// <param name="book">The <see cref="Book"/> object containing updated information.</param>
        void Update(string isbn, Book book);

        /// <summary>
        /// Deletes a book record by its ISBN.
        /// </summary>
        /// <param name="isbn">The ISBN of the book to delete.</param>
        void Delete(string isbn);

        /// <summary>
        /// Retrieves all book records.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{Book}"/> containing all book records.</returns>
        IEnumerable<Book> GetAll();
    }
}
