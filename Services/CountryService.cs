using Corporate.Application.Services.Infrastructure;
using Corporate.Application.Services.Model.Geography;
using Corporate.Application.Services.Services.Interfaces;

namespace Corporate.Application.Services.Services;

public class CountryService : ICountryService
{
    private readonly IServiceFactory<CountryService> _serviceFactory;
    private readonly ILogger<CountryService> _logger;

    public CountryService(IServiceFactory<CountryService> serviceFactory, ILogger<CountryService> logger)
    {
        _serviceFactory = serviceFactory;
        _logger = logger;
    }

    public Country? GetCountry(string cityName)
    {
        var parameters = $"capital/{cityName}";
        var country = _serviceFactory.ExecuteGet<Country>(parameters).Result;

        return country;
    }
}
