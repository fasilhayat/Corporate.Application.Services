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

    public CountryController(ILogger<CountryService> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GeCountry")]
    public IEnumerable<Country> Get()
    {
        var serviceFactory = new ServiceFactory<CountryService>(null, _logger, null);
        //var country = serviceFactory.GetResultAsync<Country>().Result;
        return null;
    }
}
