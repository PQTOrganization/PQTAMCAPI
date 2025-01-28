using Microsoft.AspNetCore.Mvc;

using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.Controller.Onboarding
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidentialStatusController : ControllerBase
    {
        private readonly IResidentialStatusService _residentialStatusService;

        public ResidentialStatusController(IResidentialStatusService residentialStatusService)
        {
            _residentialStatusService = residentialStatusService;
        }

        // GET: api/ResidentialStatus
        [HttpGet]
        public async Task<ActionResult<List<ResidentialStatus>>> GetResidentialStatus()
        {
            return await _residentialStatusService.GetAllAsync();
        }

        // GET: api/ResidentialStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResidentialStatus>> GetResidentialStatus(int id)
        {
            var residentialStatus = await _residentialStatusService.FindAsync(id);

            if (residentialStatus == null)
            {
                return NotFound();
            }

            return residentialStatus;
        }

        // PUT: api/ResidentialStatus/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ResidentialStatus>> PutResidentialStatus(int id, ResidentialStatus residentialStatus)
        {
            if (id != residentialStatus.ResidentialStatusId)
            {
                return BadRequest();
            }

            ResidentialStatus updatedResidentialStatus = await _residentialStatusService.UpdateResidentialStatus(id, residentialStatus);
            return updatedResidentialStatus;
        }

        // POST: api/ResidentialStatus
        [HttpPost]
        public async Task<ActionResult<ResidentialStatus>> PostResidentialStatus(ResidentialStatus residentialStatus)
        {
            ResidentialStatus insertedResidentialStatus = await _residentialStatusService.InsertResidentialStatus(residentialStatus);
            return insertedResidentialStatus;
        }

        // DELETE: api/ResidentialStatus/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteResidentialStatus(int id)
        {
            var deleted = await _residentialStatusService.DeleteResidentialStatus(id);
            return deleted;
        }

    }
}
