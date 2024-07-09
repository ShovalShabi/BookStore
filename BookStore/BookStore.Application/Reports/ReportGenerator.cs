using Bookstore.Domain.Entities;
using System.Text;

public class ReportGenerator
{
    public string GenerateHtmlReport(IEnumerable<Book> books)
    {
        var htmlBuilder = new StringBuilder();
        htmlBuilder.Append("<html><body><h1>Bookstore Report</h1><table border='1'>");
        htmlBuilder.Append("<tr><th>ISBN</th><th>Title</th><th>Authors</th><th>Year</th><th>Price</th><th>Category</th><th>Cover</th></tr>");

        foreach (var book in books)
        {
            htmlBuilder.Append("<tr>");
            htmlBuilder.Append($"<td>{book.Isbn}</td>");
            htmlBuilder.Append($"<td>{book.Title}</td>");
            htmlBuilder.Append($"<td>{string.Join(", ", book.Authors)}</td>");
            htmlBuilder.Append($"<td>{book.Year}</td>");
            htmlBuilder.Append($"<td>{book.Price:C}</td>");
            htmlBuilder.Append($"<td>{book.Category}</td>");
            htmlBuilder.Append($"<td>{book.Cover}</td>");
            htmlBuilder.Append("</tr>");
        }

        htmlBuilder.Append("</table></body></html>");
        return htmlBuilder.ToString();
    }
}
