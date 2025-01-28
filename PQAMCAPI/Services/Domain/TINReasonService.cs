using System.Data;
using AutoMapper;
using PQAMCAPI.Interfaces.Services;
using PQAMCAPI.Models;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Services.Domain
{
    public class TINReasonService : ITINReasonService
    {
        const string PACKAGE_NAME = "AMC_TIN_REASON_PKG";

        private readonly IStoreProcedureService _spService;
        private readonly IMapper _mapper;

        public TINReasonService(IStoreProcedureService spService, IMapper mapper)
        {
            _spService = spService;
            _mapper = mapper;
        }

        public async Task<List<TINReasonDTO>> GetAllAsync()
        {
            var Reasons = await _spService.GetAllSP<TINReason>(PACKAGE_NAME + ".GET_TIN_REASONS", -1);
            return _mapper.Map<List<TINReasonDTO>>(Reasons);
        }
    }
}
