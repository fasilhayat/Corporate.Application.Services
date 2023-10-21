using System.Text.Json;
namespace Corporate.Application.Services.Infrastructure;

public sealed class ServiceFactory<TService> : IServiceFactory
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<TService> _logger;
    private readonly IConfiguration _configuration;
    private readonly string _baseAddress = "https://openlibrary.org/api/books/?bibkeys=ISBN:9781492092391&format=json";

    public ServiceFactory(HttpClient httpClient, ILogger<TService> logger, IConfiguration configuration) => (_httpClient, _logger, _configuration) = (httpClient, logger, configuration);

    public async Task<TResult> GetResultAsync<TResult>(IEnumerable<KeyValuePair<string, string>> parameters) where TResult : class, new()
    {
        //var restUrl = parameters.Select(kvp => $@"{kvp.Key}={kvp.Value}");
        //var fullUrl = $"{_baseAddress}?{ restUrl}";
        var fullUrl = $"{_baseAddress}";
        //var config = _configuration.GetSection(typeof(TService).Name);

        try
        {
            // Make HTTP GET request
            // Parse JSON response deserialize into Todo type
            var result = await _httpClient.GetFromJsonAsync<JsonContent>(fullUrl, new JsonSerializerOptions(JsonSerializerDefaults.Web));
            var a = result?.ToString();
            
            //return result ?? new TResult();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error getting something fun to say: {Error}", ex);
        }

        return new TResult();
    }

    public void Dispose() => _httpClient.Dispose();
}