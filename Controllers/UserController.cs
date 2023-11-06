using Corporate.Application.Services.Infrastructure.Filters;
using Corporate.Application.Services.Integrations;
using Corporate.Application.Services.Model.Membership;
using Corporate.Application.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Corporate.Application.Services.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserService> _logger;

    public UserController(IUserService userService, ILogger<UserService> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet(Name = "GetUsers")]
    [TypeFilter(typeof(ControllerFilter))]
    [TypeFilter(typeof(UserFilter))]
    public UserCatalog Get()
    {
        var result = _userService.GetUsers();
        return new UserCatalog
        {
            Users = result
        };
    }

    [HttpPost(Name = "AddUser")]
    [TypeFilter(typeof(ControllerFilter))]
    [TypeFilter(typeof(UserFilter))]
    public void Add(User user)
    {
        _userService.AddUser(user);
    }
}
