using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PQAMCAPI.Interfaces.Services;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Controller
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "PQAMCAuthScheme")]
    [ApiController]
    public class InvestmentRequestController : ControllerBase
    {
        private readonly IInvestmentRequestService _service;

        public InvestmentRequestController(IInvestmentRequestService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<InvestmentRequestDTO>> Get()
        {
            return await _service.GetAllInvestmentRequestsAsync();
        }

        [HttpGet("forfolio/{folionumber}")]
        public async Task<IEnumerable<InvestmentRequestDTO>> GetAllForFolio(string folionumber)
        {
            return await _service.GetAllInvestmentRequestsForFolioAsync(folionumber);
        }

        [HttpGet("{id}")]
        public async Task<InvestmentRequestDTO> Get(int id)
        {
            return await _service.GetInvestmentRequestAsync(id);
        }
        
        [HttpPost]
        public async Task<InvestmentRequestDTO> Post([FromBody] InvestmentRequestDTO Request)
        {
            return await _service.InsertInvestmentRequestAsync(Request);
        }

        [HttpPost("initial")]
        public async Task<InvestmentRequestDTO> PostInitialInvestment([FromBody] InitialInvestmentRequestDTO Request)
        {
            return await _service.InsertInitialInvestmentRequestAsync(Request);
        }

        [HttpPut("updatestatus")]
        public async Task<ActionResult> UpdateRequestStatus([FromBody] InvestmentRequestUpdateStatusDTO Request)
        {
            await _service.UpdateInvestmentRequestStatusAsync(Request.InvestmentRequestId,
                                                              Request.RequestStatus);

            return Ok("Status updated successfully");
        }

        [HttpPut("testsubmitsubsale/{id}")]
        public async Task SubmitSubSale(int id)
        {
            await _service.SubmitSubSaleRequestToCloud(id);
        }
    }
}
