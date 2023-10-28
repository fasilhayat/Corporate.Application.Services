using Corporate.Application.Services.Config;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Corporate.Application.Services.Infrastructure;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TConfig"></typeparam>
public sealed class ServiceFactory<TService, TConfig> : IServiceFactory<TService> where TConfig : class where TService : class
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
    public async Task<TResult?> Execute<TResult>(IEnumerable<KeyValuePair<string, string>> parameters) where TResult : class
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
    public async Task<TResult?> Execute<TResult>(string querystring) where TResult : class
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
    public async Task<TResult?> Execute<TResult>(JsonObject json) where TResult : class
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
    private async Task<TResult?> GetData<TResult>(HttpClient client, string parameters) where TResult : class
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
    private async Task<TResult?> PostData<TResult>(HttpClient client, JsonObject json) where TResult : class
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
    private HttpClient CreateHttpClient()
    {
        var rootSection = _configuration.GetSection($"{typeof(TConfig).Name}");
        var client = _httpClientFactory.CreateClient($"{typeof(TService).Name}Client");

        var actionList = new List<Action<IConfigurationSection, HttpClient>>
        {
            ConfigureJwt, ConfigureApiKey
        };
        
        // Match configuration and enrich client headers accordingly.
        foreach (var action in actionList)
        {
            action.Invoke(rootSection, client);
        }

        return client;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private void ConfigureJwt(IConfigurationSection? section, HttpClient client)
    {
        var jwtConfigurationSection = DetectConfigurationSection<JwtConfig>(section);
        if(jwtConfigurationSection != null) 
        {
            var jwtConfiguration = jwtConfigurationSection.Get<JwtConfig>();
            var publicKeyPayh = jwtConfiguration.PublicKeyFilePath;
            var privateKeyPayh = jwtConfiguration.PrivateKeyFilePath;
            var algorithm = jwtConfiguration.Algorithm;

            var accessToken = GenerateToken();
            // Add JWT stuff to the client header here.
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private void ConfigureApiKey(IConfigurationSection? section, HttpClient client)
    {
        var apikeyConfigurationSection = DetectConfigurationSection<ApikeyConfig>(section);
        if (apikeyConfigurationSection != null)
        {
            var apikeyConfiguration = apikeyConfigurationSection.Get<ApikeyConfig>();
            var apikey = apikeyConfiguration.Key;
            var enableEncryption = apikeyConfiguration.EnableEncryption;

            // Add API key stuff to the client header here.
            client.DefaultRequestHeaders.Add("x-api-key", apikey);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TConfigurationSectionType"></typeparam>
    /// <param name="section"></param>
    /// <returns></returns>
    private IConfigurationSection? DetectConfigurationSection<TConfigurationSectionType>(IConfiguration? section) where TConfigurationSectionType : class
    {
        var name = typeof(TConfigurationSectionType).Name;
        return section!.GetSection(name).GetChildren().Any() ? section.GetSection(name) : null;
    }

    /// <summary>
    /// /
    /// </summary>
    /// <returns></returns>
    private string GenerateToken()
    {
        var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
        _logger.LogInformation(token);
        return token;
    }
}