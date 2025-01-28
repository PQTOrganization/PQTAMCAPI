using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IRedemptionRequestDBService
    {
        Task<RedemptionRequestDTO> InsertRedemptionRequestAsync(RedemptionRequestDTO Request);
        Task UpdateRedemptionRequestStatusAsync(int RedemptionRequestId, short Status);
    }
}
