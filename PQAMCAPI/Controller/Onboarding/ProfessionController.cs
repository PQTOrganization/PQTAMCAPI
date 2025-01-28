using Microsoft.AspNetCore.Mvc;

using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.Controller.Onboarding
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionController : ControllerBase
    {
        private readonly IProfessionService _professionService;

        public ProfessionController(IProfessionService professionService)
        {
            _professionService = professionService;
        }

        // GET: api/Profession
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profession>>> GetProfession()
        {
            var professions = await _professionService.GetAllAsync();
            return professions.ToList();
        }

        // GET: api/Profession/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Profession>> GetProfession(int id)
        {
            var profession = await _professionService.FindAsync(id);

            if (profession == null)
            {
                return NotFound();
            }

            return profession;
        }


        // PUT: api/Profession/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Profession>> PutProfession(int id, Profession profession)
        {
            if (id != profession.ProfessionId)
            {
                return BadRequest();
            }

            Profession updatedProfession = await _professionService.UpdateProfession(id, profession);
            return updatedProfession;
        }

        // POST: api/Profession
        [HttpPost]
        public async Task<ActionResult<Profession>> PostProfession(Profession profession)
        {
            Profession insertedProfession = await _professionService.InsertProfession(profession);
            return insertedProfession;
        }

        // DELETE: api/Profession/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteProfession(int id)
        {

            var deleted = await _professionService.DeleteProfession(id);
            return deleted;
        }
    }
}
