using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PQAMCAPI.Interfaces.Services;
using PQAMCClasses.CloudDTOs;

namespace PQAMCAPI.Controller
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "PQAMCAuthScheme")]
    [ApiController]
    public class ITMindsController : ControllerBase
    {
        private readonly IITMindsService _accountService;
        private readonly ICloudService _service;

        public ITMindsController(ICloudService service, IITMindsService accountService)
        {
            _service = service;
            _accountService = accountService;
        }

        [HttpGet("foliolist")]
        public async Task<List<FolioDTO>> GetFolioList()
        {
            return await _service.GetFolioList();
        }

        [HttpGet("AMCFundNAVList")]
        public async Task<List<AMCFundNAVDTO>> GetAMCFundNAVList()
        {
            return await _service.GetAMCFundNAVList();
        }

        [HttpPost("AccountStatementCIS")]
        public async Task<List<InvestorAccountTransactionDTO>> GetAccountStatementCIS(GetAccountStatementCISRequestDTO ActStmtReq)
        {
            return await _service.GetAccountStatementCIS(ActStmtReq);
        }

        [HttpGet("bankList")]
        public async Task<List<BankNameDTO>> GetBanksList()
        {
            return await _service.GetBanksList();
        }
        
        [HttpPost("AccountStatementReport")]
        public async Task<AccountStatementReportDTO> GetAccountStatementReport(GetAccountStatementReportRequestDTO ActStmtRepReq)
        {
            return await _service.GetAccountStatementReport(ActStmtRepReq);
        }

        [HttpPost("AccountStatement")]
        public async Task<AccountStatementReportDTO> GetAccountStatementCISVPSReport(GetAccountStatementCISVPSReportRequestDTO ActStmtRepReq)
        {
            return await _service.GetAccountStatementCISVPSReport(ActStmtRepReq);
        }

        [HttpPost("AccountStatementVPS")]
        public async Task<List<InvestorAccountTransactionDTO>> GetAccountStatementVPS(GetAccountStatementVPSRequestDTO ActStmtReq)
        {
            return await _service.GetAccountStatementVPS(ActStmtReq);
        }

        [HttpPost("fundBankAccounts")]
        public async Task<List<FundBankAccountDTO>> GetFundBankAccounts(GetFundBankAccountsRequestDTO Request)
        {
            return await _service.GetFundBankAccounts(Request);
        }

        [HttpPost("getCISFundList")]
        public async Task<List<CISFundDTO>> GetCISFundList(GetCISFundListRequestDTO Request)
        {
            return await _service.GetCISFundList(Request);
        }

        [HttpGet("vpsPlanSequence")]
        public async Task<List<int>> GetVPSPlanSequence()
        {
            return await _service.GetVPSPlanSequence();
        }

        [HttpGet("FetchAccountStatement")]
        public async Task<ActionResult<string>> Get()
        {
            return await _accountService.GetAllAccountStatementsAsync();
        }
    }
}
