using Microsoft.AspNetCore.Mvc;
using PQAMCAPI.Interfaces.Services;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Controller.Onboarding
{
    [Route("api/[controller]")]
    [ApiController]
    public class VPSPlanController : ControllerBase
    {
        private readonly IVPSPlanService _vpsPlanService;

        public VPSPlanController(IVPSPlanService vpsPlanService)
        {
            _vpsPlanService = vpsPlanService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VPSPlanDTO>>> GetAllPlans()
        {
            return await _vpsPlanService.GetAllAsync();
        }

    }
}
