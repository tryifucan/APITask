namespace APITask_Framework.Config
{
    public class AppSettings
    {
        public required string BaseUrl { get; set; }
        public required string ApiKey { get; set; }
        public int Timeout { get; set; }
        public required string ApiKeyHeader { get; set; }
    }
}
