using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IIBFTService
    {
        Task<IBFTResponseDTO> GetTitle(IBFTRequestDTO Data);
    }
}
