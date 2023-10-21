using System.Collections;
using Corporate.Application.Services.Infrastructure;
using Corporate.Application.Services.Model.Litterature;
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
    public IEnumerable<Library> Get()
    {
        var serviceFactory = new ServiceFactory<BooksService>(new HttpClient(), _logger, null);
        var parameters = new List<KeyValuePair<string, string>>
        {
            new("bibkeys", "ISBN:9781492092391"),
            new("format","json")
        };
        var result = serviceFactory.GetResultAsync<Book>(parameters).Result;
        return null;
    }
}
