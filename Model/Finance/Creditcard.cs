namespace Corporate.Application.Services.Model.Finance;

public record Creditcard
{
    public int Id { get; init; }

    public string? Uid { get; init; }

    public string? Credit_Card_Number { get; init; }

    public DateTime Credit_Card_ExpiryDate { get; init; }

    public string? Credit_Card_Type { get; init; }
}