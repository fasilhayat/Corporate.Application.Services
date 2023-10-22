using Corporate.Application.Services.Infrastructure;
using Corporate.Application.Services.Model.Geography;
using Corporate.Application.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Corporate.Application.Services.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CountryController : ControllerBase
{
    private readonly ILogger<CountryService> _logger;
    private readonly IServiceFactory<CountryService> _serviceFactory;

    public CountryController(IServiceFactory<CountryService> serviceFactory, ILogger<CountryService> logger)
    {
        _logger = logger;
        _serviceFactory = serviceFactory;
    }

    [HttpGet(Name = "GeCountry")]
    public Country Get()
    {
        var result = _serviceFactory.Execute<List<Country>>().Result;
        return null;
    }
}
