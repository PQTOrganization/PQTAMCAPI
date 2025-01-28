using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.Controller.Onboarding
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService, IMapper mapper)
        {
            _countryService = countryService;
        }

        // GET: api/Country
        [HttpGet]
        public async Task<ActionResult<List<Country>>> GetCountry()
        {
            return await _countryService.GetAllAsync();
        }

        // GET: api/Country/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            var country = await _countryService.FindAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            return country;
        }

        // PUT: api/Country/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Country>> PutCountry(int id, Country country)
        {
            if (id != country.CountryId)
            {
                return BadRequest();
            }

            Country updatedCountry = await _countryService.UpdateCountry(id, country);
            return updatedCountry;
        }

        // POST: api/Country
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(Country country)
        {
            Country insertedCountry = await _countryService.InsertCountry(country);
            return insertedCountry;
        }

        // DELETE: api/Country/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteCountry(int id)
        {
            var deleted = await _countryService.DeleteCountry(id);
            return deleted;
        }

    }
}
