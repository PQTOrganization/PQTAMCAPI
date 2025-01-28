using PQAMCAPI.Interfaces.Services;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Services.Domain
{
    public class IBFTService : IIBFTService
    {
        public async Task<IBFTResponseDTO> GetTitle(IBFTRequestDTO Data)
        {
            return new IBFTResponseDTO()
            {
                IsSuccess = true,
                AccountTitle = "Default Title"
            };
        }
    }
}
