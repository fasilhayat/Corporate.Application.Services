using System.Text.Json.Serialization;

namespace Corporate.Application.Services.Model.Membership;

public record User
{
    public int Id { get; init; }

    public string? Uid { get; init; }

    public string? Password { get; init; }

    [JsonPropertyName("first_name")]
    public string? Firstname { get; init; }

    [JsonPropertyName("last_name")]
    public string? Lastname { get; init; }

    public string? Username { get; init; }

    public string? Email { get; init; }
}