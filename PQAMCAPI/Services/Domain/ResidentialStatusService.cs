using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.Services.Domain
{
    public class ResidentialStatusService : IResidentialStatusService
    {
        const string PACKAGE_NAME = "AMC_RESIDENTIAL_STATUS_PKG";

        private readonly IStoreProcedureService _spService;

        public ResidentialStatusService(IStoreProcedureService spService)
        {
            _spService = spService;
        }

        public Task<int> DeleteResidentialStatus(int residentialStatusId)
        {
            var rowCount = _spService.DeleteSP<ResidentialStatus>(PACKAGE_NAME + ".del_residential_status", residentialStatusId);
            return rowCount;
        }

        public Task<ResidentialStatus> FindAsync(int residentialStatusId)
        {
            var residentialStatus = _spService.GetSP<ResidentialStatus>(PACKAGE_NAME + ".get_residential_status", residentialStatusId);
            return residentialStatus;
        }

        public Task<List<ResidentialStatus>> GetAllAsync()
        {
            var residentialStatuss = _spService.GetAllSP<ResidentialStatus>(PACKAGE_NAME + ".get_residential_statuses", -1);
            return residentialStatuss;
        }

        public Task<ResidentialStatus> InsertResidentialStatus(ResidentialStatus residentialStatus)
        {
            throw new NotImplementedException();
        }

        public Task<ResidentialStatus> UpdateResidentialStatus(int residentialStatusId, ResidentialStatus residentialStatus)
        {
            throw new NotImplementedException();
        }
    }
}
