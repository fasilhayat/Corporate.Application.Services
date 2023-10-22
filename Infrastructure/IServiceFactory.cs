using System.Text.Json.Nodes;

namespace Corporate.Application.Services.Infrastructure
{
    public interface IServiceFactory<TService>
    {
        Task<TResult?> ExecuteGet<TResult>(string parameters) where TResult : class, new();

        Task<TResult?> ExecutePost<TResult>(JsonObject json) where TResult : class, new();
    }
}
