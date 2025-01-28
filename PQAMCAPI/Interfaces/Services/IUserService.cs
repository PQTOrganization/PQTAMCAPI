using Microsoft.AspNetCore.Mvc;
using PQAMCAPI.Models;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();
        Task<User> FindAsync(int userId);

        Task<UserAuthData> Login(LoginData model);
        Task<bool> Logout();

        Task<User> InsertUser(User user);
        Task<User> UpdateUser(int userId, User user);
        Task<int> DeleteUser(int userId);
        Task<User> GetUserWith(string email, string number);
        Task<AMCUserDTO> GetAMCUser(string number);
        Task<bool> UpdateOTP(int UserId, string otp);
    }
}
