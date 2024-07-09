using Bookstore.Application.Interfaces;
using Bookstore.Domain.Repositories;
using Microsoft.Win32;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services
builder.Services.AddScoped<XmlDataContext>(); // Register XmlDataContext
builder.Services.AddScoped<ReportGenerator>();
builder.Services.AddScoped<IBookRepository, BookRepository>(); // Register BookRepository implementation
builder.Services.AddScoped<IBookService, BookService>(); // Register BookService implementation

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();