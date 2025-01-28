using PQAMCAPI.Models;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IITMindsRequestDBService
    {
        Task<List<ITMindsRequest>> GetAllAsync();
        Task<ITMindsRequest> InsertITMindsRequest(ITMindsRequest Data);        
    }
}
