using PQAMCAPI.Models;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IUserApplicationDiscrepancyDBService
    {
        Task<UserApplicationDiscrepancy> FindAsync(int UserApplicationId);
        Task<UserApplicationDiscrepancy> GetDiscrepancyForUserApplication(int UserApplicationId);
        Task<UserApplicationDiscrepancy> InsertApplicationDiscrepancy(UserApplicationDiscrepancy Discrepancy);
    }
}
