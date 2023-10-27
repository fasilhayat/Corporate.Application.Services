namespace Corporate.Application.Services.Config.Interfaces
{
    public interface IJwtConfig
    {
        string? PublicKeyFilePath { get; init; }

        string? PrivateKeyFilePath { get; init; }

        string? ExternalIssuer { get; init; }

        string? InternalIssuer { get; init; }

        string? Algorithm { get; init; }

        string? HashAlgorithm { get; init; }

        bool ValidateLifetime { get; init; }

        bool ValidateAudience { get; init; }

        bool ValidateIssuer { get; init; }

        bool ValidateIssuerSigningKey { get; init; }
    }
}
