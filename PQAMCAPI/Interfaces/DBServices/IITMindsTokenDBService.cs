using PQAMCAPI.Models;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IITMindsTokenDBService
    {
        Task<List<ITMindsToken>> GetAllAsync();
        Task<ITMindsToken> InsertITMindsToken(ITMindsToken Data);
        Task<string> GetITMindsCredentials(string KeyFor);
    }
}
