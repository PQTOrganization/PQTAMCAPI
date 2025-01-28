using System.Data;
using Oracle.ManagedDataAccess.Client;

using Helper;
using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;


namespace PQAMCAPI.Services.DB
{
    public class UserApplicationDiscrepancyDBService : IUserApplicationDiscrepancyDBService
    {
        const string PACKAGE_NAME = "AMC_USER_APPLICATION_DISCREPANCY_PKG";

        private readonly IStoreProcedureService _spService;
        private OracleConnection conn;
        
        public UserApplicationDiscrepancyDBService(IStoreProcedureService spService, 
            IConfiguration configuration)
        {
            String connStr = DBSettingsHelper.GetConnectionString(configuration);
            conn = new OracleConnection(connStr);
         
            _spService = spService;
        }

        public async Task<UserApplicationDiscrepancy> FindAsync(int UserApplicationId)
        {
            return await _spService.GetSP<UserApplicationDiscrepancy>(PACKAGE_NAME + 
                ".GET_USER_APPLICATION_DISCREPANCY", UserApplicationId);
        }

        public async Task<UserApplicationDiscrepancy> GetDiscrepancyForUserApplication(
            int UserApplicationId)
        {
            return await _spService.GetSP<UserApplicationDiscrepancy>(PACKAGE_NAME + 
                ".GET_DISCREPANCY_FOR_APPLICATION", UserApplicationId);
        }
        
        public async Task<UserApplicationDiscrepancy> InsertApplicationDiscrepancy(
            UserApplicationDiscrepancy Discrepancy)
        {
            await conn.OpenAsync();

            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".INSERT_DISCREPANCY", conn);
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.Add("P_USER_APPLICATION_ID", OracleDbType.Int64).Value = 
                Discrepancy.UserApplicationId;
            objCmd.Parameters.Add("P_APPLICATION_DATA", OracleDbType.Clob).Value = 
                Discrepancy.ApplicationData;
            objCmd.Parameters.Add("P_DISCREPANT_FIELDS", OracleDbType.Clob).Value = 
                Discrepancy.DiscrepantFields;
            objCmd.Parameters.Add("P_NEW_USER_APPLICATION_DISCREPANCY_ID", 
                                                            OracleDbType.Int64, ParameterDirection.Output);

            await objCmd.ExecuteNonQueryAsync();

            // Best way to convert Int64 to int
            Discrepancy.UserApplicationDiscrepancyId = 
                int.Parse(objCmd.Parameters["P_NEW_USER_APPLICATION_DISCREPANCY_ID"].Value.ToString());

            await conn.CloseAsync();

            return Discrepancy;
        }        
    }
}
