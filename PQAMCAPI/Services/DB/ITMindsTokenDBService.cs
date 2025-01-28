using System.Data;
using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;
using Oracle.ManagedDataAccess.Client;

using Helper;
using System.Collections.Generic;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Services
{
    public class ITMindsTokenDBService : IITMindsTokenDBService
    {
        const string PACKAGE_NAME = "AMC_ITMINDS_TOKEN_PKG";

        private readonly IStoreProcedureService _spService;
        private readonly OracleConnection conn;

        public ITMindsTokenDBService(IConfiguration configuration, IStoreProcedureService spService)
        {
            String connStr = DBSettingsHelper.GetConnectionString(configuration);
            conn = new OracleConnection(connStr);

            _spService = spService;            
        }        

        public Task<List<ITMindsToken>> GetAllAsync()
        {
            var ITMindsTokens = _spService.GetAllSP<ITMindsToken>(PACKAGE_NAME + ".GET_ITMINDS_TOKENS", -1);
            return ITMindsTokens;
        }

        public async Task<ITMindsToken> InsertITMindsToken(ITMindsToken Data)
        {
            try
            {
                await conn.OpenAsync();

                OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".INSERT_ITMINDS_TOKEN", conn);
                objCmd.CommandType = CommandType.StoredProcedure;

                PopulateParamsFromData(objCmd, Data);
                
                await objCmd.ExecuteNonQueryAsync();
               
                await conn.CloseAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return Data;
        }

        public async Task<string> GetITMindsCredentials(string KeyFor)
        {
            string Credentials = string.Empty;
            try
            {
                await conn.OpenAsync();

                OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".SP_AMC_GET_ITMINDS_KEY", conn);
                objCmd.CommandType = CommandType.StoredProcedure;

                objCmd.Parameters.Add("object_key", OracleDbType.Varchar2).Value = KeyFor;
                OracleParameter returnCursor = new OracleParameter("record_set", OracleDbType.RefCursor,
                                                               ParameterDirection.Output);
                objCmd.Parameters.Add(returnCursor);

                OracleDataReader dr = objCmd.ExecuteReader();

                while (dr.Read())
                {
                    Credentials = dr["AES_PASSWORD"].ToString();
                }

                await conn.CloseAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Credentials;
        }

        private void PopulateParamsFromData(OracleCommand objCmd, ITMindsToken Data)
        {
            objCmd.Parameters.Add("P_DATETIME", OracleDbType.Date).Value = Data.DateTime;
            objCmd.Parameters.Add("P_TOKEN", OracleDbType.Varchar2).Value = Data.Token;
        }
    }
}
