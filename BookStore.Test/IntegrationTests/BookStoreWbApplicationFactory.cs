using Bookstore.Application.Interfaces;
using Bookstore.Domain.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BookStore.Test.IntegrationTests
{
    /// <summary>
    /// Custom WebApplicationFactory for integration testing the BookStore application.
    /// </summary>
    internal class BookStoreWbApplicationFactory : WebApplicationFactory<Program>
    {
        /// <summary>
        /// Configures the web host builder to use the test configuration and registers services.
        /// </summary>
        /// <param name="builder">The web host builder.</param>
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Configure the application to use the test settings
            builder.ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.Test.json", optional: false, reloadOnChange: true);
            });

            // Configure services for dependency injection
            builder.ConfigureServices(services =>
            {
                // Add controllers and API explorer
                services.AddControllers();
                services.AddEndpointsApiExplorer();
                services.AddSwaggerGen();

                // Register application services
                services.AddScoped<XmlDataContext>(); // Register XmlDataContext
                services.AddScoped<ReportGenerator>();
                services.AddScoped<IBookRepository, BookRepository>(); // Register BookRepository implementation
                services.AddScoped<IBookService, BookService>(); // Register BookService implementation

                // Configure logging
                services.AddLogging(logging =>
                {
                    logging.AddConsole();
                });
            });
        }
    }
}
