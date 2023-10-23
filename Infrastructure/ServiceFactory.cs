using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Corporate.Application.Services.Infrastructure;

public sealed class ServiceFactory<TService> : IServiceFactory<TService>
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<TService> _logger;
    private readonly JsonSerializerOptions _options;
    private readonly IConfiguration _configuration;

    public ServiceFactory(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<TService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _logger = logger;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<TResult?> Execute<TResult>(IEnumerable<KeyValuePair<string, string>> parameters) where TResult : class, new()
    {
        var querystring = $"/?{string.Join("&", parameters.Select(x => $"{x.Key}={x.Value}"))}";
        return await GetData<TResult>(querystring);
    }

    public async Task<TResult?> Execute<TResult>(string querystring) where TResult : class, new()
    {
        return await GetData<TResult>(querystring);
    }

    public async Task<TResult?> Execute<TResult>(JsonObject json) where TResult : class, new()
    {
        return await PostData<TResult>(json);
    }

    private async Task<TResult?> GetData<TResult>(string parameters) where TResult : class, new()
    {
        var httpClient = _httpClientFactory.CreateClient($"{typeof(TService).Name}Client");
        
        var uri = new Uri($"{httpClient.BaseAddress}{parameters}");
        using var response = await httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
        
        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();
        
        var result = await JsonSerializer.DeserializeAsync<TResult>(stream, _options);
        return result;
    }

    private async Task<TResult?> PostData<TResult>(JsonObject json) where TResult : class, new()
    {
        var httpClient = _httpClientFactory.CreateClient($"{typeof(TService).Name}Client");
        
        var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
        using var response = await httpClient.PostAsync(httpClient.BaseAddress, content);
        
        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();

        var result = await JsonSerializer.DeserializeAsync<TResult>(stream, _options);
        return result;
    }

    private void ReadConfiguration()
    {
        var settings = _configuration.GetSection(typeof(TService).Name);
    }
}