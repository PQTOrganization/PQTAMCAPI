using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.Controller.Onboarding
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeSourceController : ControllerBase
    {
        private readonly IIncomeSourceService _incomeSourceService;

        public IncomeSourceController(IIncomeSourceService incomeSourceService, IMapper mapper)
        {
            _incomeSourceService = incomeSourceService;
        }


        // GET: api/SourceIncome
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IncomeSource>>> GetSourceIncome()
        {
            var sourceIncomes = await _incomeSourceService.GetAllAsync();
            return sourceIncomes.ToList();
        }

        // GET: api/SourceIncome/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IncomeSource>> GetSourceIncome(int id)
        {
            var sourceIncome = await _incomeSourceService.FindAsync(id);

            if (sourceIncome == null)
            {
                return NotFound();
            }

            return sourceIncome;
        }


        // PUT: api/SourceIncome/5
        [HttpPut("{id}")]
        public async Task<ActionResult<IncomeSource>> PutSourceIncome(int id, IncomeSource sourceIncome)
        {
            if (id != sourceIncome.IncomeSourceId)
            {
                return BadRequest();
            }

            IncomeSource updatedSourceIncome = await _incomeSourceService.UpdateIncomeSource(id, sourceIncome);
            return updatedSourceIncome;
        }

        // POST: api/SourceIncome
        [HttpPost]
        public async Task<ActionResult<IncomeSource>> PostSourceIncome(IncomeSource sourceIncome)
        {
            IncomeSource insertedSourceIncome = await _incomeSourceService.InsertIncomeSource(sourceIncome);
            return insertedSourceIncome;
        }

        // DELETE: api/SourceIncome/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteSourceIncome(int id)
        {
            var deleted = await _incomeSourceService.DeleteIncomeSource(id);
            return deleted;
        }
    }
}
