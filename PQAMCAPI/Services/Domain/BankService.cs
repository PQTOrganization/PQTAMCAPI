using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.Services.Domain
{
    public class BankService : IBankService
    {
        const string PACKAGE_NAME = "AMC_BANK_PKG";

        private readonly IStoreProcedureService _spService;

        public BankService(IStoreProcedureService spService, PQAMCAPIContext dbContext)
        {
            _spService = spService;
        }

        public Task<int> DeleteBank(int bankId)
        {
            var rowCount = _spService.DeleteSP<Bank>(PACKAGE_NAME + ".del_bank", bankId);
            return rowCount;
        }

        public Task<Bank> FindAsync(int bankId)
        {
            var bank = _spService.GetSP<Bank>(PACKAGE_NAME + ".get_bank", bankId);
            return bank;
        }

        public Task<List<Bank>> GetAllAsync()
        {
            var banks = _spService.GetAllSP<Bank>(PACKAGE_NAME + ".get_banks", -1);
            return banks;
        }

        public Task<Bank> InsertBank(Bank bank)
        {
            throw new NotImplementedException();
        }

        public Task<Bank> UpdateBank(int bankId, Bank bank)
        {
            throw new NotImplementedException();
        }
    }
}
