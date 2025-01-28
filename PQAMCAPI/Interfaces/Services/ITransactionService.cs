using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface ITransactionService
    {
        Task<List<TransactionDTO>> GetTransactionsForFolioAsync(string FolioNumber);
        Task<List<PendingTransactionDTO>> GetPendingTransactionsForFolioAsync(string FolioNumber);
        Task<List<TransactionDTO>> GetTransactionsForFolioFromCloudAsync(string FolioNumber);
    }
}
