using PQAMCAPI.Models;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IInvestmentRequestDBService
    {
        Task<InvestmentRequestDTO> InsertInvestmentRequestAsync(InvestmentRequestDTO Request);
        Task UpdateInvestmentRequestStatusAsync(int InvestmentRequestId, short Status);
        Task<bool> UpdateInvestmentRequestBlinqResponse(string ReferenceNumber, string Response, short Status);
    }
}
