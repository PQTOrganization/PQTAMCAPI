using PQAMCClasses.CloudDTOs;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IRedemptionRequestService
    {
        Task<List<RedemptionRequestDTO>> GetAllRedemptionRequestsAsync();
        Task<List<RedemptionRequestDTO>> GetAllRedemptionRequestsForFolioAsync(string FolioNumber);
        Task<RedemptionRequestDTO> GetRedemptionRequestAsync(int RedemptionRequestId);
        Task<RedemptionRequestDTO> InsertRedemptionRequestAsync(RedemptionRequestDTO Request);
        Task UpdateRedemptionRequestStatusAsync(int RedemptionRequestId, short Status);
        Task<SubmitResponseDTO> SubmitRedemptionRequestToCloud(int RedemptionRequestId);
        Task<FolioBankResponseDTO> GetFolioBankList(int UserId);
    }
}
