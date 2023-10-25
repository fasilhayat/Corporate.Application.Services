namespace Corporate.Application.Services.Model.Membership;

public record UserCatalog
{
    public IEnumerable<User>? Users { get; init; }
}