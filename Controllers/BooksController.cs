using Corporate.Application.Services.Infrastructure;
using Corporate.Application.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Corporate.Application.Services.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly ILogger<BooksService> _logger;

    public BooksController(ILogger<BooksService> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        var serviceFactory = new ServiceFactory<BooksService>(null, _logger, null);


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
