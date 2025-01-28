using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PQAMCAPI.Interfaces.Services;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Controller
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "PQAMCAuthScheme")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _service;

        public TransactionsController(ITransactionService service)
        {
            _service = service;
        }

        [HttpGet("ForFolio/{folionumber}")]
        public async Task <IEnumerable<TransactionDTO>> Get(string folionumber)
        {
            return await _service.GetTransactionsForFolioAsync(folionumber);
        }

        [HttpGet("ForFolio/Cloud/{folionumber}")]
        public async Task<IEnumerable<TransactionDTO>> GetFromCloud(string folionumber)
        {
            return await _service.GetTransactionsForFolioFromCloudAsync(folionumber);
        }

        [HttpGet("Pending/ForFolio/{folionumber}")]
        public async Task<IEnumerable<PendingTransactionDTO>> GetPendingTransactions(string folionumber)
        {
            return await _service.GetPendingTransactionsForFolioAsync(folionumber);
        }
    }
}
