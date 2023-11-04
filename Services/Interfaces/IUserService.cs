using Corporate.Application.Services.Model.Membership;

namespace Corporate.Application.Services.Services.Interfaces;

public interface IUserService   
{
    IEnumerable<User>? GetUsers();

    void AddUser(User user);
}
