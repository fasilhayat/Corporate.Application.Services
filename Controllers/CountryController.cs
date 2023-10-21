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
    private readonly IHttpClientFactory _httpClientFactory;

    public CountryController(IHttpClientFactory httpClientFactory, ILogger<CountryService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    [HttpGet(Name = "GeCountry")]
    public Country Get()
    {
        var serviceFactory = new ServiceFactory<CountryService>(_httpClientFactory, _logger);

        var result = serviceFactory.Execute<List<Country>>().Result;
        return null;
    }
}
