using Corporate.Application.Services.Infrastructure;

namespace Corporate.Application.Services.Services;

public class BooksService
{
    private readonly IServiceFactory<BooksService> _serviceFactory;
    private readonly ILogger<BooksService> _logger;

    public BooksService(IServiceFactory<BooksService> serviceFactory, ILogger<BooksService> logger)
    {
        _serviceFactory = serviceFactory;
        _logger = logger;
    }
}