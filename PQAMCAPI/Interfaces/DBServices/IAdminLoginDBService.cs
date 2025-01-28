using PQAMCAPI.Models;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IAdminLoginDBService
    {
        Task<bool> Login(string Username, string Password);
    }
}
