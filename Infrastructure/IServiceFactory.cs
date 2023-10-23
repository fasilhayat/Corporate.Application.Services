using System.Text.Json.Nodes;

namespace Corporate.Application.Services.Infrastructure
{
    public interface IServiceFactory<TService>
    {
        /// <summary>
        /// Associated with HTTP GET method.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<TResult?> Execute<TResult>(IEnumerable<KeyValuePair<string, string>> parameters) where TResult : class, new();

        /// <summary>
        /// Associated with HTTP GET method.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="querystring"></param>
        /// <returns></returns>
        Task<TResult?> Execute<TResult>(string querystring) where TResult : class, new();

        /// <summary>
        /// Associated with POST
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        Task<TResult?> Execute<TResult>(JsonObject json) where TResult : class, new();
    }
}
