using Helper;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using AutoMapper;
using PQAMCAPI.Interfaces.Services;
using PQAMCClasses.DTOs;
using PQAMCClasses.CloudDTOs;

namespace PQAMCAPI.Services.DB
{
    public class FundDBService : IFundDBService
    {
        const string PACKAGE_NAME = "AMC_INVESTMENT_PKG";

        private readonly IMapper _mapper;
        private readonly OracleConnection conn;
        
        public FundDBService(IConfiguration configuration, IMapper mapper)
        {
            String connStr = DBSettingsHelper.GetConnectionString(configuration);
            conn = new OracleConnection(connStr);
         
            _mapper = mapper;
        }
        
        public async Task<decimal> GetFundNAV(int FundID)
        {
            decimal FundNAV = 0;
            await conn.OpenAsync();
            string ApplicableNavDate = DateTime.Now.ToString("dd/MMM/yyyy");
            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".GET_FUND_NAV_BY_FUNDID", conn);
            objCmd.CommandType = CommandType.StoredProcedure;


            objCmd.Parameters.Add("P_FUND_ID", OracleDbType.Int64).Value = FundID;
            objCmd.Parameters.Add("P_NAV_DATE", OracleDbType.Varchar2).Value = ApplicableNavDate;
            
            objCmd.Parameters.Add("NAV_VALUE", OracleDbType.Int64, ParameterDirection.Output);

            await objCmd.ExecuteNonQueryAsync();

            if (objCmd.Parameters["NAV_VALUE"].Value.ToString() != "null")
            {
                FundNAV = decimal.Parse(objCmd.Parameters["NAV_VALUE"].Value.ToString());
            }

            await conn.CloseAsync();

            return FundNAV;
        }

    }
}
