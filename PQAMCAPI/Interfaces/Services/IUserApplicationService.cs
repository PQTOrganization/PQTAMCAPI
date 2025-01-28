using PQAMCClasses.CloudDTOs;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IUserApplicationService
    {
        Task<List<UserApplicationDTO>> GetAllUserApplications();
        Task<UserApplicationDTO> GetUserApplication(int UserApplicationId);
        Task<UserApplicationDTO> GetApplicationForUser(int UserId);
        Task<UserApplicationDTO> InsertUserApplication(UserApplicationDTO Data);
        Task<UserApplicationDTO> UpdateUserApplication(int UserApplicationId, UserApplicationDTO Data);
        Task<UserApplicationDTO> UpdateUserApplicationStatus(int UserApplicationId, short Status);
        Task<CNICCheckResponseDTO> CheckCNICReuse(string UserId, string CNIC);
        Task<int> DeleteUserApplication(int UserApplicationId);
        Task<SubmitSaleResponseDTO> PostApplicationDataToCloud(int UserApplicationId);
        Task<SubmitDigitalAccountResponseDTO> SubmitDigitalAccount(int UserApplicationId);
        Task<UserApplicationDTO> UpdateInitialInvestment(int UserApplicationId, int InvestmentRequestID);
    }
}
