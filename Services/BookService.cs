using Corporate.Application.Services.Infrastructure;
using Corporate.Application.Services.Model.Litterature;
using Corporate.Application.Services.Services.Interfaces;

namespace Corporate.Application.Services.Services;

public class BookService : IBookService
{
    private readonly IServiceFactory<BookService> _serviceFactory;
    private readonly ILogger<BookService> _logger;

    public BookService(IServiceFactory<BookService> serviceFactory, ILogger<BookService> logger)
    {
        _serviceFactory = serviceFactory;
        _logger = logger;
    }

    public Book? GetBook(string isbn)
    {
        var kvp = new List<KeyValuePair<string, string>>
        {
            new("bibkeys", isbn),
            new("format","json")
        };

        var parameters = $"?{string.Join("&", kvp.Select(x => $"{x.Key}={x.Value}"))}";
        return _serviceFactory.ExecuteGet<Book>(parameters).Result;
    }
}