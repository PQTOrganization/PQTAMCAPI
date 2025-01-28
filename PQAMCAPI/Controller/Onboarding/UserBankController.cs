using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

namespace PQAMCAPI.Controller.Onboarding
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "PQAMCAuthScheme")]
    [ApiController]
    public class UserBankController : ControllerBase
    {
        private readonly IUserBankService _userbankService;

        public UserBankController(IUserBankService userbankService, IMapper mapper)
        {
            _userbankService = userbankService;
        }


        // GET: api/Bank
        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<UserBank>>> GetUserBank(int id)
        {
            var banks = await _userbankService.GetAllForUser(id);
            return banks.ToList();
        }

        // GET: api/Bank/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserBank>> GetBank(int id)
        {
            var bank = await _userbankService.FindAsync(id);

            if (bank == null)
            {
                return NotFound();
            }

            return bank;
        }


        // PUT: api/Bank/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserBank>> PutBank(int id, UserBank bank)
        {
            if (id != bank.UserBankId)
            {
                return BadRequest();
            }

            UserBank updatedBank = await _userbankService.UpdateBank(id, bank);
            return updatedBank;
        }

        // POST: api/Bank
        [HttpPost]
        public async Task<ActionResult<UserBank>> PostBank(UserBank bank)
        {
            UserBank insertedBank = await _userbankService.InsertBank(bank);
            return insertedBank;
        }

        // DELETE: api/Bank/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserBank>> DeleteBank(int UserApplicationId, int? BankId)
        {

            var deleted = await _userbankService.DeleteBank(UserApplicationId, BankId);
            return deleted;
        }
    }
}
