using Bookstore.Application.Interfaces;
using Bookstore.Domain.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BookStore.Test.IntegrationTests
{
    internal class BookStoreWbApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.Test.json", optional: false, reloadOnChange: true);
            });

            builder.ConfigureServices(services =>
            {
                // Add services from Program.cs
                services.AddControllers();
                services.AddEndpointsApiExplorer();
                services.AddSwaggerGen();

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
