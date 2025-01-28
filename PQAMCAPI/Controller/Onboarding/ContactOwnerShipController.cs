using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.Controller.Onboarding
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactOwnerShipController : ControllerBase
    {
        private readonly IContactOwnerShipService _contactOwnerShipService;

        public ContactOwnerShipController(IContactOwnerShipService contactOwnerShipService, IMapper mapper)
        {
            _contactOwnerShipService = contactOwnerShipService;
        }


        // GET: api/ContactOwnerShip
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactOwnerShip>>> GetContactOwnerShip()
        {
            var contactOwnerShips = await _contactOwnerShipService.GetAllAsync();
            return contactOwnerShips.ToList();
        }

        // GET: api/ContactOwnerShip/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactOwnerShip>> GetContactOwnerShip(int id)
        {
            var contactOwnerShip = await _contactOwnerShipService.FindAsync(id);

            if (contactOwnerShip == null)
            {
                return NotFound();
            }

            return contactOwnerShip;
        }


        // PUT: api/ContactOwnerShip/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ContactOwnerShip>> PutContactOwnerShip(int id, ContactOwnerShip contactOwnerShip)
        {
            if (id != contactOwnerShip.ContactOwnerShipId)
            {
                return BadRequest();
            }

            ContactOwnerShip updatedContactOwnerShip = await _contactOwnerShipService.UpdateContactOwnerShip(id, contactOwnerShip);
            return updatedContactOwnerShip;
        }

        // POST: api/ContactOwnerShip
        [HttpPost]
        public async Task<ActionResult<ContactOwnerShip>> PostContactOwnerShip(ContactOwnerShip contactOwnerShip)
        {
            ContactOwnerShip insertedContactOwnerShip = await _contactOwnerShipService.InsertContactOwnerShip(contactOwnerShip);
            return insertedContactOwnerShip;
        }

        // DELETE: api/ContactOwnerShip/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteContactOwnerShip(int id)
        {

            var deleted = await _contactOwnerShipService.DeleteContactOwnerShip(id);
            return deleted;
        }
    }
}
