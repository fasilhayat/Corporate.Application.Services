using Corporate.Application.Services.Infrastructure;
using Corporate.Application.Services.Model.Litterature;
using Corporate.Application.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Corporate.Application.Services.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly IServiceFactory<CountryService> _serviceFactory;
    private readonly ILogger<BooksService> _logger;
    
    public BooksController(IServiceFactory<CountryService> serviceFactory, ILogger<BooksService> logger)
    {
        _serviceFactory = serviceFactory;
        _logger = logger;
    }

    [HttpGet(Name = "GetBook")]
    public Library Get()
    {
        var parameters = new List<KeyValuePair<string, string>>
        {
            new("bibkeys", "ISBN:9781492092391"),
            new("format","json")
        };
        var result = _serviceFactory.Execute<Book>().Result;
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
