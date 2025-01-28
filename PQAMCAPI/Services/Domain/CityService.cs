using System.Data;

using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.Services.Domain
{
    public class CityService : ICityService
    {
        const string PACKAGE_NAME = "AMC_CITY_PKG";

        private readonly IStoreProcedureService _spService;

        public CityService(IStoreProcedureService spService, PQAMCAPIContext dbContext)
        {
            _spService = spService;
        }

        public Task<int> DeleteCity(int cityId)
        {
            var rowCount = _spService.DeleteSP<City>(PACKAGE_NAME + ".DEL_CITY", cityId);
            return rowCount;
        }

        public Task<City> FindAsync(int cityId)
        {
            var city = _spService.GetSP<City>(PACKAGE_NAME + ".GET_CITY", cityId);
            return city;
        }

        public Task<List<City>> GetAllAsync()
        {
            var cities = _spService.GetAllSP<City>(PACKAGE_NAME + ".GET_CITIES", -1);
            return cities;
        }

        public async Task<List<City>> GetCitiesByCountry(int countryId)
        {
            var cities = await _spService.GetAllSP<City>(PACKAGE_NAME + ".GET_CITIES", -1);
            return cities.Where(x => x.CountryId == countryId).ToList();
        }

        public Task<City> InsertCity(City city)
        {
            throw new NotImplementedException();
        }

        public Task<City> UpdateCity(int cityId, City city)
        {
            throw new NotImplementedException();
        }
    }
}
