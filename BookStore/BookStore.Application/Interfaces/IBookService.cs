using System.Collections.Generic;
using Bookstore.Application.DTO;

namespace Bookstore.Application.Interfaces
{
    public interface IBookService
    {
        // Retrieves a book by its ISBN.
        BookDTO GetBookByIsbn(string isbn);

        // Adds a new book record.
        BookDTO AddBook(BookDTO bookDto);

        // Edits an existing book record identified by ISBN.
        void EditBook(string isbn, BookDTO bookDto);

        // Deletes a book record by its ISBN.
        void DeleteBook(string isbn);

        // Generates an HTML report of all books.
        string GenerateReport();
    }
}
