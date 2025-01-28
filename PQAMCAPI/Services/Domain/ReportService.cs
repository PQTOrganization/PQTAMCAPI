using API.Classes;
using Org.BouncyCastle.Asn1.Ocsp;
using PQAMCAPI.Interfaces.Services;
using PQAMCAPI.Models;
using PQAMCAPI.Services.DB;
using PQAMCClasses.CloudDTOs;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Services.Domain
{
    public class ReportService : IReportService
    {        
        private readonly ICloudService _cloudService;

        public ReportService(ICloudService cloudService)
        {
            _cloudService = cloudService;
        }

        public async Task<string> GenerateReport(GetAccountStatementReportRequestDTO ActStmtReq)
        {
            AccountStatementReportDTO Response = await _cloudService.GetAccountStatementReport(ActStmtReq);
            return Response.Message;
        }

    }
}
