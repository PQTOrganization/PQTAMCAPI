using System.Data;
using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;
using Oracle.ManagedDataAccess.Client;

using Helper;
using System.Collections.Generic;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Services
{
    public class UserApplicationNomineeDBService : IUserApplicationNomineeDBService
    {
        const string PACKAGE_NAME = "AMC_USER_APPLICATION_NOMINEE_PKG";

        private readonly IStoreProcedureService _spService;
        private readonly OracleConnection conn;

        public UserApplicationNomineeDBService(IConfiguration configuration, IStoreProcedureService spService)
        {
            String connStr = DBSettingsHelper.GetConnectionString(configuration);
            conn = new OracleConnection(connStr);

            _spService = spService;            
        }
        
        public Task<UserApplicationNominee> FindAsync(int UserApplicationNomineeId)
        {
            var UserApplicationNominee = _spService.GetSP<UserApplicationNominee>(PACKAGE_NAME + ".GET_USER_APPLICATION_NOMINEE",
                                                                    UserApplicationNomineeId);
            return UserApplicationNominee;
        }

        public Task<List<UserApplicationNominee>> GetNomineesForUserApplication(int UserApplicationNomineeId)
        {
            var UserApplicationNominees = _spService.GetAllSP<UserApplicationNominee>(PACKAGE_NAME + ".GET_NOMINEES_FOR_USER_APPLICATION", UserApplicationNomineeId);
            return UserApplicationNominees;
        }

        public async Task<UserApplicationNominee> InsertUserApplicationNominee(UserApplicationNominee Data)
        {
            await conn.OpenAsync();

            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".INSERT_USER_APPLICATION_NOMINEE", conn);
            objCmd.CommandType = CommandType.StoredProcedure;

            PopulateParamsFromData(objCmd, Data);
            objCmd.Parameters.Add("P_NEW_USER_APPLICATION_NOMINEE_ID", OracleDbType.Int64, ParameterDirection.Output);

            await objCmd.ExecuteNonQueryAsync();

            // Best way to convert Int64 to int
            Data.UserApplicationNomineeId = int.Parse(objCmd.Parameters["P_NEW_USER_APPLICATION_NOMINEE_ID"].Value
                                        .ToString());

            await conn.CloseAsync();

            return Data;
        }

        public async Task<UserApplicationNominee> UpdateUserApplicationNominee(int UserApplicationNomineeId, UserApplicationNominee Data)
        {
            try
            {
                await conn.OpenAsync();

                OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".UPDATE_USER_APPLICATION_NOMINEE", conn);
                objCmd.CommandType = CommandType.StoredProcedure;

                objCmd.Parameters.Add("P_USER_APPLICATION_NOMINEE_ID", OracleDbType.Int64).Value = Data.UserApplicationNomineeId;
                PopulateParamsFromData(objCmd, Data);

                objCmd.Parameters.Add("P_RECORD_COUNT", OracleDbType.Int64, ParameterDirection.Output);

                await objCmd.ExecuteNonQueryAsync();

                int count = int.Parse(objCmd.Parameters["P_RECORD_COUNT"].Value.ToString());

                await conn.CloseAsync();

                return Data;
            }
            catch(Exception ex)
            {

            }
            return null;
        }

        public Task<int> DeleteUserApplicationNominee(int UserApplicationNomineeId)
        {
            var rowCount = _spService.DeleteSP<UserApplication>(PACKAGE_NAME + ".DELETE_USER_APPLICATION_NOMINEE", UserApplicationNomineeId);
                                                                
            return rowCount;
        }

        public Task<int> DeleteNomineeByUserApplicationID(int UserApplicationId)
        {
            var rowCount = _spService.DeleteSP<UserApplication>(PACKAGE_NAME + ".DELETE_NOMINEES_BY_APPLICATION_ID", UserApplicationId);

            return rowCount;
        }

        private void PopulateParamsFromData(OracleCommand objCmd, UserApplicationNominee Data)
        {
            objCmd.Parameters.Add("P_USER_APPLICATION_ID", OracleDbType.Int64).Value = Data.UserApplicationId;
            objCmd.Parameters.Add("P_SERIAL_NUMBER", OracleDbType.Int64).Value = Data.SerialNumber;
            objCmd.Parameters.Add("P_NAME", OracleDbType.Varchar2).Value = Data.Name;
            objCmd.Parameters.Add("P_RELATIONSHIP", OracleDbType.Varchar2).Value = Data.Relationship;
            objCmd.Parameters.Add("P_SHARE", OracleDbType.Int64).Value = Data.Share;
            objCmd.Parameters.Add("P_RESIDENTIAL_ADDRESS", OracleDbType.Varchar2).Value = Data.ResidentialAddress;
            objCmd.Parameters.Add("P_TELEPHONE_NUMBER", OracleDbType.Varchar2).Value = Data.TelephoneNumber;
            objCmd.Parameters.Add("P_BANK_ACCOUNT_DETAIL", OracleDbType.Varchar2).Value = Data.BankAccountDetail;
            objCmd.Parameters.Add("P_CNIC", OracleDbType.Varchar2).Value = Data.CNIC;
        }
    }
}
