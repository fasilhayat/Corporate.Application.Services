using Corporate.Application.Services.Infrastructure;

namespace Corporate.Application.Services.Services;

public class CountryService
{
    private readonly IServiceFactory<CountryService> _serviceFactory;
    private readonly ILogger<CountryService> _logger;

    public CountryService(IServiceFactory<CountryService> serviceFactory, ILogger<CountryService> logger)
    {
        _serviceFactory = serviceFactory;
        _logger = logger;
    }
}
