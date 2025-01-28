using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.Services.Domain
{
    public class IncomeSourceService : IIncomeSourceService
    {
        const string PACKAGE_NAME = "AMC_INCOME_SOURCE_PKG";

        private readonly IStoreProcedureService _spService;

        public IncomeSourceService(IStoreProcedureService spService)
        {
            _spService = spService;
        }

        public Task<int> DeleteIncomeSource(int incomeSourceId)
        {
            var rowCount = _spService.DeleteSP<IncomeSource>(PACKAGE_NAME + ".del_source_income", incomeSourceId);
            return rowCount;
        }

        public Task<IncomeSource> FindAsync(int incomeSourceId)
        {
            var sourceIncome = _spService.GetSP<IncomeSource>(PACKAGE_NAME + ".get_source_income", incomeSourceId);
            return sourceIncome;
        }

        public Task<List<IncomeSource>> GetAllAsync()
        {
            var sourceIncomes = _spService.GetAllSP<IncomeSource>(PACKAGE_NAME + ".get_source_incomes", -1);
            return sourceIncomes;
        }

        public Task<IncomeSource> InsertIncomeSource(IncomeSource IncomeSource)
        {
            throw new NotImplementedException();
        }

        public Task<IncomeSource> UpdateIncomeSource(int incomeSourceId, IncomeSource IncomeSource)
        {
            throw new NotImplementedException();
        }
    }
}
