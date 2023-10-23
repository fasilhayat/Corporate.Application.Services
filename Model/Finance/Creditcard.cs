namespace Corporate.Application.Services.Model.Finance;

public class Creditcard
{
    public int Id { get; set; }

    public string? Uid { get; set; }

    public string? CreditcardNumber { get; set; }

    public DateTime CreditcardExpiryDate { get; set; }

    public string? CreditcardType { get; set; }
}

