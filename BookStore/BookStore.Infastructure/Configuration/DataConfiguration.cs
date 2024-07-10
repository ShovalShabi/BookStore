using Microsoft.Extensions.Configuration;

namespace Bookstore.Infrastructure.Configurations
{
    /// <summary>
    /// Configuration class to load data-related configuration settings.
    /// </summary>
    public class DataConfiguration
    {
        /// <summary>
        /// Gets or sets the file path configured for data operations.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataConfiguration"/> class.
        /// </summary>
        /// <param name="configuration">The <see cref="IConfiguration"/> instance to retrieve configuration settings.</param>
        public DataConfiguration(IConfiguration configuration)
        {
            // Load the file path from the configuration
            FilePath = configuration.GetSection("DataConfiguration:FilePath").Value;
        }
    }
}
