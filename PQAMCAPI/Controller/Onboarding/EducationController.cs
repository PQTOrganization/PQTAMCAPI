using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.Controller.Onboarding
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly IEducationService _educationService;

        public EducationController(IEducationService educationService, IMapper mapper)
        {
            _educationService = educationService;
        }


        // GET: api/Education
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Education>>> GetEducation()
        {
            var educations = await _educationService.GetAllAsync();
            return educations.ToList();
        }

        // GET: api/Education/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Education>> GetEducation(int id)
        {
            var education = await _educationService.FindAsync(id);

            if (education == null)
            {
                return NotFound();
            }

            return education;
        }


        // PUT: api/Education/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Education>> PutEducation(int id, Education education)
        {
            if (id != education.EducationId)
            {
                return BadRequest();
            }

            Education updatedEducation = await _educationService.UpdateEducation(id, education);
            return updatedEducation;
        }

        // POST: api/Education
        [HttpPost]
        public async Task<ActionResult<Education>> PostEducation(Education education)
        {
            Education insertedEducation = await _educationService.InsertEducation(education);
            return insertedEducation;
        }

        // DELETE: api/Education/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteEducation(int id)
        {

            var deleted = await _educationService.DeleteEducation(id);
            return deleted;
        }
    }
}
