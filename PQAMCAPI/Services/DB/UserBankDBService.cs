using PQAMCAPI.Models;
using Helper;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using AutoMapper;
using PQAMCAPI.Interfaces.Services;
using API.Classes;

namespace PQAMCAPI.Services.DB
{
    public class UserBankDBService : IUserBankDBService
    {
        const string PACKAGE_NAME = "AMC_USER_BANK_PKG";

        private readonly IMapper _mapper;
        private readonly IStoreProcedureService _spService;
        private OracleConnection conn;
        
        public UserBankDBService(IStoreProcedureService spService, IConfiguration configuration, IMapper mapper)
        {
            String connStr = DBSettingsHelper.GetConnectionString(configuration);
            conn = new OracleConnection(connStr);
         
            _mapper = mapper;
            _spService = spService;
        }

        public async Task<UserBank> FindAsync(int userBankId)
        {
            return await _spService.GetSP<UserBank>(PACKAGE_NAME + ".GET_USER_BANK", userBankId);
        }

        public async Task<List<UserBank>> GetAllAsync()
        {
            return await _spService.GetAllSP<UserBank>(PACKAGE_NAME + ".GET_USER_BANKS", -1);
        }

        public async Task<List<UserBank>> GetAllForUser(int userId)
        {
            return await _spService.GetAllSP<UserBank>(PACKAGE_NAME + ".GET_USERS_BANKS", userId);
        }
        
        public async Task<UserBank> InsertUserBank(UserBank userBank)
        {
            await conn.OpenAsync();

            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".INSERT_USER_BANK", conn);
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.Add("P_BANK_ID", OracleDbType.Int64).Value = userBank.BankId;
            objCmd.Parameters.Add("P_USER_ID", OracleDbType.Int64).Value = userBank.UserId;
            objCmd.Parameters.Add("P_IBAN_NUMBER", OracleDbType.NVarchar2).Value = userBank.IBANNumber;
            objCmd.Parameters.Add("P_IS_IBAN_VERIFIED", OracleDbType.Int16).Value = userBank.IsIBANVerified;
            objCmd.Parameters.Add("P_ONE_LINK_TTTLE", OracleDbType.NVarchar2).Value = userBank.OneLinkTitle;
            objCmd.Parameters.Add("P_IS_OB_ACCOUNT", OracleDbType.Int16).Value = userBank.IsOBAccount;
            objCmd.Parameters.Add("P_NEW_USER_BANK_ID", OracleDbType.Int64, ParameterDirection.Output);

            await objCmd.ExecuteNonQueryAsync();

            // Best way to convert Int64 to int
            userBank.UserBankId = int.Parse(objCmd.Parameters["P_NEW_USER_BANK_ID"].Value.ToString());

            await conn.CloseAsync();

            return userBank;
        }

        public async Task<UserBank> UpdateUserBank(int userBankId, UserBank userBank)
        {
            await conn.OpenAsync();

            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".UPDATE_USER_BANK", conn);
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.Add("P_USER_BANK_ID", OracleDbType.Int64).Value = userBank.UserBankId;
            objCmd.Parameters.Add("P_BANK_ID", OracleDbType.Int64).Value = userBank.BankId;
            objCmd.Parameters.Add("P_USER_ID", OracleDbType.Int64).Value = userBank.UserId;
            objCmd.Parameters.Add("P_IBAN_NUMBER", OracleDbType.NVarchar2).Value = userBank.IBANNumber;
            objCmd.Parameters.Add("P_IS_IBAN_VERIFIED", OracleDbType.Int16).Value = userBank.IsIBANVerified;
            objCmd.Parameters.Add("P_ONE_LINK_TTTLE", OracleDbType.NVarchar2).Value = userBank.OneLinkTitle;
            objCmd.Parameters.Add("P_IS_OB_ACCOUNT", OracleDbType.Int16).Value = userBank.IsOBAccount;

            await objCmd.ExecuteNonQueryAsync();

            await conn.CloseAsync();

            return userBank;
        }

        public async Task<UserBank> DeleteUserBank(int UserId, int? BankId)
        {
            await conn.OpenAsync();

            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".DELETE_USER_BANK", conn);
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.Add("P_USER_ID", OracleDbType.Int64).Value = UserId;
            objCmd.Parameters.Add("P_BANK_ID", OracleDbType.Int64).Value = BankId;
            objCmd.Parameters.Add("P_RECORD_COUNT", OracleDbType.Int64, ParameterDirection.Output);

            await objCmd.ExecuteNonQueryAsync();

            var DeletedRecords = int.Parse(objCmd.Parameters["P_RECORD_COUNT"].Value.ToString());

            await conn.CloseAsync();

            if (DeletedRecords == 0)
                throw new MyAPIException("No bank found.");

            return new UserBank();
        }
    }
}
