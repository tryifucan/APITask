using APITask_Framework.Config;
using RestSharp;
using System;

namespace APITask_Framework
{
    public class ApiClient
    {
        private readonly RestClient _client;
        private readonly string _apiKey;
        private readonly string _apiKeyHeader;

        public ApiClient()
        {
            var baseUrl = ConfigurationReader.BaseUrl;
            var timeout = ConfigurationReader.Timeout;
            _apiKey = ConfigurationReader.ApiKey;
            _apiKeyHeader = ConfigurationReader.ApiKeyHeader;

            var options = new RestClientOptions(baseUrl)
            {
                ThrowOnAnyError = false,
                Timeout = TimeSpan.FromMilliseconds(timeout)
            };

            _client = new RestClient(options);
        }

        public RestResponse Execute(RestRequest request)
        {
            AddDefaultHeaders(request);
            return _client.Execute(request);
        }

        public RestResponse<T> Execute<T>(RestRequest request) where T : new()
        {
            AddDefaultHeaders(request);
            return _client.Execute<T>(request);
        }

        private void AddDefaultHeaders(RestRequest request)
        {
            request.AddOrUpdateHeader(_apiKeyHeader, _apiKey);
        }
    }
}
