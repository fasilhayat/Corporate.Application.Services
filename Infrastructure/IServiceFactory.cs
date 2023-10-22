namespace Corporate.Application.Services.Infrastructure
{
    public interface IServiceFactory<TService>
    {
        Task<TResult?> Execute<TResult>() where TResult : class, new();

        //Task<TResult> GetResultAsync<TResult>(IEnumerable<KeyValuePair<string, string>> parameters)
        //    where TResult : class, new();
    }
}
