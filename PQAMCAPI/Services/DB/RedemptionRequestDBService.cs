using Helper;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using AutoMapper;
using PQAMCAPI.Interfaces.Services;
using API.Classes;
using PQAMCClasses.DTOs;
using PQAMCAPI.Models;

namespace PQAMCAPI.Services.DB
{
    public class RedemptionRequestDBService : IRedemptionRequestDBService
    {
        const string PACKAGE_NAME = "AMC_REDEMPTION_REQUEST_PKG";

        private readonly IMapper _mapper;
        private readonly OracleConnection conn;
        
        public RedemptionRequestDBService(IConfiguration configuration, IMapper mapper)
        {
            String connStr = DBSettingsHelper.GetConnectionString(configuration);
            conn = new OracleConnection(connStr);
         
            _mapper = mapper;
        }
        
        public async Task<RedemptionRequestDTO> InsertRedemptionRequestAsync(
            RedemptionRequestDTO Request)
        {
            await conn.OpenAsync();

            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".INSERT_REDEMPTION_REQUEST", conn);
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.Add("P_USER_ID", OracleDbType.Int64).Value = Request.UserId;
            objCmd.Parameters.Add("P_FOLIO_NUMBER", OracleDbType.Varchar2).Value = Request.FolioNumber;
            objCmd.Parameters.Add("P_FUND_ID", OracleDbType.Int64).Value = Request.FundId;
            objCmd.Parameters.Add("P_REDEMPTION_AMOUNT", OracleDbType.Decimal).Value = 
                Request.RedemptionAmount;
            objCmd.Parameters.Add("P_BACK_END_LOAD", OracleDbType.Decimal).Value =
                Request.BackEndLoad;
            objCmd.Parameters.Add("P_NAV_APPLIED", OracleDbType.Decimal).Value =
                Request.NavApplied;
            objCmd.Parameters.Add("P_BANK_ID", OracleDbType.Varchar2).Value =
                Request.BankID;
            objCmd.Parameters.Add("P_BANK_ACCOUNT_NO", OracleDbType.Varchar2).Value =
                Request.BankAccountNo;

            objCmd.Parameters.Add("P_NEW_REDEMPTION_REQUEST_ID", OracleDbType.Int64, ParameterDirection.Output);

            await objCmd.ExecuteNonQueryAsync();

            // Best way to convert Int64 to int
            Request.RedemptionRequestId = int.Parse(objCmd.Parameters["P_NEW_REDEMPTION_REQUEST_ID"].Value.ToString());

            await conn.CloseAsync();

            return Request;
        }

        public async Task UpdateRedemptionRequestStatusAsync(int RedemptionRequestId, short Status)
        {
            await conn.OpenAsync();

            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".UPDATE_REDEMPTION_REQUEST_STATUS", conn);
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.Add("P_REDEMPTION_REQUEST_ID", OracleDbType.Int64).Value = 
                RedemptionRequestId;
            objCmd.Parameters.Add("P_STATUS", OracleDbType.Int64).Value = Status;
            objCmd.Parameters.Add("P_RECORD_COUNT", OracleDbType.Int64, ParameterDirection.Output);

            await objCmd.ExecuteNonQueryAsync();

            int count = int.Parse(objCmd.Parameters["P_RECORD_COUNT"].Value.ToString());
            await conn.CloseAsync();
        }
    }
}
