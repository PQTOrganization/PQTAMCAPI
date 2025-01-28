using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PQAMCAPI.Interfaces.Services;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Controller
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "PQAMCAuthScheme")]
    [ApiController]
    public class FundController : ControllerBase
    {
        private readonly IFundService _service;

        public FundController(IFundService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<FundDTO>> Get()
        {
            return await _service.GetFundsAsync();
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<FundDTO>> Get(int id)
        {
            return await _service.GetFundsByAccountCategoryAsync(id);
        }

        [HttpGet("Cloud")]
        public async Task<IEnumerable<FundDTO>> GetFromCloud()
        {
            return await _service.GetFundsFromCloudAsync();
        }

        [HttpGet("Cloud/{id}")]
        public async Task<IEnumerable<FundDTO>> GetFromCloud(int id)
        {
            return await _service.GetFundsFromCloudAsync();
        }

        [HttpGet("banks")]
        public async Task<IEnumerable<FundBankDTO>> GetFundsWithBanks()
        {
            return await _service.GetAllFundsWithBankAsync();
        }

        [HttpGet("banks/cloud")]
        public async Task<IEnumerable<FundBankDTO>> GetFundsWithBanksFromCloud()
        {
            return await _service.GetAllFundsWithBankFromCloudAsync();
        }
    }
}
