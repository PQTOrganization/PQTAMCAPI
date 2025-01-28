using Oracle.ManagedDataAccess.Client;
using AutoMapper;

using Helper;
using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.Services.DB
{
    public class DocumentDBService : IDocumentDBService
    {
        const string PACKAGE_NAME = "AMC_DOCUMENT_PKG"; 

        private readonly IMapper _mapper;
        private readonly OracleConnection conn;
        private readonly IStoreProcedureService _spService;

        public DocumentDBService(IStoreProcedureService spService, IConfiguration configuration, IMapper mapper)
        {
            String connStr = DBSettingsHelper.GetConnectionString(configuration);
            conn = new OracleConnection(connStr);

            _mapper = mapper;
            _spService = spService;
        }

        public async Task<List<Document>> GetAllAsync()
        {
            return await _spService.GetAllSP<Document>(PACKAGE_NAME + ".GET_DOCUMENTS", -1);
        }
    }
}
