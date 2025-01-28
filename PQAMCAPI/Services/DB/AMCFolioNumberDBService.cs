using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;
using Oracle.ManagedDataAccess.Client;
using Helper;

namespace PQAMCAPI.Services
{
    public class AMCFolioNumberDBService : IAMCFolioNumberDBService
    {
        const string PACKAGE_NAME = "AMC_FOLIO_NUMBER_PKG";

        private readonly IStoreProcedureService _spService;
        private readonly OracleConnection conn;

        public AMCFolioNumberDBService(IConfiguration configuration, IStoreProcedureService spService)
        {
            String connStr = DBSettingsHelper.GetConnectionString(configuration);
            conn = new OracleConnection(connStr);

            _spService = spService;            
        }
        
        public Task<List<AMCFolioNumber>> GetAllAsync()
        {
            var AMCFolioNumbers = _spService.GetAllSP<AMCFolioNumber>(PACKAGE_NAME + ".GET_FOLIO_NUMBERS", -1);
            return AMCFolioNumbers;
        }

    }
}
