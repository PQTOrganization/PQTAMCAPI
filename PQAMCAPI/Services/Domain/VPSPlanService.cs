using System.Data;
using AutoMapper;
using PQAMCAPI.Interfaces.Services;
using PQAMCAPI.Models;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Services.Domain
{
    public class VPSPlanService : IVPSPlanService
    {
        const string PACKAGE_NAME = "AMC_VPS_PLAN_PKG";

        private readonly IStoreProcedureService _spService;
        private readonly IMapper _mapper;

        public VPSPlanService(IStoreProcedureService spService, PQAMCAPIContext dbContext, IMapper mapper)
        {
            _spService = spService;
            _mapper = mapper;
        }

        public async Task<List<VPSPlanDTO>> GetAllAsync()
        {
            try
            {
                var plans = await _spService.GetAllSP<VPSPlan>(PACKAGE_NAME + ".get_plans" , -1);
                return _mapper.Map<List<VPSPlanDTO>>(plans);
            }
            catch(Exception ex) 
            {
                throw ex;
            }
            
        }

    }
}
