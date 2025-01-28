using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IInvestmentService
    {
        Task<InvestmentSummaryDTO> GetInvestmentSummaryForFolioAsync(string FolioNumber);
        Task<List<FundPositionDTO>> GetFundWisePositionForFolioAsync(string FolioNumber);
        Task<List<FundPositionDTO>> GetAllFundWisePositionForFolioAsync(string FolioNumber);
        Task<List<LabelWiseInvestmentDTO>> GetFundRiskWiseSummaryForFolioAsync(string FolioNumber);
        Task<List<LabelWiseInvestmentDTO>> GetFundCategoryWiseSummaryForFolioAsync(string FolioNumber);
        Task<List<LabelWiseInvestmentDTO>> GetFundSummaryForFolioAsync(string FolioNumber);
        Task<List<LabelWiseInvestmentDTO>> GetFundSummaryForFolioAsyncFromCloudAsync(string FolioNumber);
        Task<InvestmentSummaryDTO> GetInvestmentSummaryForFolioFromCloudAsync(string FolioNumber);
        Task<List<LabelWiseInvestmentDTO>> GetFundCategoryWiseSummaryForFolioFromCloudAsync(string FolioNumber);
        Task<List<LabelWiseInvestmentDTO>> GetFundRiskWiseSummaryForFolioFromCloudAsync(string FolioNumber);
        Task<List<FundPositionDTO>> GetFundWisePositionForFolioFromCloudAsync(string FolioNumber);
        Task<List<FundPositionDTO>> GetAllFundWisePositionForFolioFromCloudAsync(string FolioNumber);
        Task<DashboardSummaryDTO> GetDashboardSummaryFromCloud(string FolioNumber);
    }
}
