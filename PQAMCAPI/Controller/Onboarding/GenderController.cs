using Microsoft.AspNetCore.Mvc;

using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.Controller.Onboarding
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly IGenderService _genderService;

        public GenderController(IGenderService genderService)
        {
            _genderService = genderService;
        }


        // GET: api/Gender
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gender>>> GetGender()
        {
            var genders = await _genderService.GetAllAsync();
            return genders.ToList();
        }

        // GET: api/Gender/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gender>> GetGender(int id)
        {
            var gender = await _genderService.FindAsync(id);

            if (gender == null)
            {
                return NotFound();
            }

            return gender;
        }


        // PUT: api/Gender/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Gender>> PutGender(int id, Gender gender)
        {
            if (id != gender.GenderId)
            {
                return BadRequest();
            }

            Gender updatedGender = await _genderService.UpdateGender(id, gender);
            return updatedGender;
        }

        // POST: api/Gender
        [HttpPost]
        public async Task<ActionResult<Gender>> PostGender(Gender gender)
        {
            Gender insertedGender = await _genderService.InsertGender(gender);
            return insertedGender;
        }

        // DELETE: api/Gender/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteGender(int id)
        {

            var deleted = await _genderService.DeleteGender(id);
            return deleted;
        }
    }
}
