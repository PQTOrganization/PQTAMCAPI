using PQAMCClasses.CloudDTOs;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IInvestmentRequestService
    {
        Task<List<InvestmentRequestDTO>> GetAllInvestmentRequestsAsync();
        Task<List<InvestmentRequestDTO>> GetAllInvestmentRequestsForFolioAsync(string FolioNumber);
        Task<InvestmentRequestDTO> GetInvestmentRequestAsync(int InvestmentRequestId);
        Task<InvestmentRequestDTO> InsertInvestmentRequestAsync(InvestmentRequestDTO Request);
        Task UpdateInvestmentRequestStatusAsync(int InvestmentRequestId, short Status);
        Task<SubmitResponseDTO> SubmitSubSaleRequestToCloud(int InvestmentRequestId);
        Task<bool> UpdateInvestmentRequestBlinqResponse(string ReferenceNumber, string Response, short Status);
        Task<InvestmentRequestDTO> InsertInitialInvestmentRequestAsync(InitialInvestmentRequestDTO InitialRequest);
    }
}
