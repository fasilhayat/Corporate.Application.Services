using System.Text.Json;

namespace Corporate.Application.Services.Infrastructure;

public sealed class ServiceFactory<TService> : IServiceFactory<TService>
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly JsonSerializerOptions _options;
    private readonly ILogger<TService> _logger;

    public async Task<TResult?> Execute<TResult>(string parameters) where TResult : class, new()
    {
        return await GetDataWithHttpClientFactory<TResult>(parameters);
    }

    public ServiceFactory(IHttpClientFactory httpClientFactory, ILogger<TService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    private async Task<TResult?> GetDataWithHttpClientFactory<TResult>(string parameters) where TResult : class, new()
    {
        var httpClient = _httpClientFactory.CreateClient($"{typeof(TService).Name}Client");
        var uri = new Uri($"{httpClient.BaseAddress}{parameters}");
        using var response = await httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);

        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();

        // TODO: fix JsonDocument to custom object type
        var result = await JsonSerializer.DeserializeAsync<JsonDocument>(stream, _options);
        _logger.LogInformation(result!.RootElement.ToString());
        return null;
    }
}