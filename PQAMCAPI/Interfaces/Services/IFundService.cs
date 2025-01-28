using PQAMCClasses.CloudDTOs;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IFundService
    {
        Task<List<FundDTO>> GetFundsAsync();
        Task<List<FundBankDTO>> GetAllFundsWithBankAsync();
        Task<List<FundBankDTO>> GetAllFundsWithBankFromCloudAsync();
        Task<List<FundDTO>> GetFundsFromCloudAsync();
        Task<List<FundDTO>> GetFundsByAccountCategoryAsync(int AccountCategoryID);
        Task<FundNAVDTO> GetFundNAVAsync(int FundID);
    }
}
