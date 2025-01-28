using PQAMCAPI.Models;
using Helper;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using AutoMapper;
using PQAMCAPI.Interfaces.Services;
using API.Classes;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Services.DB
{
    public class SMSLogDBService : ISMSLogDBService
    {
        const string PACKAGE_NAME = "AMC_SMS_LOG_PKG";

        private readonly IMapper _mapper;
        private readonly IStoreProcedureService _spService;
        private OracleConnection conn;
        
        public SMSLogDBService(IStoreProcedureService spService, IConfiguration configuration, IMapper mapper)
        {
            String connStr = DBSettingsHelper.GetConnectionString(configuration);
            conn = new OracleConnection(connStr);
         
            _mapper = mapper;
            _spService = spService;
        }

        public async Task<SMSLog> InsertSMSLog(SMSLog smsLog)
        {
            await conn.OpenAsync();

            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".INSERT_SMSLOG", conn);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("P_MOBILE_NUMBER", OracleDbType.Varchar2).Value = smsLog.MobileNumber;
            objCmd.Parameters.Add("P_OPERATION", OracleDbType.Int64).Value = smsLog.Operation;
            objCmd.Parameters.Add("P_USER_ID", OracleDbType.Int64).Value = smsLog.UserID;
            objCmd.Parameters.Add("P_SMS_LOG_ID", OracleDbType.Int64, ParameterDirection.Output);

            await objCmd.ExecuteNonQueryAsync();

            smsLog.SMSLogID = int.Parse(objCmd.Parameters["P_SMS_LOG_ID"].Value
                                       .ToString());

            await conn.CloseAsync();
            return smsLog;
        }

        public async Task<int> GetSMSCount(string MobileNumber, DateTime StartDateTime, DateTime EndDateTime)
        {
            await conn.OpenAsync();

            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".SENT_SMS_COUNT", conn);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("P_MOBILE_NUMBER", OracleDbType.Varchar2).Value = MobileNumber;
            objCmd.Parameters.Add("P_START_DATETIME", OracleDbType.TimeStamp).Value = StartDateTime;
            objCmd.Parameters.Add("P_END_DATETIME", OracleDbType.TimeStamp).Value = EndDateTime;
            objCmd.Parameters.Add("P_SMS_COUNT", OracleDbType.Int64, ParameterDirection.Output);

            await objCmd.ExecuteNonQueryAsync();

            int SMSCount = int.Parse(objCmd.Parameters["P_SMS_COUNT"].Value
                                       .ToString());

            await conn.CloseAsync();
            return SMSCount;
        }

    }
}
