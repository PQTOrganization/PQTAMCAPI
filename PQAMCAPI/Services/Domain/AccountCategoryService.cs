using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.Services.Domain
{
    public class AccountCategoryService : IAccountCategoryService
    {
        const string PACKAGE_NAME = "AMC_ACCOUNT_CATEGORY_PKG";

        private readonly IStoreProcedureService _spService;

        public AccountCategoryService(IStoreProcedureService spService)
        {
            _spService = spService;
        }

        public Task<int> DeleteAccountCategory(int accountCategoryId)
        {
            var rowCount = _spService.DeleteSP<AccountCategory>(PACKAGE_NAME + ".del_account_category", accountCategoryId);
            return rowCount;
        }

        public Task<AccountCategory> FindAsync(int accountCategoryId)
        {
            var accountCategory = _spService.GetSP<AccountCategory>(PACKAGE_NAME + ".get_account_category", accountCategoryId);
            return accountCategory;
        }

        public Task<List<AccountCategory>> GetAllAsync()
        {
            var accountCategories = _spService.GetAllSP<AccountCategory>(PACKAGE_NAME + ".get_account_categories", -1);
            return accountCategories;
        }

        public Task<AccountCategory> InsertAccountCategory(AccountCategory accountCategory)
        {
            throw new NotImplementedException();
        }

        public Task<AccountCategory> UpdateAccountCategory(int accountCategoryId, AccountCategory accountCategory)
        {
            throw new NotImplementedException();
        }
    }
}
