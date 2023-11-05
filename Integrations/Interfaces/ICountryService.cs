using Corporate.Application.Services.Model.Geography;

namespace Corporate.Application.Services.Services.Interfaces;

public interface ICountryService
{
    Country? GetCountry(string capitalCityName);
}

