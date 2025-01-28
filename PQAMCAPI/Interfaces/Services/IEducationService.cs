using PQAMCAPI.Models;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IEducationService
    {
        Task<List<Education>> GetAllAsync();
        Task<Education> FindAsync(int educationId);
        Task<Education> InsertEducation(Education education);
        Task<Education> UpdateEducation(int educationId, Education education);
        Task<int> DeleteEducation(int educationId);



    }
}
