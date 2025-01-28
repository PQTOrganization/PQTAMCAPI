using Helper;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using AutoMapper;
using PQAMCAPI.Interfaces.Services;
using PQAMCClasses.DTOs;


namespace PQAMCAPI.Services.DB
{
    public class FundTransferRequestDBService : IFundTransferRequestDBService
    {
        const string PACKAGE_NAME = "AMC_FUND_TRANSFER_REQUEST_PKG";

        private readonly IMapper _mapper;
        private readonly OracleConnection conn;
        
        public FundTransferRequestDBService(IConfiguration configuration, IMapper mapper)
        {
            String connStr = DBSettingsHelper.GetConnectionString(configuration);
            conn = new OracleConnection(connStr);
         
            _mapper = mapper;
        }
        
        public async Task<FundTransferRequestDTO> InsertFundTransferRequestAsync(
            FundTransferRequestDTO Request)
        {
            await conn.OpenAsync();

            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".INSERT_FUND_TRANSFER_REQUEST", conn);
            objCmd.CommandType = CommandType.StoredProcedure;


            objCmd.Parameters.Add("P_USER_ID", OracleDbType.Int64).Value = Request.UserId;
            objCmd.Parameters.Add("P_FOLIO_NUMBER", OracleDbType.Varchar2).Value = Request.FolioNumber;
            objCmd.Parameters.Add("P_FROM_FUND_ID", OracleDbType.Int64).Value = Request.FromFundId;
            objCmd.Parameters.Add("P_TRANSFER_AMOUNT", OracleDbType.Decimal).Value = 
                Request.TransferAmount;
            objCmd.Parameters.Add("P_FROM_NAV_APPLIED", OracleDbType.Int16).Value = 
                Request.FromNavApplied;
            objCmd.Parameters.Add("P_FROM_NUM_OF_UNITS", OracleDbType.Decimal).Value =
                Request.FromNumOfUnits;
            objCmd.Parameters.Add("P_TO_FUND_ID", OracleDbType.Decimal).Value =
                Request.ToFundId;
            objCmd.Parameters.Add("P_TO_NAV_APPLIED", OracleDbType.Clob).Value =
                Request.ToNavApplied;
            objCmd.Parameters.Add("P_TO_NUM_OF_UNITS", OracleDbType.Clob).Value =
                Request.ToNumOfUnits;

            objCmd.Parameters.Add("P_NEW_FUND_TRANSFER_REQUEST_ID", OracleDbType.Int64, 
                ParameterDirection.Output);

            await objCmd.ExecuteNonQueryAsync();

            // Best way to convert Int64 to int
            Request.FundTransferRequestId = int.Parse(objCmd.Parameters["P_NEW_FUND_TRANSFER_REQUEST_ID"].Value.ToString());

            await conn.CloseAsync();

            return Request;
        }

        public async Task UpdateFundTransferRequestStatusAsync(int FundTransferRequestId, 
            short Status)
        {
            await conn.OpenAsync();

            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".UPDATE_FUND_TRANSFER_REQUEST_STATUS", conn);
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.Add("P_FUND_TRANSFER_REQUEST_ID", OracleDbType.Int64).Value = 
                FundTransferRequestId;
            objCmd.Parameters.Add("P_STATUS", OracleDbType.Int64).Value = Status;
            objCmd.Parameters.Add("P_RECORD_COUNT", OracleDbType.Int64, ParameterDirection.Output);

            await objCmd.ExecuteNonQueryAsync();

            int count = int.Parse(objCmd.Parameters["P_RECORD_COUNT"].Value.ToString());
            await conn.CloseAsync();
        }
    }
}
