using PQAMCAPI.Models;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IAccountCategoryService
    {
        Task<List<AccountCategory>> GetAllAsync();
        Task<AccountCategory> FindAsync(int accountCategoryId);
        Task<AccountCategory> InsertAccountCategory(AccountCategory accountCategory);
        Task<AccountCategory> UpdateAccountCategory(int accountCategoryId, AccountCategory accountCategory);
        Task<int> DeleteAccountCategory(int accountCategoryId);
    }
}
