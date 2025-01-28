using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.Services.Domain
{
    public class EducationService : IEducationService
    {
        const string PACKAGE_NAME = "AMC_EDUCATION_PKG";

        private readonly IStoreProcedureService _spService;

        public EducationService(IStoreProcedureService spService)
        {
            _spService = spService;
        }

        public Task<int> DeleteEducation(int educationId)
        {
            var rowCount = _spService.DeleteSP<Education>(PACKAGE_NAME + ".del_education", educationId);
            return rowCount;
        }

        public Task<Education> FindAsync(int educationId)
        {
            var education = _spService.GetSP<Education>(PACKAGE_NAME + ".get_education", educationId);
            return education;
        }

        public Task<List<Education>> GetAllAsync()
        {
            var educations = _spService.GetAllSP<Education>(PACKAGE_NAME + ".get_educations", -1);
            return educations;
        }

        public Task<Education> InsertEducation(Education education)
        {
            throw new NotImplementedException();
        }

        public Task<Education> UpdateEducation(int educationId, Education education)
        {
            throw new NotImplementedException();
        }
    }
}
