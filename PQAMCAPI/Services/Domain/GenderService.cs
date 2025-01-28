using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.Services.Domain
{
    public class GenderService : IGenderService
    {
        const string PACKAGE_NAME = "AMC_GENDER_PKG";

        private readonly IStoreProcedureService _spService;

        public GenderService(IStoreProcedureService spService, PQAMCAPIContext dbContext)
        {
            _spService = spService;
        }

        public Task<int> DeleteGender(int genderId)
        {
            var rowCount = _spService.DeleteSP<Gender>(PACKAGE_NAME + ".del_gender", genderId);
            return rowCount;
        }

        public Task<Gender> FindAsync(int genderId)
        {
            var gender = _spService.GetSP<Gender>(PACKAGE_NAME + ".get_gender", genderId);
            return gender;
        }

        public Task<List<Gender>> GetAllAsync()
        {
            var genders = _spService.GetAllSP<Gender>(PACKAGE_NAME + ".get_genders", -1);
            return genders;
        }

        public Task<Gender> InsertGender(Gender gender)
        {
            throw new NotImplementedException();
        }

        public Task<Gender> UpdateGender(int genderId, Gender gender)
        {
            throw new NotImplementedException();
        }
    }
}
