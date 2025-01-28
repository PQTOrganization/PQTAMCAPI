using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PQAMCAPI.Interfaces.Services;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Controller
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "PQAMCAuthScheme")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IInvestmentService _service;

        public DashboardController(IInvestmentService service)
        {
            _service = service;
        }

        [HttpGet("InvestmentSummary/{folionumber}")]
        public async Task<InvestmentSummaryDTO> GetInvestmentSummary(string folionumber)
        {
            return await _service.GetInvestmentSummaryForFolioAsync(folionumber);
        }

        [HttpGet("InvestmentSummaryCloud/{folionumber}")]
        public async Task<InvestmentSummaryDTO> GetInvestmentSummaryFromCloud(string folionumber)
        {
            return await _service.GetInvestmentSummaryForFolioFromCloudAsync(folionumber);
        }

        [HttpGet("CategoryWiseBreakup/{folionumber}")]
        public async Task<List<LabelWiseInvestmentDTO>> GetCategoryWiseBreakup(string folionumber)
        {
            return await _service.GetFundCategoryWiseSummaryForFolioAsync(folionumber);
        }

        

        [HttpGet("CategoryWiseBreakupCloud/{folionumber}")]
        public async Task<List<LabelWiseInvestmentDTO>> GetCategoryWiseBreakupFromCloud(string folionumber)
        {
            return await _service.GetFundCategoryWiseSummaryForFolioFromCloudAsync(folionumber);
        }

        [HttpGet("RiskWiseBreakup/{folionumber}")]
        public async Task<List<LabelWiseInvestmentDTO>> GetRiskWiseBreakup(string folionumber)
        {
            return await _service.GetFundRiskWiseSummaryForFolioAsync(folionumber);
        }

        [HttpGet("RiskWiseBreakupCloud/{folionumber}")]
        public async Task<List<LabelWiseInvestmentDTO>> GetRiskWiseBreakupFromCloud(string folionumber)
        {
            return await _service.GetFundRiskWiseSummaryForFolioFromCloudAsync(folionumber);
        }

        [HttpGet("FundWiseSummary/{folionumber}")]
        public async Task<List<LabelWiseInvestmentDTO>> GetFundWiseSummary(string folionumber)
        {
            return await _service.GetFundSummaryForFolioAsync(folionumber);
        }

        [HttpGet("FundWiseSummaryCloud/{folionumber}")]
        public async Task<List<LabelWiseInvestmentDTO>> GetFundWiseSummaryFromCloud(string folionumber)
        {
            return await _service.GetFundSummaryForFolioAsyncFromCloudAsync(folionumber);
        }

        [HttpGet("SummaryCloud/{folionumber}")]
        public async Task<DashboardSummaryDTO> GetDashboard(string FolioNumber)
        {
            return await _service.GetDashboardSummaryFromCloud(FolioNumber);
        }
    }
}
