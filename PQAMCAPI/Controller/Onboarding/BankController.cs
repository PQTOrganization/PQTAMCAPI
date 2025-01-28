using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.Controller.Onboarding
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBankService _bankService;

        public BankController(IBankService bankService, IMapper mapper)
        {
            _bankService = bankService;
        }


        // GET: api/Bank
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bank>>> GetBank()
        {
            var banks = await _bankService.GetAllAsync();
            return banks.ToList();
        }

        // GET: api/Bank/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bank>> GetBank(int id)
        {
            var bank = await _bankService.FindAsync(id);

            if (bank == null)
            {
                return NotFound();
            }

            return bank;
        }


        // PUT: api/Bank/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Bank>> PutBank(int id, Bank bank)
        {
            if (id != bank.BankId)
            {
                return BadRequest();
            }

            Bank updatedBank = await _bankService.UpdateBank(id, bank);
            return updatedBank;
        }

        // POST: api/Bank
        [HttpPost]
        public async Task<ActionResult<Bank>> PostBank(Bank bank)
        {
            Bank insertedBank = await _bankService.InsertBank(bank);
            return insertedBank;
        }

        // DELETE: api/Bank/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteBank(int id)
        {

            var deleted = await _bankService.DeleteBank(id);
            return deleted;
        }
    }
}
