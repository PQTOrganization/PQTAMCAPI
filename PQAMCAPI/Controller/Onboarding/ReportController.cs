using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using PQAMCAPI.Interfaces.Services;
using PQAMCAPI.Models;
using PQAMCClasses.CloudDTOs;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Controller.Onboarding
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IUserBankDBService _userBankDBService;
        private readonly IUserApplicationDocumentDBService _docDBService;
        private readonly IMapper _mapper;

        public ReportController(IReportService reportService, IMapper mapper)
        {
            _reportService = reportService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<string> Post(GetAccountStatementReportRequestDTO Request)
        {
            string Response = await _reportService.GenerateReport(Request);

            return Response;
        }
    }
}
