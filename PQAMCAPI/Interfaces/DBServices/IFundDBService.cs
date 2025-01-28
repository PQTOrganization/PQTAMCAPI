using PQAMCAPI.Models;
using PQAMCClasses.CloudDTOs;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IFundDBService
    {
        Task<decimal> GetFundNAV(int FundID);
    }
}
