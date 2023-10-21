using Corporate.Application.Services.Infrastructure;
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
    public IEnumerable<WeatherForecast> Get()
    {
        var serviceFactory = new ServiceFactory<CountryService>(null, _logger, null);


        return null;

        //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //{
        //    Date = DateTime.Now.AddDays(index),
        //    TemperatureC = Random.Shared.Next(-20, 55),
        //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //})
        //.ToArray();
    }
}
