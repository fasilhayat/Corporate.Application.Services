using Corporate.Application.Services.Model.Litterature;
using Corporate.Application.Services.Services;
using Corporate.Application.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Corporate.Application.Services.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly ILogger<BookService> _logger;
    
    public BooksController(IBookService bookService, ILogger<BookService> logger)
    {
        _bookService = bookService;
        _logger = logger;
    }

    [HttpGet(Name = "GetBook")]
    public Library Get()
    {
        var isbn = "ISBN:9781492092391";
        var result = _bookService.GetBook(isbn);

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
