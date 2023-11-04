using Corporate.Application.Services.Model.Membership;
using Corporate.Application.Services.Services;
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
    public UserCatalog Get()
    {
        var result = _userService.GetUsers();
        return new UserCatalog
        {
            Users = result
        };
    }

    [HttpPost(Name = "AddUser")]
    public void Add(User user)
    {
        _userService.AddUser(user);
    }
}
