using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.Services.Domain
{
    public class CountryService : ICountryService
    {
        const string PACKAGE_NAME = "AMC_COUNTRY_PKG";

        private readonly IStoreProcedureService _spService;

        public CountryService(IStoreProcedureService spService)
        {
            _spService = spService;
        }

        public Task<int> DeleteCountry(int countryId)
        {
            var rowCount = _spService.DeleteSP<Country>(PACKAGE_NAME + ".DEL_COUNTRY", countryId);
            return rowCount;
        }

        public Task<Country> FindAsync(int countryId)
        {
            var country = _spService.GetSP<Country>(PACKAGE_NAME + ".GET_COUNTRY", countryId);
            return country;
        }

        public Task<List<Country>> GetAllAsync()
        {
            var countries = _spService.GetAllSP<Country>(PACKAGE_NAME + ".GET_COUNTRIES", -1);
            return countries;
        }

        public Task<Country> InsertCountry(Country city)
        {
            throw new NotImplementedException();
        }

        public Task<Country> UpdateCountry(int countryId, Country country)
        {
            throw new NotImplementedException();
        }
    }
}
