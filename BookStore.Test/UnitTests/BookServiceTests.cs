using Moq;
using Microsoft.Extensions.Logging;
using Bookstore.Domain.Repositories;
using Bookstore.Application.Interfaces;
using Bookstore.Domain.Entities;
using BookStore.BookStore.Application.Utils;
using Bookstore.Application.DTO;
using Microsoft.AspNetCore.Http;

namespace BookStore.Test.UnitTests
{
    /// <summary>
    /// Unit tests for the BookService class.
    /// </summary>
    public class BookServiceTests
    {
        private Mock<IBookRepository> _bookRepositoryMock;
        private Mock<ILogger<BookService>> _loggerMock;
        private IBookService _bookService;

        /// <summary>
        /// Initializes the mocks and the BookService instance.
        /// </summary>
        public BookServiceTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _loggerMock = new Mock<ILogger<BookService>>();
            _bookService = new BookService(_bookRepositoryMock.Object, new ReportGenerator(), _loggerMock.Object);
        }

        /// <summary>
        /// Tests that GetBookByIsbn throws an exception for null or empty ISBN.
        /// </summary>
        [Fact]
        public void GetBookByIsbn_NullOrEmptyIsbn()
        {
            // Act, Assert
            var exception = Assert.Throws<BookServiceException>(() => _bookService.GetBookByIsbn(null));
            Assert.Equal("The book does not exist.", exception.Message);

            exception = Assert.Throws<BookServiceException>(() => _bookService.GetBookByIsbn(""));
            Assert.Equal("The book does not exist.", exception.Message);

            _loggerMock.Verify(
                x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.Is<It.IsAnyType>((v, t) => true), It.IsAny<Exception>(), It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Exactly(2));
        }

