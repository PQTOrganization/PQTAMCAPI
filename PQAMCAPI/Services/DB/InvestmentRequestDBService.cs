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
    public class InvestmentRequestDBService : IInvestmentRequestDBService
    {
        const string PACKAGE_NAME = "AMC_INVESTMENT_REQUEST_PKG";

        private readonly IMapper _mapper;
        private readonly OracleConnection conn;
        
        public InvestmentRequestDBService(IConfiguration configuration, IMapper mapper)
        {
            String connStr = DBSettingsHelper.GetConnectionString(configuration);
            conn = new OracleConnection(connStr);
         
            _mapper = mapper;
        }
        
        public async Task<InvestmentRequestDTO> InsertInvestmentRequestAsync(InvestmentRequestDTO Request)
        {
            await conn.OpenAsync();

            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".INSERT_INVESTMENT_REQUEST", conn);
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.Add("P_USER_ID", OracleDbType.Int64).Value = Request.UserId;
            objCmd.Parameters.Add("P_FOLIO_NUMBER", OracleDbType.Varchar2).Value = Request.FolioNumber;
            objCmd.Parameters.Add("P_FUND_ID", OracleDbType.Int64).Value = Request.FundId;
            objCmd.Parameters.Add("P_INVESTMENT_AMOUNT", OracleDbType.Decimal).Value = 
                Request.InvestmentAmount;
            objCmd.Parameters.Add("P_PAYMENT_MODE", OracleDbType.Int16).Value = Request.PaymentMode;
            objCmd.Parameters.Add("P_FRONT_END_LOAD", OracleDbType.Decimal).Value =
                Request.FrontEndLoad;
            objCmd.Parameters.Add("P_NAV_APPLIED", OracleDbType.Decimal).Value =
                Request.NavApplied;
            objCmd.Parameters.Add("P_PROOF_OF_PAYMENT", OracleDbType.Clob).Value =
                Request.ProofOfPayment;

            objCmd.Parameters.Add("P_BANK_ID", OracleDbType.Int64).Value = Request.BankId;
            objCmd.Parameters.Add("P_REFERENCE_PREFIX", OracleDbType.Varchar2).Value = 
                Request.OnlinePaymentReference;
            objCmd.Parameters.Add("P_INITIAL_INVESTMENT", OracleDbType.Int16).Value =
                Request.IsInitialInvestment;
            objCmd.Parameters.Add("P_BRANCH_ID", OracleDbType.Varchar2).Value =
                Request.BranchId;
            objCmd.Parameters.Add("P_BANK_NAME", OracleDbType.Varchar2).Value =
               Request.BankName;
            objCmd.Parameters.Add("P_ACCOUNT_NO", OracleDbType.Varchar2).Value =
                Request.AccountNumber;
            objCmd.Parameters.Add("P_ITMINDS_BANK_ID", OracleDbType.Varchar2).Value =
               Request.ITMindsBankID;

            objCmd.Parameters.Add("P_NEW_INVESTMENT_REQUEST_ID", OracleDbType.Int64, 
                ParameterDirection.Output);
            objCmd.Parameters.Add("P_NEW_REFERENCE", OracleDbType.Varchar2, 200).Direction = 
                ParameterDirection.Output;

            await objCmd.ExecuteNonQueryAsync();

            // Best way to convert Int64 to int
            Request.InvestmentRequestId = int.Parse(objCmd.Parameters["P_NEW_INVESTMENT_REQUEST_ID"].Value.ToString());
            Request.OnlinePaymentReference = objCmd.Parameters["P_NEW_REFERENCE"].Value.ToString();
            
            await conn.CloseAsync();

            return Request;
        }

        public async Task UpdateInvestmentRequestStatusAsync(int InvestmentRequestId, short Status)
        {
            await conn.OpenAsync();

            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".UPDATE_INVESTMENT_REQUEST_STATUS", conn);
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.Add("P_INVESTMENT_REQUEST_ID", OracleDbType.Int64).Value = InvestmentRequestId;
            objCmd.Parameters.Add("P_STATUS", OracleDbType.Int64).Value = Status;
            objCmd.Parameters.Add("P_RECORD_COUNT", OracleDbType.Int64, ParameterDirection.Output);

            await objCmd.ExecuteNonQueryAsync();

            int count = int.Parse(objCmd.Parameters["P_RECORD_COUNT"].Value.ToString());
            await conn.CloseAsync();
        }

        public async Task<bool> UpdateInvestmentRequestBlinqResponse(string ReferenceNumber, string Response,
            short Status)
        {
            await conn.OpenAsync();

            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".UPDATE_INVESTMENT_REQUEST_BLINQ_RESPONSE", conn);
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.Add("P_PAYMENT_REFERENCE", OracleDbType.Varchar2).Value = ReferenceNumber;
            objCmd.Parameters.Add("P_REQUEST_STATUS", OracleDbType.Int64).Value = Status;
            objCmd.Parameters.Add("P_RESPONSE", OracleDbType.Clob).Value = Response;
            objCmd.Parameters.Add("P_RECORD_COUNT", OracleDbType.Int64, ParameterDirection.Output);

            await objCmd.ExecuteNonQueryAsync();

            int count = int.Parse(objCmd.Parameters["P_RECORD_COUNT"].Value.ToString());
            await conn.CloseAsync();

            return count > 0;
        }

    }
}
