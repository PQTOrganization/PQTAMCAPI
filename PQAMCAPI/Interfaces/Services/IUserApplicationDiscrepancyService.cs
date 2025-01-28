using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IUserApplicationDiscrepancyService
    {
        Task<UserApplicationDiscrepancyDTO> GetUserApplicationDiscrepancy(int UserApplicationDiscrepancyId);
        Task<UserApplicationDiscrepancyDTO> GetDiscrepancyForUserApplication(int UserApplicationId);
        Task<UserApplicationDiscrepancyDTO> InsertDiscrepancy(InsertDiscrepancyDTO Data);
    }
}
