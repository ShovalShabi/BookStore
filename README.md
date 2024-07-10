# Bookstore Application

This repository contains a .NET application for managing books. It includes functionality to add, edit, delete books, and generate reports.

## Running the Program

### Using Visual Studio 2022

1. **Prerequisites:**
   - Install [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0).
   - Install [Visual Studio 2022](https://visualstudio.microsoft.com/vs/).

2. **Setup:**
   - Clone the repository to your local machine.

3. **Running the Application:**
   - Open the solution (`*.sln`) file in Visual Studio 2022.
   - Set the startup project to the main project containing `Program.cs`.
   - Press F5 or click on the "Start" button to build and run the application.

### Using Docker

1. **Prerequisites:**
   - Install [Docker Desktop](https://www.docker.com/products/docker-desktop).

2. **Building and Running the Docker Container:**
   - Open a terminal.
   - Navigate to the root directory of the project containing the Dockerfile.
   - Build the Docker image:
     ```
     docker build -t bookstore-app .
     ```
   - Run the Docker container:
     ```
     docker run -d -p 8080:80 --name bookstore-container bookstore-app
     ```
   - The application will be accessible at `http://localhost:8080`.

## Running Tests

### Using Visual Studio 2022

1. **Running Unit Tests:**
   - In Visual Studio, open the Test Explorer (`Test` -> `Test Explorer`).
   - Click `Run All` to execute all unit tests.

2. **Running Integration Tests:**
   - Integration tests might require additional setup such as a test database or external services.
   - Ensure the necessary dependencies are configured for integration testing.
   - Run integration tests similarly in the Test Explorer.

### Using Terminal

1. **Running Unit Tests:**
   - Navigate to the directory containing the unit tests project.
   - Execute the following command:
     ```
     dotnet test
     ```

2. **Running Integration Tests:**
   - Navigate to the directory containing the integration tests project.
   - Execute the following command:
     ```
     dotnet test
     ```
   - Ensure any necessary configurations or environment variables are set before running.

## Controller API

The application exposes several endpoints through the controller API. Here are some examples:

- **GET `/api/book/{isbn}`**
  - Retrieves a book by its ISBN.
  - Example: `http://localhost:8080/api/book/978-3-16-148410-0`

- **POST `/api/book`**
  - Adds a new book.
  - Example payload:
    ```json
    {
      "isbn": "978-3-16-148410-0",
      "title": "Sample Book",
      "authors": "John Doe, Jane Smith",
      "year": 2024,
      "price": 29.99,
      "category": "Fiction",
      "cover": "hardcover"
    }
    ```

- **PUT `/api/book/{isbn}`**
  - Updates an existing book by its ISBN.
  - Example payload:
    ```json
    {
      "title": "Updated Book Title",
      "authors": "Jane Smith",
      "year": 2025,
      "price": 34.99,
      "category": "Fiction",
      "cover": "paperback"
    }
    ```

- **DELETE `/api/book/{isbn}`**
  - Deletes a book by its ISBN.
  - Example: `http://localhost:8080/api/book/978-3-16-148410-0`

- **GET `/api/book/report`**
  - Generates an HTML report of all books.

Make sure to adjust paths (`/api/book` and `/api/book/report`) and payload data according to your application's requirements and endpoints structure.

**P.S**
For better understading of the HTML element you can enter into [this url](https://www.freeformatter.com/html-formatter.html#before-output) and paste the server's response.
For enhancing the visualization of the HTML element you can enter into [this url](https://html.onlineviewer.net/) and paste the server's response/the formatted text from the link from above this line.
