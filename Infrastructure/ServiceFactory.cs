﻿using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Corporate.Application.Services.Config;

namespace Corporate.Application.Services.Infrastructure;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TServiceConfig"></typeparam>
public sealed class ServiceFactory<TService, TServiceConfig> : IServiceFactory<TService> 
    where TService : class 
    where TServiceConfig : class
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

        //TODO: Read configuration
        var someting = ExtractServiceConfiguration();
        
        var httpClient = _httpClientFactory.CreateClient($"{typeof(TService).Name}Client");
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
        //TODO: Read configuration
        var someting = ExtractServiceConfiguration();

        var httpClient = _httpClientFactory.CreateClient($"{typeof(TService).Name}Client");
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
        var httpClient = _httpClientFactory.CreateClient($"{typeof(TService).Name}Client");
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
    public IConfigurationSection ExtractServiceConfiguration()
    {
        var configSections = new List<Func<IConfigurationSection>>
        {
            JwtConfiguration,
            ApiKeyConfiguration
        };

        var configSection = configSections.Select(x => x.Invoke());
        return configSection.FirstOrDefault()!;

    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IConfigurationSection JwtConfiguration()
    {
        //TODO: Read configuration
        var settings = _configuration.GetSection($"{typeof(TServiceConfig).Name}");
        var setting = settings.Get<JwtConfig>();

        return settings;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IConfigurationSection ApiKeyConfiguration()
    {
        //TODO: Read configuration
        var settings = _configuration.GetSection($"{typeof(TServiceConfig).Name}");
        var setting = settings.Get<ApikeyConfig>();
        return settings;
    }
}