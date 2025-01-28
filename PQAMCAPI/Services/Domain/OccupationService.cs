using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.Services.Domain
{
    public class OccupationService : IOccupationService
    {
        const string PACKAGE_NAME = "AMC_OCCUPATION_PKG";

        private readonly IStoreProcedureService _spService;

        public OccupationService(IStoreProcedureService spService)
        {
            _spService = spService;
        }

        public Task<int> DeleteOccupation(int occupationId)
        {
            var rowCount = _spService.DeleteSP<Occupation>(PACKAGE_NAME + ".del_occupation", occupationId);
            return rowCount;
        }

        public Task<Occupation> FindAsync(int occupationId)
        {
            var occupation = _spService.GetSP<Occupation>(PACKAGE_NAME + ".get_occupation", occupationId);
            return occupation;
        }

        public Task<List<Occupation>> GetAllAsync()
        {
            var occupations = _spService.GetAllSP<Occupation>(PACKAGE_NAME + ".get_occupations", -1);
            return occupations;
        }

        public Task<Occupation> InsertOccupation(Occupation occupation)
        {
            throw new NotImplementedException();
        }

        public Task<Occupation> UpdateOccupation(int occupationId, Occupation occupation)
        {
            throw new NotImplementedException();
        }
    }
}
