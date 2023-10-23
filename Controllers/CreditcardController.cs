using Corporate.Application.Services.Model.Finance;
using Corporate.Application.Services.Services;
using Corporate.Application.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Corporate.Application.Services.Controllers;

[ApiController]
[Route("[controller]")]
public class CreditcardController : ControllerBase
{
    private readonly ICreditcardService _creditcardService;
    private readonly ILogger<CreditcardService> _logger;
    
    public CreditcardController(ICreditcardService creditcardService, ILogger<CreditcardService> logger)
    {
        _creditcardService = creditcardService;
        _logger = logger;
    }

    [HttpGet(Name = "GetCreditcard")]
    public Creditcard Get()
    {
        var result = _creditcardService.GetCreditcard();
        return result!;
    }
}
