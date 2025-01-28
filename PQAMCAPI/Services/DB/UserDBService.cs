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
    public class UserDBService : IUserDBService
    {
        const string PACKAGE_NAME = "AMC_USER_PKG";

        private readonly IMapper _mapper;
        private readonly IStoreProcedureService _spService;
        private OracleConnection conn;
        
        public UserDBService(IStoreProcedureService spService, IConfiguration configuration, IMapper mapper)
        {
            String connStr = DBSettingsHelper.GetConnectionString(configuration);
            conn = new OracleConnection(connStr);
         
            _mapper = mapper;
            _spService = spService;
        }

        public Task<User> FindAsync(int userId)
        {
            return _spService.GetSP<User>(PACKAGE_NAME + ".GET_USER_BY_ID", userId);
        }

        public Task<List<User>> GetAllAsync()
        {
            return _spService.GetAllSP<User>(PACKAGE_NAME + ".GET_USERS", -1);
        }

        public async Task<User> InsertUser(User user)
        {
            var exists = await IfUserExist(user);

            if (exists)
                throw new MyAPIException("User Already Exists");

            await conn.OpenAsync();

            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".INSERT_USER", conn);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("Email", OracleDbType.NVarchar2).Value = user.Email;
            objCmd.Parameters.Add("MobileNumber", OracleDbType.NVarchar2).Value = user.MobileNumber;
            objCmd.Parameters.Add("OTP", OracleDbType.NVarchar2).Value = user.OTP;
            OracleParameter returnCursor = new OracleParameter("UsersList", OracleDbType.RefCursor,
                                                               ParameterDirection.Output);
            objCmd.Parameters.Add(returnCursor);

            OracleDataReader dr = objCmd.ExecuteReader();

            List<User> users = new List<User>();
            while (dr.Read())
            {
                users.Add(_mapper.Map<IDataReader, User>(dr));
            }
            await conn.CloseAsync();
            return users[0];
        }

        public async Task<User> UpdateUser(int userId, User user)
        {
            await conn.OpenAsync();

            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".UPDATE_USER", conn);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("P_USER_ID", OracleDbType.Int64).Value = user.UserId;
            objCmd.Parameters.Add("P_FIRST_NAME", OracleDbType.NVarchar2).Value = user.FirstName;
            objCmd.Parameters.Add("P_FIRST_NAME", OracleDbType.NVarchar2).Value = user.LastName;
            objCmd.Parameters.Add("P_CNIC", OracleDbType.Varchar2).Value = user.CNIC;
            objCmd.Parameters.Add("P_PROFILE_IMAGE", OracleDbType.Clob).Value = user.ProfileImage;
           
            OracleDataReader dr = objCmd.ExecuteReader();

            await conn.CloseAsync();
            return user;
        }

        public async Task<User> GetUserWith(string email, string number)
        {
            await conn.OpenAsync();
            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".get_user", conn);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("mobile_number", OracleDbType.Varchar2).Value = number;
            objCmd.Parameters.Add("email", OracleDbType.Varchar2).Value = email;
            OracleParameter returnCursor = new OracleParameter("recordset", OracleDbType.RefCursor, 
                                                               ParameterDirection.Output);
            objCmd.Parameters.Add(returnCursor);

            OracleDataReader dr = objCmd.ExecuteReader();
            Console.WriteLine(dr);
            List<User> objectList = new List<User>();
            while (dr.Read())
            {
                objectList.Add(_mapper.Map<IDataReader, User>(dr));
            }
            if (objectList.Count > 0)
            {
                await conn.CloseAsync();
                return objectList[0];
            }
            else
            {
                throw new MyAPIException("User Not Found");
            }
        }

        public async Task<AMCUserDTO> GetAMCUser(string number)
        {
            await conn.OpenAsync();
            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".get_amc_user", conn);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("mobile_number", OracleDbType.Varchar2).Value = number;
            OracleParameter returnCursor = new OracleParameter("recordset", OracleDbType.RefCursor,
                                                               ParameterDirection.Output);
            objCmd.Parameters.Add(returnCursor);

            OracleDataReader dr = objCmd.ExecuteReader();

            List<AMCUserDTO> objectList = new List<AMCUserDTO>();
            while (dr.Read())
            {
                objectList.Add(_mapper.Map<IDataReader, AMCUserDTO>(dr));
            }

            if (objectList.Count > 0)
            {
                await conn.CloseAsync();
                return objectList[0];
            }
            else
            {
                throw new MyAPIException("User Not Found");
            }
        }

        public async Task<List<UserFolioDTO>> GetFoliosOfUser(string MobileNumber)
        {
            await conn.OpenAsync();
            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".GET_FOLIOS_OF_USER", conn);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("mobile_number", OracleDbType.Varchar2).Value = MobileNumber;
            OracleParameter returnCursor = new OracleParameter("recordset", OracleDbType.RefCursor,
                                                               ParameterDirection.Output);
            objCmd.Parameters.Add(returnCursor);

            OracleDataReader dr = objCmd.ExecuteReader();

            List<UserFolioDTO> objectList = new List<UserFolioDTO>();
            while (dr.Read())
            {
                objectList.Add(_mapper.Map<IDataReader, UserFolioDTO>(dr));
            }

            if (objectList.Count > 0)
            {
                await conn.CloseAsync();
                return objectList;
            }
            else            
                throw new MyAPIException("User Folios Not Found");
            
        }
        public async Task<bool> IfUserExist(User user)
        {
            await conn.OpenAsync();
            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".IF_EXIST", conn);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("mobile_number", OracleDbType.Varchar2).Value = user.MobileNumber;
            objCmd.Parameters.Add("email", OracleDbType.Varchar2).Value = user.Email;
            objCmd.Parameters.Add("recordset", OracleDbType.Int32, ParameterDirection.Output);

            objCmd.ExecuteNonQuery();
            await conn.CloseAsync();
            return Convert.ToInt32(objCmd.Parameters["recordset"].Value.ToString()) > 0;
        }

        public async Task<bool> UpdateOTP(int UserId, string otp)
        {
            await conn.OpenAsync();
            DataTable dt = new DataTable();
            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".UPDATE_OTP", conn);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("UserId", OracleDbType.Int32).Value = UserId;
            objCmd.Parameters.Add("OTP", OracleDbType.Varchar2).Value = otp;
            objCmd.Parameters.Add("OTPGenerationDateTime", OracleDbType.Date).Value = DateTime.UtcNow;
            objCmd.ExecuteNonQuery();
            await conn.CloseAsync();
            return true;
        }

        public async Task<bool> UpdateOTPRetryAttempts(int UserId, int IncorrectOTPRetryAttempts, string UserAccountStatus)
        {
            await conn.OpenAsync();
            DataTable dt = new DataTable();
            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".UPDATE_INCORRECT_OTP_ATTEMPTS", conn);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("P_USERID", OracleDbType.Int32).Value = UserId;
            objCmd.Parameters.Add("P_INCORRECT_ATTEMPTS", OracleDbType.Int32).Value = IncorrectOTPRetryAttempts;
            objCmd.Parameters.Add("P_ACCOUNT_STATUS", OracleDbType.Varchar2).Value = UserAccountStatus;
            objCmd.ExecuteNonQuery();
            await conn.CloseAsync();
            return true;
        }
    }
}
