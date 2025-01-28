using PQAMCClasses.CloudDTOs;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IFundTransferRequestService
    {
        Task<List<FundTransferRequestDTO>> GetAllFundTransferRequestsAsync();
        Task<List<FundTransferRequestDTO>> GetAllFundTransferRequestsForFolioAsync(string FolioNumber);
        Task<FundTransferRequestDTO> GetFundTransferRequestAsync(int InvestmentRequestId);
        Task<FundTransferRequestDTO> InsertFundTransferRequestAsync(FundTransferRequestDTO Request);
        Task UpdateFundTransferRequestStatusAsync(int InvestmentRequestId, short Status);
        Task<SubmitResponseDTO> SubmitFundTransferRequesToCloud(int FundTransferRequestId);
    }
}
