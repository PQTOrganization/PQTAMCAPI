using PQAMCAPI.Models;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IITMindsService
    {
        Task<string> GetAllAccountStatementsAsync();
       
    }
}
