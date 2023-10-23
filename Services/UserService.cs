using Corporate.Application.Services.Infrastructure;
using Corporate.Application.Services.Model.Membership;
using Corporate.Application.Services.Services.Interfaces;

namespace Corporate.Application.Services.Services;

public class UserService : IUserService
{
    private readonly IServiceFactory<UserService> _serviceFactory;
    private readonly ILogger<UserService> _logger;

    public UserService(IServiceFactory<UserService> serviceFactory, ILogger<UserService> logger)
    {
        _serviceFactory = serviceFactory;
        _logger = logger;
    }

    public IEnumerable<User>? GetUsers()
    {
        var parameters = new List<KeyValuePair<string, string>>
        {
            new("size", "2"),
            new("is_json","true")
        };

        return _serviceFactory.Execute<List<User>>(parameters).Result;
    }
}