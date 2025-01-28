using PQAMCClasses.CloudDTOs;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IReportService
    {
        Task<string> GenerateReport(GetAccountStatementReportRequestDTO ActStmtReq);        
    }
}
