using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PQAMCAPI.Interfaces.Services;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Controller
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "PQAMCAuthScheme")]
    [ApiController]
    public class InvestmentController : ControllerBase
    {
        private readonly IInvestmentService _service;

        public InvestmentController(IInvestmentService service)
        {
            _service = service;
        }

        [HttpGet("fundwiseposition/{folioNumber}")]
        public async Task<IEnumerable<FundPositionDTO>> GetFundWisePositionForFolio(string folioNumber)
        {
            return await _service.GetFundWisePositionForFolioAsync(folioNumber);
        }

        [HttpGet("fundwiseposition/cloud/{folioNumber}")]
        public async Task<IEnumerable<FundPositionDTO>> GetFundWisePositionForFolioFromCloud(string folioNumber)
        {
            return await _service.GetFundWisePositionForFolioFromCloudAsync(folioNumber);
        }

        [HttpGet("allfundwiseposition/{folioNumber}")]
        public async Task<IEnumerable<FundPositionDTO>> GetAllFundWisePositionForFolio(string folioNumber)
        {
            return await _service.GetAllFundWisePositionForFolioAsync(folioNumber);
        }


        [HttpGet("allfundwiseposition/cloud/{folioNumber}")]
        public async Task<IEnumerable<FundPositionDTO>> GetAllFundWisePositionForFolioFromCloud(string folioNumber)
        {
            return await _service.GetAllFundWisePositionForFolioFromCloudAsync(folioNumber);
        }
    }
}
