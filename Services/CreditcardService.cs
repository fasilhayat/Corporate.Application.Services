using Corporate.Application.Services.Infrastructure;
using Corporate.Application.Services.Model.Finance;
using Corporate.Application.Services.Services.Interfaces;

namespace Corporate.Application.Services.Services;

public class CreditcardService : ICreditcardService
{
    private readonly IServiceFactory<CreditcardService> _serviceFactory;
    private readonly ILogger<CreditcardService> _logger;

    public CreditcardService(IServiceFactory<CreditcardService> serviceFactory, ILogger<CreditcardService> logger)
    {
        _serviceFactory = serviceFactory;
        _logger = logger;
    }

    public Creditcard? GetCreditcard()
    {
        var parameters = "/credit_cards";
        return _serviceFactory.Execute<Creditcard>(parameters).Result;
    }
}