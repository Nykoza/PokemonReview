using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces;

public interface ICountryRepository
{
    ICollection<Country> GetCountries();
    Country GetCountry(int id);
    Country GetCountryByOwner(int ownerId);
    Task<ICollection<Owner>> GetOwnersFromACountry(int countryId);
    bool CountryExists(int id);
    bool UpdateCountry(Country country);
    bool CreateCountry(Country country);
    bool DeleteCountry(Country country);
    bool Save();
}