using System.Text.Json;
namespace Corporate.Application.Services.Infrastructure;

public sealed class ServiceFactory<TService> : IServiceFactory<TService>
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly JsonSerializerOptions _options;
    private readonly ILogger<TService> _logger;
    //private readonly string _baseAddress = "https://openlibrary.org/api/books/?bibkeys=ISBN:9781492092391&format=json";
    private readonly string _baseAddress = "https://restcountries.com/v3.1/capital/copenhagen";

    public async Task<TResult?> Execute<TResult>() where TResult : class, new()
    {
        return await GetDataWithHttpClientFactory<TResult>();
    }

    public ServiceFactory(IHttpClientFactory httpClientFactory, ILogger<TService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    private async Task<TResult?> GetDataWithHttpClientFactory<TResult>() where TResult : class, new()
    {
        var httpClient = _httpClientFactory.CreateClient();
        using (var response = await httpClient.GetAsync(_baseAddress, HttpCompletionOption.ResponseHeadersRead))
        {
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            // TODO: fix JsonDocument to custom object type
            var result = await JsonSerializer.DeserializeAsync<JsonDocument>(stream, _options);
            return null;
        }
    }
}