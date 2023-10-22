using Corporate.Application.Services.Model.Geography;
using Corporate.Application.Services.Services;
using Corporate.Application.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Corporate.Application.Services.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CountryController : ControllerBase
{
    private readonly ICountryService _countryService;
    private readonly ILogger<CountryService> _logger;
    

    public CountryController(ICountryService countryService, ILogger<CountryService> logger)
    {
        _countryService = countryService;
        _logger = logger;
    }

    [HttpGet(Name = "GeCountry")]
    public Country Get()
    {
        var result = _countryService.GetCountry("Copenhagen");
        return null;
    }
}
