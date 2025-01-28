using PQAMCAPI.Models;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IUserDBService
    {
        Task<User> FindAsync(int userId);
        Task<List<User>> GetAllAsync();
        Task<User> InsertUser(User user);
        Task<User> UpdateUser(int userId, User user);
        Task<User> GetUserWith(string email, string number);
        Task<AMCUserDTO> GetAMCUser(string number);
        Task<bool> IfUserExist(User user);
        Task<bool> UpdateOTP(int UserId, string otp);
        Task<List<UserFolioDTO>> GetFoliosOfUser(string MobileNumber);
        Task<bool> UpdateOTPRetryAttempts(int UserId, int IncorrectOTPRetryAttempts, string UserAccountStatus);
    }
}
