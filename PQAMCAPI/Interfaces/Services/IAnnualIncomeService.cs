using PQAMCAPI.Models;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IAnnualIncomeService
    {
        Task<List<AnnualIncome>> GetAllAsync();
        Task<AnnualIncome> FindAsync(int annualIncomeId);
        Task<AnnualIncome> InsertAnnualIncome(AnnualIncome annualIncome);
        Task<AnnualIncome> UpdateAnnualIncome(int annualIncomeId, AnnualIncome annualIncome);
        Task<int> DeleteAnnualIncome(int annualIncomeId);



    }
}
