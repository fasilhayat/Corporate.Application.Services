namespace Corporate.Application.Services.Infrastructure
{
    public interface IServiceFactory<TService>
    {
        Task<TResult?> Execute<TResult>(string parameters) where TResult : class, new();
    }
}
