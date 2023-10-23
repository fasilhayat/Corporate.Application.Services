using System.Text.Json.Serialization;

namespace Corporate.Application.Services.Model.Membership;

public class User
{
    public int Id { get; set; }

    public string? Uid { get; set; }

    public string? Password { get; set; }

    [JsonPropertyName("first_name")]
    public string? Firstname { get; set; }

    [JsonPropertyName("last_name")]
    public string? Lastname { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }
}