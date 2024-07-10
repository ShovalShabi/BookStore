using System.Collections.Generic;
using Bookstore.Application.DTO;

namespace Bookstore.Application.Interfaces
{
    /// <summary>
    /// Provides methods for managing book records.
    /// </summary>
    public interface IBookService
    {
        /// <summary>
        /// Retrieves a book by its ISBN.
        /// </summary>
        /// <param name="isbn">The ISBN of the book.</param>
        /// <returns>A <see cref="BookDTO"/> representing the book.</returns>
        BookDTO GetBookByIsbn(string isbn);

        /// <summary>
        /// Adds a new book record.
        /// </summary>
        /// <param name="bookDto">The data transfer object containing book details.</param>
        /// <returns>A <see cref="BookDTO"/> representing the added book.</returns>
        BookDTO AddBook(BookDTO bookDto);

        /// <summary>
        /// Edits an existing book record identified by ISBN.
        /// </summary>
        /// <param name="isbn">The ISBN of the book to edit.</param>
        /// <param name="bookDto">The data transfer object containing updated book details.</param>
        void EditBook(string isbn, BookDTO bookDto);

        /// <summary>
        /// Deletes a book record by its ISBN.
        /// </summary>
        /// <param name="isbn">The ISBN of the book to delete.</param>
        void DeleteBook(string isbn);

        /// <summary>
        /// Generates an HTML report of all books.
        /// </summary>
        /// <returns>A string containing the HTML report.</returns>
        string GenerateReport();
    }
}
