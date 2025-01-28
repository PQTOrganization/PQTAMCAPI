using PQAMCAPI.Models;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IUserApplicationNomineeDBService
    {
        Task<UserApplicationNominee> FindAsync(int UserApplicationNomineeId);
        Task<List<UserApplicationNominee>> GetNomineesForUserApplication(int UserApplicationNomineeId);
        Task<UserApplicationNominee> InsertUserApplicationNominee(UserApplicationNominee Data);
        Task<UserApplicationNominee> UpdateUserApplicationNominee(int UserApplicationNomineeId, UserApplicationNominee Data);
        Task<int> DeleteUserApplicationNominee(int UserApplicationNomineeId);
        Task<int> DeleteNomineeByUserApplicationID(int UserApplicationId);
    }
}
