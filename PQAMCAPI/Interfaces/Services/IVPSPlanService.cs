using PQAMCAPI.Models;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IVPSPlanService
    {
        Task<List<VPSPlanDTO>> GetAllAsync();
        
    }
}
