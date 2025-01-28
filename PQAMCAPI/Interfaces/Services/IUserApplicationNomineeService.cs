using PQAMCClasses.CloudDTOs;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IUserApplicationNomineeService
    {
        Task<UserApplicationNomineeDTO> GetUserApplicationNominee(int UserApplicationNomineeID);
        Task<List<UserApplicationNomineeDTO>> GetNomineesForUserApplication(int UserApplicationNomineeID);
        Task<List<UserApplicationNomineeDTO>> InsertUserApplicationNominee(List<UserApplicationNomineeDTO> Data);
        Task<UserApplicationNomineeDTO> InsertUserApplicationNominee(UserApplicationNomineeDTO Data);
        Task<UserApplicationNomineeDTO> UpdateUserApplicationNominee(int UserApplicationNomineeId, UserApplicationNomineeDTO Data);
        Task<int> DeleteUserApplicationNominee(int UserApplicationNomineeId);
        Task<int> DeleteNomineeByUserApplicationID(int UserApplicationId);
    }
}
