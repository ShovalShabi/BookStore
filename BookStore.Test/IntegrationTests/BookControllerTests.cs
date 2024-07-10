using System.Net;
using System.Net.Http.Json;
using Bookstore.Application.DTO;
using FluentAssertions;


namespace BookStore.Test.IntegrationTests
{
    public class BookControllerTests : IClassFixture<BookStoreWbApplicationFactory>
    {
        [Fact]
        public async Task GetBook_ByValidIsbn_Returns_Ok()
        {
            // Arrange
            var isbn = "1234567890";
            using var factory = new BookStoreWbApplicationFactory();
            using var client = factory.CreateClient();

            await AddBookToXMLFile(isbn, client);

            try
            {
                // Act
                var response = await client.GetAsync($"/api/book/{isbn}");

                // Assert
                response.EnsureSuccessStatusCode(); // Status Code 200-299
                var book = await response.Content.ReadFromJsonAsync<BookDTO>();

                Assert.Equal(isbn, book?.Isbn);
            }
            finally
            {
                // Clean up
                await DeleteBookFromXMLFile(isbn, client);
            }
        }

        [Fact]
        public async Task GetBook_ByInvalidIsbn_Returns_NotFound()
        {
            // Arrange
            var isbn = "9999999999"; // Assuming this ISBN does not exist
            using var factory = new BookStoreWbApplicationFactory();
            using var client = factory.CreateClient();

            await DeleteBookFromXMLFile(isbn, client); // Ensure it's deleted if somehow exists

            try
            {
                // Act
                var response = await client.GetAsync($"/api/book/{isbn}");

                // Assert
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
            finally
            {
                // Clean up
                await DeleteBookFromXMLFile(isbn, client);
            }
        }

        [Fact]
        public async Task AddBook_WithValidData_Returns_Created()
        {
            // Arrange
            var bookDto = new BookDTO
            {
                Isbn = "9876543210",
                Title = "Test Book",
                Authors = "Author One, Author Two",
                Year = 2022,
                Price = 29.99m,
                Category = "Fiction",
                Cover = "Cover Image"
            };

            using var factory = new BookStoreWbApplicationFactory();
            using var client = factory.CreateClient();

            try
            {
                // Act
                var response = await client.PostAsJsonAsync("/api/book", bookDto);

                // Assert
                response.EnsureSuccessStatusCode(); // Status Code 200-299
                var createdBook = await response.Content.ReadFromJsonAsync<BookDTO>();

                Assert.Equal(bookDto.Isbn, createdBook?.Isbn);
            }
            finally
            {
                // Clean up
                await DeleteBookFromXMLFile(bookDto.Isbn, client);
            }
        }

        [Fact]
        public async Task AddBook_WithInvalidIsbn_Returns_BadRequest()
        {
            // Arrange
            var bookDto = new BookDTO
            {
                Isbn = "123", // Invalid ISBN
                Title = "Test Book",
                Authors = "Author One, Author Two",
                Year = 2022,
                Price = 29.99m,
                Category = "Fiction",
                Cover = "Cover Image"
            };

            using var factory = new BookStoreWbApplicationFactory();
            using var client = factory.CreateClient();

            try
            {
                // Act
                var response = await client.PostAsJsonAsync("/api/book", bookDto);

                // Assert
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
            finally
            {
                // Clean up (no need since book is not added)
            }
        }

        [Fact]
        public async Task EditBook_WithValidIsbn_Returns_NoContent()
        {
            // Arrange
            var isbn = "9876543210"; // Assuming this ISBN exists
            var bookDto = new BookDTO
            {
                Isbn = isbn,
                Title = "Edited Title",
                Authors = "Edited Author",
                Year = 2023,
                Price = 39.99m,
                Category = "Non-Fiction",
                Cover = "Edited Cover Image"
            };

            using var factory = new BookStoreWbApplicationFactory();
            using var client = factory.CreateClient();

            await AddBookToXMLFile(isbn, client);

            try
            {
                // Act
                var response = await client.PutAsJsonAsync($"/api/book/{isbn}", bookDto);

                // Assert
                response.EnsureSuccessStatusCode(); // Status Code 200-299
            }
            finally
            {
                // Clean up
                await DeleteBookFromXMLFile(isbn, client);
            }
        }

        [Fact]
        public async Task EditBook_WithInvalidIsbn_Returns_BadRequest()
        {
            // Arrange
            var isbn = ""; // Invalid ISBN
            var bookDto = new BookDTO
            {
                Isbn = isbn,
                Title = "Edited Title",
                Authors = "Edited Author",
                Year = 2023,
                Price = 39.99m,
                Category = "Non-Fiction",
                Cover = "Edited Cover Image"
            };

            using var factory = new BookStoreWbApplicationFactory();
            using var client = factory.CreateClient();

            try
            {
                // Act
                var response = await client.PutAsJsonAsync($"/api/book/{isbn}", bookDto);

                // Assert
                Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
            }
            finally
            {
                // Clean up (no need since book is not updated)
            }
        }

        [Fact]
        public async Task DeleteBook_WithValidIsbn_Returns_NoContent()
        {
            // Arrange
            var isbn = "1234567890"; // Assuming this ISBN exists
            using var factory = new BookStoreWbApplicationFactory();
            using var client = factory.CreateClient();

            await AddBookToXMLFile(isbn, client);

            try
            {
                // Act
                var response = await client.DeleteAsync($"/api/book/{isbn}");

                // Assert
                response.EnsureSuccessStatusCode(); // Status Code 200-299
            }
            finally
            {
                // Clean up
                await DeleteBookFromXMLFile(isbn, client);
            }
        }

        [Fact]
        public async Task GetReport_Returns_ReportContent()
        {
            // Arrange
            // Assuming there are books in the database to generate a report

            using var factory = new BookStoreWbApplicationFactory();
            using var client = factory.CreateClient();

            try
            {
                // Act
                var response = await client.GetAsync("/api/Book/report");

                // Assert
                response.EnsureSuccessStatusCode(); // Status Code 200-299
                var content = await response.Content.ReadAsStringAsync();
                Assert.Contains("<html><body><h1>Bookstore Report</h1><table border='1'>", content); // Assuming the report content contains this phrase
            }
            finally
            {
                // Clean up (no need since no book is added or modified)
            }
        }

        private async Task AddBookToXMLFile(string isbn, HttpClient client)
        {
            var bookDto = new BookDTO
            {
                Isbn = isbn,
                Title = "Test Book",
                Authors = "Author One, Author Two",
                Year = 2022,
                Price = 29.99m,
                Category = "Fiction",
                Cover = "Cover Image"
            };

            try
            {
                var response = await client.PostAsJsonAsync("/api/book", bookDto);
                response.StatusCode.Should().Be(HttpStatusCode.Created);
            }
            catch (HttpRequestException ex)
            {
                // Log or handle the exception appropriately
                Console.WriteLine($"Error adding book: {ex.Message}");
                throw; // Rethrow the exception to indicate test failure
            }
        }

        private async Task DeleteBookFromXMLFile(string isbn, HttpClient client)
        {
            var response = await client.DeleteAsync($"/api/book/{isbn}");
            response.EnsureSuccessStatusCode();
        }
    }
}
