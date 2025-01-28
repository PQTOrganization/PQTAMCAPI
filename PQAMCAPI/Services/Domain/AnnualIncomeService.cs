using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.Services.Domain
{
    public class AnnualIncomeService : IAnnualIncomeService
    {
        const string PACKAGE_NAME = "AMC_ANNUAL_INCOME_PKG";

        private readonly IStoreProcedureService _spService;

        public AnnualIncomeService(IStoreProcedureService spService, PQAMCAPIContext dbContext)
        {
            _spService = spService;
        }

        public Task<int> DeleteAnnualIncome(int annualIncomeId)
        {
            var rowCount = _spService.DeleteSP<AnnualIncome>(PACKAGE_NAME + ".DEL_ANNUAL_INCOME", annualIncomeId);
            return rowCount;
        }

        public Task<AnnualIncome> FindAsync(int annualIncomeId)
        {
            var annualIncome = _spService.GetSP<AnnualIncome>(PACKAGE_NAME + ".GET_ANNUAL_INCOME", annualIncomeId);
            return annualIncome;
        }

        public Task<List<AnnualIncome>> GetAllAsync()
        {
            var cities = _spService.GetAllSP<AnnualIncome>(PACKAGE_NAME + ".GET_ANNUAL_INCOMES", -1);
            return cities;
        }

        public Task<AnnualIncome> InsertAnnualIncome(AnnualIncome annualIncome)
        {
            throw new NotImplementedException();
        }

        public Task<AnnualIncome> UpdateAnnualIncome(int annualIncomeId, AnnualIncome annualIncome)
        {
            throw new NotImplementedException();
        }
    }
}
