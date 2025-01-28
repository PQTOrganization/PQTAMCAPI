using PQAMCAPI.Models;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IGenderService
    {
        Task<List<Gender>> GetAllAsync();
        Task<Gender> FindAsync(int GenderId);
        Task<Gender> InsertGender(Gender Gender);
        Task<Gender> UpdateGender(int GenderId, Gender Gender);
        Task<int> DeleteGender(int GenderId);



    }
}
