using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.DBServices
{
    public interface ITransactionDBService
    {
        Task<List<TransactionDTO>> GetTransactionsForFolioAsync(string FolioNumber);
    }
}
