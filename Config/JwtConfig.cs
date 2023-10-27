using Corporate.Application.Services.Config.Interfaces;

namespace Corporate.Application.Services.Config;

public record JwtConfig : IJwtConfig
{
    public string? PublicKeyFilePath { get; init; }

    public string? PrivateKeyFilePath { get; init; }

    public string? ExternalIssuer { get; init; }

    public string? InternalIssuer { get; init; }

    public string? Algorithm { get; init; }

    public string? HashAlgorithm { get; init; }

    public bool ValidateLifetime { get; init; }

    public bool ValidateAudience { get; init; }

    public bool ValidateIssuer { get; init; }

    public bool ValidateIssuerSigningKey { get; init; }
}