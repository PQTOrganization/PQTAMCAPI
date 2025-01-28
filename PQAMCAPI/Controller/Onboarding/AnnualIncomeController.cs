
using Microsoft.AspNetCore.Mvc;
using PQAMCAPI.Models;
using AutoMapper;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.Controller.Onboarding
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnualIncomeController : ControllerBase
    {
        private readonly IAnnualIncomeService _annualIncomeService;
        private readonly IMapper _mapper;
        public AnnualIncomeController(IAnnualIncomeService annualIncomeService, IMapper mapper)
        {
            _annualIncomeService = annualIncomeService;
            _mapper = mapper;
        }


        // GET: api/AnnualIncome
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnnualIncome>>> GetAnnualIncome()
        {
            var annualIncomelist = await _annualIncomeService.GetAllAsync();
            return annualIncomelist.ToList();
        }

        // GET: api/AnnualIncome/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnnualIncome>> GetAnnualIncome(int id)
        {
            var annualIncome = await _annualIncomeService.FindAsync(id);

            if (annualIncome == null)
            {
                return NotFound();
            }

            return annualIncome;
        }

        // PUT: api/AnnualIncome/5
        [HttpPut("{id}")]
        public async Task<ActionResult<AnnualIncome>> PutAnnualIncome(int id, AnnualIncome annualIncome)
        {
            if (id != annualIncome.AnnualIncomeId)
            {
                return BadRequest();
            }

            AnnualIncome updatedAnnualIncome = await _annualIncomeService.UpdateAnnualIncome(id, annualIncome);
            return updatedAnnualIncome;
        }

        // POST: api/AnnualIncome
        [HttpPost]
        public async Task<ActionResult<AnnualIncome>> PostAnnualIncome(AnnualIncome annualIncome)
        {
            AnnualIncome insertedAnnualIncome = await _annualIncomeService.InsertAnnualIncome(annualIncome);
            return insertedAnnualIncome;
        }

        // DELETE: api/AnnualIncome/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteAnnualIncome(int id)
        {

            var deleted = await _annualIncomeService.DeleteAnnualIncome(id);
            return deleted;
        }
    }
}
