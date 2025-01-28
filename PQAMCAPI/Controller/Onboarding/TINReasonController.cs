using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using PQAMCAPI.Interfaces.Services;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Controller.Onboarding
{
    [Route("api/[controller]")]
    [ApiController]
    public class TINReasonController : ControllerBase
    {
        private readonly ITINReasonService _service;

        public TINReasonController(ITINReasonService service, IMapper mapper)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<TINReasonDTO>>> GetTinReason()
        {
            return await _service.GetAllAsync();
        }
    }
}
