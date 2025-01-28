using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PQAMCAPI.Interfaces.Services;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Controller.Onboarding
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "PQAMCAuthScheme")]
    [ApiController]
    public class IBFTController : ControllerBase
    {
        private readonly IIBFTService _service;

        public IBFTController(IIBFTService service)
        {
            _service = service;
        }

        [HttpPost("fetchtitle")]
        public async Task<IBFTResponseDTO> GetTitle([FromBody] IBFTRequestDTO Data)
        {
            return await _service.GetTitle(Data);
        }
    }
}
