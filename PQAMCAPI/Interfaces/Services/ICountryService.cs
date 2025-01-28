using PQAMCAPI.Models;

namespace PQAMCAPI.Interfaces.Services
{
    public interface ICountryService
    {
        Task<List<Country>> GetAllAsync();
        Task<Country> FindAsync(int countryId);
        Task<Country> InsertCountry(Country city);
        Task<Country> UpdateCountry(int countryId, Country country);
        Task<int> DeleteCountry(int countryId);

    }
}
