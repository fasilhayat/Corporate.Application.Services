using Corporate.Application.Services.Model.Litterature;

namespace Corporate.Application.Services.Services.Interfaces;

public interface IBookService   
{
    Book? GetBook(string isbn);
}
