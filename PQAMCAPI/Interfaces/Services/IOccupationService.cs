using PQAMCAPI.Models;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IOccupationService
    {
        Task<List<Occupation>> GetAllAsync();
        Task<Occupation> FindAsync(int occupationId);
        Task<Occupation> InsertOccupation(Occupation occupation);
        Task<Occupation> UpdateOccupation(int occupationId, Occupation occupation);
        Task<int> DeleteOccupation(int occupationId);



    }
}
