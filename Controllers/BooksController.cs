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
    private readonly IHttpClientFactory _httpClientFactory;

    public BooksController(IHttpClientFactory httpClientFactory, ILogger<BooksService> logger)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet(Name = "GetBook")]
    public Library Get()
    {
        var serviceFactory = new ServiceFactory<BooksService>(_httpClientFactory, _logger);
        var parameters = new List<KeyValuePair<string, string>>
        {
            new("bibkeys", "ISBN:9781492092391"),
            new("format","json")
        };
        var result = serviceFactory.Execute<Book>().Result;
        return new Library
        {
            Date = DateTime.Now,
            Books = new List<Book>
            {
                new()
                {
                    BookInformation = result!.BookInformation
                }
            }
        };
    }
}
