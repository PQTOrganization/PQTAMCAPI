using PQAMCAPI.Models;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IInvestorTransactionDBService
    {
        Task<InvestorTransaction> InsertInvestorTransaction(InvestorTransaction Data);
    }
}
