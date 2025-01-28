using PQAMCAPI.Models;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IUserApplicationDBService
    {
        Task<List<UserApplication>> GetAllAsync();
        Task<UserApplication> FindAsync(int UserApplicationId);
        Task<UserApplication> GetApplicationForUser(int UserId);
        Task<UserApplication> InsertUserApplication(UserApplication Data);
        Task<UserApplication> UpdateUserApplication(int UserApplicationId, UserApplication Data);
        Task<UserApplication> UpdateUserApplicationStatus(int UserApplicationId, short Status);
        Task<Boolean> DoesCNICExistsForOtherApplication(string CellNo, string CNIC);
        Task<int> DeleteUserApplication(int UserApplicationId);
        Task<UserApplication> UpdateInitialInvestment(int UserApplicationId, int InvestmentRequestID);
    }
}
