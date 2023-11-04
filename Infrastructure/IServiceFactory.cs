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
    Task<TResult?> Execute<TResult>(JsonObject json) where TResult : class;

    /// <summary>
    /// Associated with HTTP POST method.
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    void Execute<TObject>(TObject obj) where TObject : class;
}