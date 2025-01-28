using PQAMCAPI.Models;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IUserBankDBService
    {
        Task<UserBank> FindAsync(int userBankId);
        Task<List<UserBank>> GetAllAsync();
        Task<List<UserBank>> GetAllForUser(int userId);
        Task<UserBank> InsertUserBank(UserBank userBank);
        Task<UserBank> UpdateUserBank(int userBankId, UserBank userBank);
        Task<UserBank> DeleteUserBank(int UserApplicationId, int? BankId);
    }
}
