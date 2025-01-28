using Microsoft.AspNetCore.Mvc;

using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.Controller.Onboarding
{
    [Route("api/[controller]")]
    [ApiController]
    public class OccupationController : ControllerBase
    {
        private readonly IOccupationService _occupationService;

        public OccupationController(IOccupationService occupationService)
        {
            _occupationService = occupationService;
        }


        // GET: api/Occupation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Occupation>>> GetOccupation()
        {
            var occupations = await _occupationService.GetAllAsync();
            return occupations.ToList();
        }

        // GET: api/Occupation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Occupation>> GetOccupation(int id)
        {
            var occupation = await _occupationService.FindAsync(id);

            if (occupation == null)
            {
                return NotFound();
            }

            return occupation;
        }


        // PUT: api/Occupation/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Occupation>> PutOccupation(int id, Occupation occupation)
        {
            if (id != occupation.OccupationId)
            {
                return BadRequest();
            }

            Occupation updatedOccupation = await _occupationService.UpdateOccupation(id, occupation);
            return updatedOccupation;
        }

        // POST: api/Occupation
        [HttpPost]
        public async Task<ActionResult<Occupation>> PostOccupation(Occupation occupation)
        {
            Occupation insertedOccupation = await _occupationService.InsertOccupation(occupation);
            return insertedOccupation;
        }

        // DELETE: api/Occupation/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteOccupation(int id)
        {

            var deleted = await _occupationService.DeleteOccupation(id);
            return deleted;
        }
    }
}
