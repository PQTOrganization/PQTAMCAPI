using PQAMCAPI.Models;

namespace PQAMCAPI.Interfaces.Services
{
    public interface ICityService
    {
        Task<List<City>> GetAllAsync();
        Task<List<City>> GetCitiesByCountry(int countryId);
        Task<City> FindAsync(int cityId);
        Task<City> InsertCity(City city);
        Task<City> UpdateCity(int cityId, City city);
        Task<int> DeleteCity(int cityId);



    }
}
