using System.Xml.Linq;


/// <summary>
/// Represents a context for interacting with XML data based on a file path.
/// </summary>
public class XmlDataContext
{
    private readonly string _filePath;
    private readonly ILogger<XmlDataContext> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="XmlDataContext"/> class.
    /// </summary>
    /// <param name="configuration">The <see cref="IConfiguration"/> instance to retrieve configuration settings.</param>
    /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> instance to log messages.</param>
    public XmlDataContext(IConfiguration configuration, ILogger<XmlDataContext> logger)
    {
        _filePath = configuration.GetSection("DataConfiguration:FilePath").Value;
        _logger = logger;
    }

    /// <summary>
    /// Loads an XML document from the configured file path.
    /// </summary>
    /// <returns>The loaded <see cref="XDocument"/>.</returns>
    public XDocument LoadXml()
    {
        try
        {
            _logger.LogInformation($"Loading XML from file: {_filePath}");
            return XDocument.Load(_filePath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error loading XML from file: {_filePath}");
            throw;
        }
    }

    /// <summary>
    /// Saves an XML document to the configured file path.
    /// </summary>
    /// <param name="document">The <see cref="XDocument"/> to save.</param>
    public void SaveXml(XDocument document)
    {
        try
        {
            _logger.LogInformation($"Saving XML to file: {_filePath}");
            document.Save(_filePath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error saving XML to file: {_filePath}");
            throw;
        }
    }
}
