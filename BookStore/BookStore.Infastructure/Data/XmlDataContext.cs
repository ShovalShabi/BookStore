using System.Xml.Linq;

public class XmlDataContext
{
    private readonly string _filePath;

    public XmlDataContext(IConfiguration configuration)
    {
        _filePath = configuration.GetSection("DataConfiguration:FilePath").Value;
    }

    public XDocument LoadXml()
    {
        return XDocument.Load(_filePath);
    }

    public void SaveXml(XDocument document)
    {
        document.Save(_filePath);
    }
}
