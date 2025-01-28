using PQAMCAPI.Models;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IContactOwnerShipService
    {
        Task<List<ContactOwnerShip>> GetAllAsync();
        Task<ContactOwnerShip> FindAsync(int contactOwnerShipId);
        Task<ContactOwnerShip> InsertContactOwnerShip(ContactOwnerShip contactOwnerShip);
        Task<ContactOwnerShip> UpdateContactOwnerShip(int contactOwnerShipId, ContactOwnerShip contactOwnerShip);
        Task<int> DeleteContactOwnerShip(int contactOwnerShipId);



    }
}
