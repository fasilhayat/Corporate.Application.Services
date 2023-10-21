namespace Corporate.Application.Services.Model.Litterature;

public class Library
{
    public DateTime Date { get; init; }

    public IEnumerable<Book>? Books { get; set; }
}