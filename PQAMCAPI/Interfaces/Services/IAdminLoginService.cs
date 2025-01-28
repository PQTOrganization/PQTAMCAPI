using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IAdminLoginService
    {
        Task<AdminAuthData> Login(string username, string password);
    }
}
