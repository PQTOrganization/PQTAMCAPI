using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.Controller.Onboarding
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService _areaService;

        public AreaController(IAreaService areaService, IMapper mapper)
        {
            _areaService = areaService;
        }

        // GET: api/Area
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Area>>> GetArea()
        {
            var areas = await _areaService.GetAllAsync();
            return areas.ToList();
        }


        [HttpGet("byCity/{id}")]
        public async Task<ActionResult<IEnumerable<Area>>> GetCityByCountry(int id)
        {
            var areas = await _areaService.GetAreasByCity(id);
            if (areas == null)
            {
                return NotFound();
            }

            return areas;
        }

        // GET: api/Area/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Area>> GetArea(int id)
        {
            var area = await _areaService.FindAsync(id);

            if (area == null)
            {
                return NotFound();
            }

            return area;
        }


        // PUT: api/Area/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Area>> PutArea(int id, Area area)
        {
            if (id != area.AreaId)
            {
                return BadRequest();
            }

            Area updatedArea = await _areaService.UpdateArea(id, area);
            return updatedArea;
        }

        // POST: api/Area
        [HttpPost]
        public async Task<ActionResult<Area>> PostArea(Area area)
        {
            Area insertedArea = await _areaService.InsertArea(area);
            return insertedArea;
        }

        // DELETE: api/Area/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteArea(int id)
        {

            var deleted = await _areaService.DeleteArea(id);
            return deleted;
        }
    }
}
