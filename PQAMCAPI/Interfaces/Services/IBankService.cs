using PQAMCAPI.Models;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IBankService
    {
        Task<List<Bank>> GetAllAsync();
        Task<Bank> FindAsync(int bankId);
        Task<Bank> InsertBank(Bank bank);
        Task<Bank> UpdateBank(int bankId, Bank bank);
        Task<int> DeleteBank(int bankId);



    }
}
