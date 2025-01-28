using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.Controller.Onboarding
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
        }


        // GET: api/City
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCity()
        {
            var cities = await _cityService.GetAllAsync();
            return cities.ToList();
        }

        // GET: api/City/5
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {
            var city = await _cityService.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        [HttpGet("byCountry/{id}")]
        public async Task<ActionResult<IEnumerable<City>>> GetCityByCountry(int id)
        {
            var cities = await _cityService.GetCitiesByCountry(id);

            if (cities == null)
            {
                return NotFound();
            }

            return cities;
        }

        // PUT: api/City/5
        [HttpPut("{id}")]
        public async Task<ActionResult<City>> PutCity(int id, City city)
        {
            if (id != city.CityId)
            {
                return BadRequest();
            }

            City updatedCity = await _cityService.UpdateCity(id, city);
            return updatedCity;
        }

        // POST: api/City
        [HttpPost]
        public async Task<ActionResult<City>> PostCity(City city)
        {
            City insertedCity = await _cityService.InsertCity(city);
            return insertedCity;
        }

        // DELETE: api/City/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteCity(int id)
        {

            var deleted = await _cityService.DeleteCity(id);
            return deleted;
        }
    }
}
