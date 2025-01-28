using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.Services.Domain
{
    public class UserBankService : IUserBankService
    {
        //const string PACKAGE_NAME = "AMC_BANK_PKG";

        private readonly IStoreProcedureService _spService;
        private readonly IUserBankDBService _userBankDBService;

        public UserBankService(IStoreProcedureService spService, IUserBankDBService userbankDBService, 
            PQAMCAPIContext dbContext)
        {
            _spService = spService;
            _userBankDBService = userbankDBService;
        }

        public async Task<UserBank> DeleteBank(int UserApplicationId, int? BankId)
        {
            return await _userBankDBService.DeleteUserBank(UserApplicationId, BankId);
        }

        public Task<UserBank> FindAsync(int userBankId)
        {            
           return _userBankDBService.FindAsync(userBankId);
        }

        public async Task<List<UserBank>> GetAllForUser(int UserId)
        {
            return await _userBankDBService.GetAllForUser(UserId);
        }

        public async Task<UserBank> InsertBank(UserBank bank)
        {
            return await _userBankDBService.InsertUserBank(bank);            
        }

        public async Task<UserBank> UpdateBank(int bankId, UserBank bank)
        {
            return await _userBankDBService.UpdateUserBank(bankId, bank);
        }
    }
}
