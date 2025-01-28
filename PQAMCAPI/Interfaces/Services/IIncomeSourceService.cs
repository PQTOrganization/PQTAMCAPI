using PQAMCAPI.Models;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IIncomeSourceService
    {
        Task<List<IncomeSource>> GetAllAsync();
        Task<IncomeSource> FindAsync(int sourceIncomeId);
        Task<IncomeSource> InsertIncomeSource(IncomeSource sourceIncome);
        Task<IncomeSource> UpdateIncomeSource(int sourceIncomeId, IncomeSource sourceIncome);
        Task<int> DeleteIncomeSource(int sourceIncomeId);
    }
}
