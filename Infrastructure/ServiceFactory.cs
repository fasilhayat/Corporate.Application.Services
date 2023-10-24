using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Corporate.Application.Services.Infrastructure;

public sealed class ServiceFactory<TService> : IServiceFactory<TService>
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<TService> _logger;
    private readonly IConfiguration _configuration;
    private readonly JsonSerializerOptions _options;

    public ServiceFactory(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<TService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _logger = logger;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<TResult?> Execute<TResult>(IEnumerable<KeyValuePair<string, string>> parameters) where TResult : class, new()
    {
        //TODO: Read configuration
        var settings = _configuration.GetSection($"{typeof(TService).Name}Config");
        var querystring = $"/?{string.Join("&", parameters.Select(x => $"{x.Key}={x.Value}"))}";
        var httpClient = _httpClientFactory.CreateClient($"{typeof(TService).Name}Client");
        return await GetData<TResult>(httpClient, querystring);
    }

    public async Task<TResult?> Execute<TResult>(string querystring) where TResult : class, new()
    {
        //TODO: Read configuration
        var settings = _configuration.GetSection($"{typeof(TService).Name}Config");
        var httpClient = _httpClientFactory.CreateClient($"{typeof(TService).Name}Client");
        return await GetData<TResult>(httpClient, querystring);
    }

    public async Task<TResult?> Execute<TResult>(JsonObject json) where TResult : class, new()
    {
        var httpClient = _httpClientFactory.CreateClient($"{typeof(TService).Name}Client");
        return await PostData<TResult>(httpClient, json);
    }

    private async Task<TResult?> GetData<TResult>(HttpClient client, string parameters) where TResult : class, new()
    {
        var uri = new Uri($"{client.BaseAddress}{parameters}");
        using var response = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
        
        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<TResult>(stream, _options);
        return result;
    }

    private async Task<TResult?> PostData<TResult>(HttpClient client, JsonObject json) where TResult : class, new()
    {
        var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
        using var response = await client.PostAsync(client.BaseAddress, content);
        
        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<TResult>(stream, _options);
        return result;
    }
}