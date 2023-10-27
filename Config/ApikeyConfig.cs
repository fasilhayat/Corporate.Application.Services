using Corporate.Application.Services.Config.Interfaces;

namespace Corporate.Application.Services.Config;

public class ApikeyConfig : IApikeyConfig
{
    public string? Key { get; init; }

    public bool EnableEncryption { get; init; }
}