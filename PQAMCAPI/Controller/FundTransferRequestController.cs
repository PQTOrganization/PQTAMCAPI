using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PQAMCAPI.Interfaces.Services;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Controller
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "PQAMCAuthScheme")]
    [ApiController]
    public class FundTransferRequestController : ControllerBase
    {
        private readonly IFundTransferRequestService _service;

        public FundTransferRequestController(IFundTransferRequestService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<FundTransferRequestDTO>> Get()
        {
            return await _service.GetAllFundTransferRequestsAsync();
        }

        [HttpGet("forfolio/{folionumber}")]
        public async Task<IEnumerable<FundTransferRequestDTO>> GetAllForFolio(string folionumber)
        {
            return await _service.GetAllFundTransferRequestsForFolioAsync(folionumber);
        }

        [HttpGet("{id}")]
        public async Task<FundTransferRequestDTO> Get(int id)
        {
            return await _service.GetFundTransferRequestAsync(id);
        }
        
        [HttpPost]
        public async Task<FundTransferRequestDTO> Post([FromBody] FundTransferRequestDTO Request)
        {
            return await _service.InsertFundTransferRequestAsync(Request);
        }

        [HttpPut("updatestatus")]
        public async Task<ActionResult> UpdateRequestStatus([FromBody] FundTransferRequestDTO Request)
        {
            await _service.UpdateFundTransferRequestStatusAsync(Request.FundTransferRequestId,
                                                                Request.RequestStatus);
            return Ok("Status updated successfully");
        }

        [HttpPut("testsubmitftcloud/{id}")]
        public async Task SubmitFundTransferToCloud(int id)
        {
            await _service.SubmitFundTransferRequesToCloud(id);
        }
    }
}
