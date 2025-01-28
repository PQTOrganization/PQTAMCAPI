using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PQAMCAPI.Interfaces.Services;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Controller
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "PQAMCAuthScheme")]
    [ApiController]
    public class RedemptionRequestController : ControllerBase
    {
        private readonly IRedemptionRequestService _service;

        public RedemptionRequestController(IRedemptionRequestService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<RedemptionRequestDTO>> Get()
        {
            return await _service.GetAllRedemptionRequestsAsync();
        }

        [HttpGet("forfolio/{folionumber}")]
        public async Task<IEnumerable<RedemptionRequestDTO>> GetAllForFolio(string folionumber)
        {
            return await _service.GetAllRedemptionRequestsForFolioAsync(folionumber);
        }

        [HttpGet("{id}")]
        public async Task<RedemptionRequestDTO> Get(int id)
        {
            return await _service.GetRedemptionRequestAsync(id);
        }
        
        [HttpPost]
        public async Task<RedemptionRequestDTO> Post([FromBody] RedemptionRequestDTO Request)
        {
            return await _service.InsertRedemptionRequestAsync(Request);
        }

        [HttpPut("updatestatus")]
        public async Task<ActionResult> UpdateRequestStatus([FromBody] RedemptionRequestDTO Request)
        {
            await _service.UpdateRedemptionRequestStatusAsync(Request.RedemptionRequestId,
                                                              Request.RequestStatus);
            return Ok("Status updated successfully");
        }

        [HttpPut("testredemptioncloud/{id}")]
        public async Task SubmitRedemptionToCloud(int id)
        {
            await _service.SubmitRedemptionRequestToCloud(id);
        }

        [HttpGet("banks/{id}")]
        public async Task<FolioBankResponseDTO> GetFolioBankList(int id)
        {
            return await _service.GetFolioBankList(id);
        }
    }
}
