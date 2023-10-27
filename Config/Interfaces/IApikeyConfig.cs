namespace Corporate.Application.Services.Config.Interfaces
{
    public interface IApikeyConfig
    {
        string? Key { get; init; }
        bool EnableEncryption { get; init; }
    }
}
