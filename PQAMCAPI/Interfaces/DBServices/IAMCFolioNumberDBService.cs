using PQAMCAPI.Models;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IAMCFolioNumberDBService
    {
        Task<List<AMCFolioNumber>> GetAllAsync();
    }
}
