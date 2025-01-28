using PQAMCAPI.Models;
using Helper;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using AutoMapper;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.Services.DB
{
    public class TokenDBService : ITokenDBService
    {
        const string PACKAGE_NAME = "AMC_USER_REFRESH_TOKEN_PKG"; 

        private readonly IMapper _mapper;
        private OracleConnection conn;
        
        public TokenDBService(IConfiguration configuration, IMapper mapper)
        {
            String connStr = DBSettingsHelper.GetConnectionString(configuration);
            conn = new OracleConnection(connStr);

            _mapper = mapper;
        }

        public async Task<UserRefreshToken> InsertToken(UserRefreshToken userToken)
        {
            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".INSERT_REFRESH_TOKEN", conn);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("UserId", OracleDbType.Int32).Value = userToken.UserId;
            objCmd.Parameters.Add("RefreshToken", OracleDbType.NVarchar2).Value = userToken.RefreshToken;
            objCmd.Parameters.Add("TokenDate", OracleDbType.NVarchar2).Value = userToken.TokenDate.ToString("yyyy-MMM-dd HH:mm:ss");
            OracleParameter returnCursor = new OracleParameter("UserToken", OracleDbType.RefCursor, ParameterDirection.Output);
            objCmd.Parameters.Add(returnCursor);

            OracleDataReader dr = objCmd.ExecuteReader();

            List<UserRefreshToken> userTokens = new List<UserRefreshToken>();
            while (dr.Read())
            {
                userTokens.Add(_mapper.Map<IDataReader, UserRefreshToken>(dr));
            }
            await conn.CloseAsync();
            return userTokens[0];
        }

        public async Task<UserRefreshToken> UpdateToken(UserRefreshToken userToken)
        {
            await conn.OpenAsync();
            DataTable dt = new DataTable();
            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".UPDATE_REFRESH_TOKEN", conn);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("UserId", OracleDbType.Int32).Value = userToken.UserId;
            objCmd.Parameters.Add("RefreshToken", OracleDbType.NVarchar2).Value = userToken.RefreshToken;
            objCmd.Parameters.Add("TokenDate", OracleDbType.NVarchar2).Value = userToken.TokenDate.ToString("yyyy-MMM-dd HH:mm:ss");
            OracleParameter returnCursor = new OracleParameter("UserToken", OracleDbType.RefCursor, ParameterDirection.Output);
            objCmd.Parameters.Add(returnCursor);

            OracleDataReader dr = objCmd.ExecuteReader();

            List<UserRefreshToken> userTokens = new List<UserRefreshToken>();
            while (dr.Read())
            {
                userTokens.Add(_mapper.Map<IDataReader, UserRefreshToken>(dr));
            }
            await conn.CloseAsync();
            return userTokens[0];
        }

        public async Task<bool> InsertExpireToken(UserToken userToken)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                DataTable dt = new DataTable();
                OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".INSERT_EXPIRE_TOKEN", conn);
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("UserId", OracleDbType.Int32).Value = userToken.UserId;
                objCmd.Parameters.Add("Token", OracleDbType.NVarchar2).Value = userToken.Token;
                objCmd.Parameters.Add("TokenDate", OracleDbType.NVarchar2).Value = userToken.TokenDate.ToString("yyyy-MMM-dd HH:mm:ss");
                objCmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> IsExpireToken(string userToken)
        {
            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            DataTable dt = new DataTable();
            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".IS_EXPIRE_TOKEN", conn);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("Token", OracleDbType.NVarchar2).Value = userToken;
            objCmd.Parameters.Add("recordset", OracleDbType.Int32, ParameterDirection.Output);

            objCmd.ExecuteNonQuery();
            await conn.CloseAsync();
            return Convert.ToInt32(objCmd.Parameters["recordset"].Value.ToString()) > 0;
        }
    }
}
