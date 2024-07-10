using System;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class XmlDataContext
{
    private readonly string _filePath;
    private readonly ILogger<XmlDataContext> _logger;

    public XmlDataContext(IConfiguration configuration, ILogger<XmlDataContext> logger)
    {
        _filePath = configuration.GetSection("DataConfiguration:FilePath").Value;
        _logger = logger;
    }

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
