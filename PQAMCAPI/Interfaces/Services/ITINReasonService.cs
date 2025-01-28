using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface ITINReasonService
    {
        Task<List<TINReasonDTO>> GetAllAsync();
    }
}
