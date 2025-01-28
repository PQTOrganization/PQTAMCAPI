using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.Controller.Onboarding
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountCategoryController : ControllerBase
    {
        private readonly IAccountCategoryService _accountCategoryService;
        private readonly IMapper _mapper;
        public AccountCategoryController(IAccountCategoryService accountCategoryService, IMapper mapper)
        {
            _accountCategoryService = accountCategoryService;
            _mapper = mapper;
        }


        // GET: api/AccountCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountCategory>>> GetAccountCategory()
        {
            var cities = await _accountCategoryService.GetAllAsync();
            return cities.ToList();
        }

        // GET: api/AccountCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountCategory>> GetAccountCategory(int id)
        {
            var accountCategory = await _accountCategoryService.FindAsync(id);

            if (accountCategory == null)
            {
                return NotFound();
            }

            return accountCategory;
        }


        // PUT: api/AccountCategory/5
        [HttpPut("{id}")]
        public async Task<ActionResult<AccountCategory>> PutAccountCategory(int id, AccountCategory accountCategory)
        {
            if (id != accountCategory.AccountCategoryId)
            {
                return BadRequest();
            }

            AccountCategory updatedAccountCategory = await _accountCategoryService.UpdateAccountCategory(id, accountCategory);
            return updatedAccountCategory;

        }

        // POST: api/AccountCategory
        [HttpPost]
        public async Task<ActionResult<AccountCategory>> PostAccountCategory(AccountCategory accountCategory)
        {
            AccountCategory insertedAccountCategory = await _accountCategoryService.InsertAccountCategory(accountCategory);
            return insertedAccountCategory;
        }

        // DELETE: api/AccountCategory/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteAccountCategory(int id)
        {

            var deleted = await _accountCategoryService.DeleteAccountCategory(id);
            return deleted;
        }
    }
}