        /// <summary>
        /// Tests that GetBookByIsbn retrieves the book when it exists.
        /// </summary>
        [Fact]
        public void GetBookByIsbn_ShouldRetrieveBook_WhenExists()
        {
            // Given
            var isbn = "1234567890";
            var book = new Book { Isbn = isbn, Title = "Test Book", Authors = new List<string> { "Author One", "Author Two" }, Year = 2021, Price = 19.99m, Category = "Fiction", Cover = "Cover Image" };
            _bookRepositoryMock.Setup(repo => repo.GetByIsbn(isbn)).Returns(book);

            // When
            var result = _bookService.GetBookByIsbn(isbn);

            // Then
            Assert.NotNull(result);
            Assert.Equal(isbn, result.Isbn);
            _loggerMock.Verify(
                x => x.Log(LogLevel.Information, It.IsAny<EventId>(), It.Is<It.IsAnyType>((v, t) => true), It.IsAny<Exception>(), It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }

        /// <summary>
        /// Tests that GetBookByIsbn throws an exception when the book is not found.
        /// </summary>
        [Fact]
        public void GetBookByIsbn_ShouldThrowException_WhenNotFound()
        {
            // Given
            var isbn = "1234567890";
            _bookRepositoryMock.Setup(repo => repo.GetByIsbn(isbn)).Returns((Book)null);

            // When, Then
            var exception = Assert.Throws<BookServiceException>(() => _bookService.GetBookByIsbn(isbn));
            Assert.Equal("The book does not exist.", exception.Message);
            _loggerMock.Verify(
                x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.Is<It.IsAnyType>((v, t) => true), It.IsAny<Exception>(), It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }

        /// <summary>
        /// Tests that AddBook adds a book when it does not already exist.
        /// </summary>
        [Fact]
        public void AddBook_ShouldAddBook_WhenNotExists()
        {
            // Given
            var bookDto = new BookDTO
            {
                Isbn = "1234567890",
                Title = "Test Book",
                Authors = "Author One, Author Two",
                Year = 2021,
                Price = 19.99m,
                Category = "Fiction",
                Cover = "Cover Image"
            };

            _bookRepositoryMock.Setup(repo => repo.GetByIsbn(bookDto.Isbn)).Returns((Book)null);

            // When
            var result = _bookService.AddBook(bookDto);

            // Then
            Assert.NotNull(result);
            Assert.Equal(bookDto.Isbn, result.Isbn);
            _bookRepositoryMock.Verify(repo => repo.Add(It.IsAny<Book>()), Times.Once);
            _loggerMock.Verify(
                x => x.Log(LogLevel.Information, It.IsAny<EventId>(), It.Is<It.IsAnyType>((v, t) => true), It.IsAny<Exception>(), It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }

        /// <summary>
        /// Tests that AddBook throws an exception for an invalid ISBN length.
        /// </summary>
        [Fact]
        public void AddBook_InvalidIsbnLength()
        {
            // Arrange
            var bookDto = new BookDTO
            {
                Isbn = "123456789", // Invalid length (9 characters)
                Title = "Test Book",
                Authors = "Author One, Author Two",
                Year = 2021,
                Price = 19.99m,
                Category = "Fiction",
                Cover = "Cover Image"
            };

            // Act, Assert
            var exception = Assert.Throws<BookServiceException>(() => _bookService.AddBook(bookDto));
            Assert.Equal("ISBN code is not valid.", exception.Message);

            _bookRepositoryMock.Verify(repo => repo.Add(It.IsAny<Book>()), Times.Never);
            _loggerMock.Verify(
                x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.Is<It.IsAnyType>((v, t) => true), It.IsAny<Exception>(), It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }

        /// <summary>
        /// Tests that AddBook throws an exception for a null or empty ISBN.
        /// </summary>
        [Fact]
        public void AddBook_NullOrEmptyIsbn()
        {
            // Arrange
            var bookDto = new BookDTO
            {
                Isbn = "", // Empty ISBN
                Title = "Test Book",
                Authors = "Author One, Author Two",
                Year = 2021,
                Price = 19.99m,
                Category = "Fiction",
                Cover = "Cover Image"
            };

            // Act, Assert
            var exception = Assert.Throws<BookServiceException>(() => _bookService.AddBook(bookDto));
            Assert.Equal("ISBN code is not valid.", exception.Message);

            _bookRepositoryMock.Verify(repo => repo.Add(It.IsAny<Book>()), Times.Never);
            _loggerMock.Verify(
                x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.Is<It.IsAnyType>((v, t) => true), It.IsAny<Exception>(), It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }

        /// <summary>
        /// Tests that AddBook returns the existing book if it already exists.
        /// </summary>
        [Fact]
        public void AddBook_ShouldReturnExistingBook_WhenAlreadyExists()
        {
            // Given
            var bookDto = new BookDTO
            {
                Isbn = "1234567890",
                Title = "Test Book",
                Authors = "Author One, Author Two",
                Year = 2021,
                Price = 19.99m,
                Category = "Fiction",
                Cover = "Cover Image"
            };

            var existingBook = new Book
            {
                Isbn = bookDto.Isbn,
                Title = bookDto.Title,
                Authors = new List<string> { "Author One", "Author Two" },
                Year = bookDto.Year,
                Price = bookDto.Price,
                Category = bookDto.Category,
                Cover = bookDto.Cover
            };

            _bookRepositoryMock.Setup(repo => repo.GetByIsbn(bookDto.Isbn)).Returns(existingBook);

            // When
            var result = _bookService.AddBook(bookDto);

            // Then
            Assert.NotNull(result);
            Assert.Equal(existingBook.Isbn, result.Isbn); // Ensure the returned book has the correct ISBN

            // Verify that repository methods were not called
            _bookRepositoryMock.Verify(repo => repo.Add(It.IsAny<Book>()), Times.Never);

            // Verify logging behavior
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => true),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.Once); // Verify logging occurred once
        }

        /// <summary>
        /// Tests that EditBook updates the book when it exists.
        /// </summary>
        [Fact]
        public void EditBook_ShouldUpdateBook_WhenExists()
        {
            // Given
            var isbn = "1234567890";
            var bookDto = new BookDTO
            {
                Isbn = isbn,
                Title = "Updated Title",
                Authors = "Updated Author",
                Year = 2022,
                Price = 29.99m,
                Category = "Non-Fiction",
                Cover = "Updated Cover Image"
            };

            var existingBook = new Book
            {
                Isbn = isbn,
                Title = "Original Title",
                Authors = new List<string> { "Original Author" },
                Year = 2021,
                Price = 19.99m,
                Category = "Fiction",
                Cover = "Original Cover Image"
            };

            _bookRepositoryMock.Setup(repo => repo.GetByIsbn(isbn)).Returns(existingBook);

            // When
            _bookService.EditBook(isbn, bookDto);

            // Then
            _bookRepositoryMock.Verify(repo => repo.Update(isbn, It.IsAny<Book>()), Times.Once);
            _loggerMock.Verify(
                x => x.Log(LogLevel.Information, It.IsAny<EventId>(), It.Is<It.IsAnyType>((v, t) => true), It.IsAny<Exception>(), It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }

        /// <summary>
        /// Tests that EditBook throws an exception when the book does not exist.
        /// </summary>
        [Fact]
        public void EditBook_ShouldThrowException_WhenNotExists()
        {
            // Given
            var isbn = "1234567890";
            _bookRepositoryMock.Setup(repo => repo.GetByIsbn(isbn)).Returns((Book)null);

            var bookDto = new BookDTO
            {
                Isbn = isbn,
                Title = "Updated Title",
                Authors = "Updated Author",
                Year = 2022,
                Price = 29.99m,
                Category = "Non-Fiction",
                Cover = "Updated Cover Image"
            };

            // When, Then
            var exception = Assert.Throws<BookServiceException>(() => _bookService.EditBook(isbn, bookDto));
            Assert.Equal("The book does not exist.", exception.Message);
            _bookRepositoryMock.Verify(repo => repo.Update(isbn, It.IsAny<Book>()), Times.Never);
            _loggerMock.Verify(
                x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.Is<It.IsAnyType>((v, t) => true), It.IsAny<Exception>(), It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }

        /// <summary>
        /// Tests that EditBook throws an exception for a null or empty ISBN.
        /// </summary>
        [Fact]
        public void EditBook_NullOrEmptyIsbn_ThrowsException()
        {
            // Arrange
            var isbn = ""; // Empty ISBN
            var bookDto = new BookDTO
            {
                Title = "Updated Test Book",
                Authors = "Author One, Author Two",
                Year = 2022,
                Price = 29.99m,
                Category = "Non-fiction",
                Cover = "Updated Cover Image"
            };

            _bookRepositoryMock.Setup(repo => repo.GetByIsbn(isbn)).Returns((Book)null);

            // Act, Assert
            var exception = Assert.Throws<BookServiceException>(() => _bookService.EditBook(isbn, bookDto));
            Assert.Equal("The book does not exist.", exception.Message);

            _bookRepositoryMock.Verify(repo => repo.Update(It.IsAny<string>(), It.IsAny<Book>()), Times.Never);
            _loggerMock.Verify(
                x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.Is<It.IsAnyType>((v, t) => true), It.IsAny<Exception>(), It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }

        /// <summary>
        /// Tests that DeleteBook deletes the book.
        /// </summary>
        [Fact]
        public void DeleteBook_ShouldDeleteBook()
        {
            // Given
            var isbn = "1234567890";

            // When
            _bookService.DeleteBook(isbn);

            // Then
            _bookRepositoryMock.Verify(repo => repo.Delete(isbn), Times.Once);
            _loggerMock.Verify(
                x => x.Log(LogLevel.Information, It.IsAny<EventId>(), It.Is<It.IsAnyType>((v, t) => true), It.IsAny<Exception>(), It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }

        /// <summary>
        /// Tests that GenerateReport generates a report.
        /// </summary>
        [Fact]
        public void GenerateReport_ShouldGenerateReport()
        {
            // Given
            var books = new List<Book> {
                new Book { Isbn = "1234567890", Title = "Book 1", Authors = new List<string> { "Author One" }, Year = 2021, Price = 19.99m, Category = "Fiction", Cover = "Cover Image 1" },
                new Book { Isbn = "0987654321", Title = "Book 2", Authors = new List<string> { "Author Two" }, Year = 2022, Price = 29.99m, Category = "Non-Fiction", Cover = "Cover Image 2" }
            };

            _bookRepositoryMock.Setup(repo => repo.GetAll()).Returns(books);

            // When
            var result = _bookService.GenerateReport();

            // Then
            Assert.NotNull(result);
            Assert.Contains("<html>", result); // Example check for HTML content
            _loggerMock.Verify(
                x => x.Log(LogLevel.Information, It.IsAny<EventId>(), It.Is<It.IsAnyType>((v, t) => true), It.IsAny<Exception>(), It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }

        /// <summary>
        /// Cleanup method executed after each test.
        /// </summary>
        public void AfterEach()
        {
            _bookRepositoryMock.Reset();
            _loggerMock.Invocations.Clear();
        }
    }
}
