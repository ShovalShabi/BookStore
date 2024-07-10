using Bookstore.Application.Interfaces;
using Bookstore.Domain.Repositories;


// Create a new WebApplication instance with the provided command-line arguments.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();          // Registers controller services for handling HTTP requests.
builder.Services.AddEndpointsApiExplorer(); // Registers services for API endpoint exploration.
builder.Services.AddSwaggerGen();           // Configures Swagger generation for API documentation.

// Register services in the dependency injection (DI) container.
builder.Services.AddScoped<XmlDataContext>();                  // Registers XmlDataContext as a scoped service for XML data management.
builder.Services.AddScoped<ReportGenerator>();                 // Registers ReportGenerator as a scoped service for generating reports.
builder.Services.AddScoped<IBookRepository, BookRepository>(); // Registers BookRepository as the implementation for IBookRepository.
builder.Services.AddScoped<IBookService, BookService>();       // Registers BookService as the implementation for IBookService.

// Configure logging settings.
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging")); // Loads logging configuration settings.
builder.Logging.AddConsole();                                                  // Adds a console logger to log messages.

// Build the application using the configured services and middleware.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable Swagger middleware to generate Swagger JSON endpoint.
    app.UseSwagger();

    // Enable Swagger UI middleware to serve Swagger UI at /swagger.
    app.UseSwaggerUI();
}

// Enable authorization middleware.
app.UseAuthorization();

// Map controllers to handle incoming HTTP requests.
app.MapControllers();

// Start listening for incoming requests.
app.Run();
