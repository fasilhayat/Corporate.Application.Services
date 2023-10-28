using Corporate.Application.Services.Config;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Corporate.Application.Services.Infrastructure;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TConfig"></typeparam>
public sealed class ServiceFactory<TService, TConfig> : IServiceFactory<TService> where TConfig : class
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<TService> _logger;
    private readonly IConfiguration _configuration;
    private readonly JsonSerializerOptions _options;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="httpClientFactory"></param>
    /// <param name="configuration"></param>
    /// <param name="logger"></param>
    public ServiceFactory(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<TService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _logger = logger;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public async Task<TResult?> Execute<TResult>(IEnumerable<KeyValuePair<string, string>> parameters) where TResult : class, new()
    {
        var querystring = $"/?{string.Join("&", parameters.Select(x => $"{x.Key}={x.Value}"))}";
        var httpClient = CreateHttpClient();
        return await GetData<TResult>(httpClient, querystring);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="querystring"></param>
    /// <returns></returns>
    public async Task<TResult?> Execute<TResult>(string querystring) where TResult : class, new()
    {
        var httpClient = CreateHttpClient();
        return await GetData<TResult>(httpClient, querystring);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="json"></param>
    /// <returns></returns>
    public async Task<TResult?> Execute<TResult>(JsonObject json) where TResult : class, new()
    {
        var httpClient = CreateHttpClient();
        return await PostData<TResult>(httpClient, json);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="client"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    private async Task<TResult?> GetData<TResult>(HttpClient client, string parameters) where TResult : class, new()
    {
        var uri = new Uri($"{client.BaseAddress}{parameters}");
        _logger.LogInformation($"Calling '{uri.AbsoluteUri}'");
        using var response = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
        
        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<TResult>(stream, _options);

        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="client"></param>
    /// <param name="json"></param>
    /// <returns></returns>
    private async Task<TResult?> PostData<TResult>(HttpClient client, JsonObject json) where TResult : class, new()
    {
        var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
        using var response = await client.PostAsync(client.BaseAddress, content);
        
        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<TResult>(stream, _options);

        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public HttpClient CreateHttpClient()
    {
        var configSections = new List<Func<IConfigurationSection?, HttpClient>>
        {
            ConfigureJwt!,
            ApiKeyConfiguration!
        };
        
        var rootSection = _configuration.GetSection($"{typeof(TConfig).Name}");
        var httpClient = configSections.Select(x => x.Invoke(rootSection)).SingleOrDefault(x => x != null);
        return httpClient ?? _httpClientFactory.CreateClient($"{typeof(TService).Name}Client");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private HttpClient? ConfigureJwt(IConfigurationSection? section)
    {
        var jwtConfiguration = DetectConfigurationSection<JwtConfig>(section);
        return jwtConfiguration != null ? _httpClientFactory.CreateClient($"{typeof(TService).Name}Client") : null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private HttpClient? ApiKeyConfiguration(IConfigurationSection? section)
    {
        var apikeyConfiguration = DetectConfigurationSection<ApikeyConfig>(section);
        return apikeyConfiguration != null ? _httpClientFactory.CreateClient($"{typeof(TService).Name}Client") : null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TConfigurationSectionType"></typeparam>
    /// <param name="section"></param>
    /// <returns></returns>
    private IConfigurationSection? DetectConfigurationSection<TConfigurationSectionType>(IConfiguration? section)
    {
        var name = typeof(TConfigurationSectionType).Name;
        return section!.GetSection(name).GetChildren().Any() ? section.GetSection(name) : null;
    }
}