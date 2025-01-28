using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.Services.Domain
{
    public class ContactOwnerShipService : IContactOwnerShipService
    {
        const string PACKAGE_NAME = "AMC_CONTACT_OWNERSHIP_PKG";

        private readonly IStoreProcedureService _spService;

        public ContactOwnerShipService(IStoreProcedureService spService, PQAMCAPIContext dbContext)
        {
            _spService = spService;
        }

        public Task<int> DeleteContactOwnerShip(int contactOwnerShipServiceId)
        {
            var rowCount = _spService.DeleteSP<ContactOwnerShip>(PACKAGE_NAME + ".del_contact_ownership", contactOwnerShipServiceId);
            return rowCount;
        }

        public Task<ContactOwnerShip> FindAsync(int contactOwnerShipServiceId)
        {
            var contactOwnerShipService = _spService.GetSP<ContactOwnerShip>(PACKAGE_NAME + ".get_contact_ownership", contactOwnerShipServiceId);
            return contactOwnerShipService;
        }

        public Task<List<ContactOwnerShip>> GetAllAsync()
        {
            var contactOwnerShipServices = _spService.GetAllSP<ContactOwnerShip>(PACKAGE_NAME + ".get_contact_ownerships", -1);
            return contactOwnerShipServices;
        }

        public Task<ContactOwnerShip> InsertContactOwnerShip(ContactOwnerShip contactOwnerShipService)
        {
            throw new NotImplementedException();
        }

        public Task<ContactOwnerShip> UpdateContactOwnerShip(int contactOwnerShipServiceId, ContactOwnerShip contactOwnerShipService)
        {
            throw new NotImplementedException();
        }
    }
}
