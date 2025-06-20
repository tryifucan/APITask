using Microsoft.Extensions.Configuration;

namespace APITask_Framework.Config
{
    public static class ConfigurationManager
    {
        private static readonly IConfigurationRoot config;

        static ConfigurationManager()
        {
            config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public static string Get(string key)
        {
            return config[key];
        }
    }
}
