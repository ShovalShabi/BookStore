using Microsoft.Extensions.Configuration;

namespace Bookstore.Infrastructure.Configurations
{
    public class DataConfiguration
    {
        public string FilePath { get; set; }

        public DataConfiguration(IConfiguration configuration)
        {
            // Load the file path from the configuration
            FilePath = configuration.GetSection("DataConfiguration:FilePath").Value;
        }
    }
}
