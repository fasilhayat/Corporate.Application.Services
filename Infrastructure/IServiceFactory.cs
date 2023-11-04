using System.Text.Json;
using System.Text.Json.Nodes;

namespace Corporate.Application.Services.Infrastructure;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TService"></typeparam>
public interface IServiceFactory<TService>
{
    /// <summary>
    /// Associated with HTTP GET method.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="parameters"></param>
    /// <returns></returns>
    Task<TResult?> Execute<TResult>(IEnumerable<KeyValuePair<string, string>> parameters) where TResult : class;

    /// <summary>
    /// Associated with HTTP GET method.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="querystring"></param>
    /// <returns></returns>
    Task<TResult?> Execute<TResult>(string querystring) where TResult : class;

    /// <summary>
    /// Associated with HTTP POST method.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="json"></param>
    /// <returns></returns>
    Task<TResult?> Execute<TResult>(JsonDocument jsonDocument) where TResult : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TObjectType"></typeparam>
    /// <param name="data"></param>
    void Execute<TObjectType>(TObjectType data) where TObjectType : class;
}