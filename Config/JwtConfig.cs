namespace Corporate.Application.Services.Config;

public class JwtConfig
{
    public string? PublicKeyPath { get; set; }

    public string? PrivateKeyPath { get; set; }

    public string? EncryptionAlgorithm { get; set; }

    public bool EnableEncryption { get; set; }
}