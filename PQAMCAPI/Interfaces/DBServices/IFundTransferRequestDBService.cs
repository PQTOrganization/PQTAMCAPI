using PQAMCAPI.Models;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IFundTransferRequestDBService
    {
        Task<FundTransferRequestDTO> InsertFundTransferRequestAsync(FundTransferRequestDTO Request);
        Task UpdateFundTransferRequestStatusAsync(int FundTransferRequestId, short Status);
    }
}
