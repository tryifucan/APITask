using Microsoft.Extensions.Configuration;


namespace APITask_Framework.Config
{
    public static class ConfigurationReader
    {
        private static readonly AppSettings _settings;

        static ConfigurationReader()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _settings = config.Get<AppSettings>();
        }

        public static string BaseUrl => _settings.BaseUrl;
        public static string ApiKey => _settings.ApiKey;
        public static int Timeout => _settings.Timeout;
        public static string ApiKeyHeader => _settings.ApiKeyHeader;

        public static AppSettings GetAppSettings() => _settings;
    }
}
