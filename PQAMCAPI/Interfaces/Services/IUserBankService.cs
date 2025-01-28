using PQAMCAPI.Models;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IUserBankService
    {
        Task<List<UserBank>> GetAllForUser(int UserId);
        Task<UserBank> FindAsync(int userBankId);
        Task<UserBank> InsertBank(UserBank bank);
        Task<UserBank> UpdateBank(int userBankId, UserBank bank);
        Task<UserBank> DeleteBank(int UserApplicationId, int? BankId);

    }
}
