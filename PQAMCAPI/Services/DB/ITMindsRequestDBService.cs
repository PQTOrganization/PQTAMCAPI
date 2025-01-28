using System.Data;
using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;
using Oracle.ManagedDataAccess.Client;

using Helper;
using System.Collections.Generic;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Services
{
    public class ITMindsRequestDBService : IITMindsRequestDBService
    {
        const string PACKAGE_NAME = "AMC_ITMINDS_REQUEST_PKG";

        private readonly IStoreProcedureService _spService;
        private readonly OracleConnection conn;

        public ITMindsRequestDBService(IConfiguration configuration, IStoreProcedureService spService)
        {
            String connStr = DBSettingsHelper.GetConnectionString(configuration);
            conn = new OracleConnection(connStr);

            _spService = spService;            
        }        

        public Task<List<ITMindsRequest>> GetAllAsync()
        {
            var ITMindsRequests = _spService.GetAllSP<ITMindsRequest>(PACKAGE_NAME + ".get_itminds_requests", -1);
            return ITMindsRequests;
        }

        public async Task<ITMindsRequest> InsertITMindsRequest(ITMindsRequest Data)
        {
            try
            {
                
                await conn.OpenAsync();

                OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".INSERT_ITMINDS_REQUEST", conn);
                objCmd.CommandType = CommandType.StoredProcedure;

                PopulateParamsFromData(objCmd, Data);
                objCmd.Parameters.Add("P_ID", OracleDbType.Int64, ParameterDirection.Output);

                await objCmd.ExecuteNonQueryAsync();

                // Best way to convert Int64 to int
                Data.Id = int.Parse(objCmd.Parameters["P_ID"].Value.ToString());

                await conn.CloseAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return Data;
        }

        private void PopulateParamsFromData(OracleCommand objCmd, ITMindsRequest Data)
        {
            objCmd.Parameters.Add("P_USER_APPLICATION_ID", OracleDbType.Int64).Value = Data.UserApplicationID;
            objCmd.Parameters.Add("P_REQUEST_TYPE", OracleDbType.Varchar2).Value = Data.RequestType;
            objCmd.Parameters.Add("P_REQUEST", OracleDbType.Clob).Value = Data.Request;
            objCmd.Parameters.Add("P_RESPONSE", OracleDbType.Clob).Value = Data.Response;
            objCmd.Parameters.Add("P_REQUESTDATE", OracleDbType.Date).Value = Data.RequestDateTime;
            objCmd.Parameters.Add("P_PARENT_ID", OracleDbType.Int64).Value = Data.ParentID;
        }
    }
}
