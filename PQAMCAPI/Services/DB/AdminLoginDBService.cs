using Oracle.ManagedDataAccess.Client;
using AutoMapper;

using Helper;
using PQAMCAPI.Interfaces.Services;
using System.Data;

namespace PQAMCAPI.Services.DB
{
    public class AdminLoginDBService : IAdminLoginDBService
    {
        const string PACKAGE_NAME = "AMC_ADMIN_LOGIN_PKG"; 

        private readonly IMapper _mapper;
        private readonly OracleConnection conn;

        public AdminLoginDBService(IConfiguration configuration, IMapper mapper)
        {
            String connStr = DBSettingsHelper.GetConnectionString(configuration);
            conn = new OracleConnection(connStr);

            _mapper = mapper;
        }

        public async Task<bool> Login(string Username, string Password)
        {
            await conn.OpenAsync();

            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".LOGIN", conn);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("P_EMAIL", OracleDbType.NVarchar2).Value = Username;
            objCmd.Parameters.Add("P_PASSWORD", OracleDbType.NVarchar2).Value = Password;
            objCmd.Parameters.Add("P_RESULT", OracleDbType.Int32, ParameterDirection.Output);

            objCmd.ExecuteNonQuery();
            await conn.CloseAsync();
            return Convert.ToInt32(objCmd.Parameters["P_RESULT"].Value.ToString()) != 0;
        }
    }
}
