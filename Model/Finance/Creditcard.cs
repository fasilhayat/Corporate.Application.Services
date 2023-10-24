namespace Corporate.Application.Services.Model.Finance;

public class Creditcard
{
    public int Id { get; set; }

    public string? Uid { get; set; }

    public string? Credit_Card_Number { get; set; }

    public DateTime Credit_Card_ExpiryDate { get; set; }

    public string? Credit_Card_Type { get; set; }
}