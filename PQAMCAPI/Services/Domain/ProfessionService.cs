using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.Services.Domain
{
    public class ProfessionService : IProfessionService
    {
        const string PACKAGE_NAME = "AMC_PROFESSION_PKG";

        private readonly IStoreProcedureService _spService;

        public ProfessionService(IStoreProcedureService spService)
        {
            _spService = spService;
        }

        public Task<int> DeleteProfession(int professionId)
        {
            var rowCount = _spService.DeleteSP<Profession>(PACKAGE_NAME + ".del_profession", professionId);
            return rowCount;
        }

        public Task<Profession> FindAsync(int professionId)
        {
            var profession = _spService.GetSP<Profession>(PACKAGE_NAME + ".get_profession", professionId);
            return profession;
        }

        public Task<List<Profession>> GetAllAsync()
        {
            var professions = _spService.GetAllSP<Profession>(PACKAGE_NAME + ".get_professions", -1);
            return professions;
        }

        public Task<Profession> InsertProfession(Profession profession)
        {
            throw new NotImplementedException();
        }

        public Task<Profession> UpdateProfession(int professionId, Profession profession)
        {
            throw new NotImplementedException();
        }
    }
}
