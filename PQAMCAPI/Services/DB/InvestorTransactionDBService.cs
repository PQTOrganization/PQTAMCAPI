using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;
using Oracle.ManagedDataAccess.Client;
using Helper;
using System.Data;

namespace PQAMCAPI.Services
{
    public class InvestorTransactionDBService : IInvestorTransactionDBService
    {
        const string PACKAGE_NAME = "AMC_INVESTOR_TRANSACTION_PKG";

        private readonly IStoreProcedureService _spService;
        private readonly OracleConnection conn;

        public InvestorTransactionDBService(IConfiguration configuration, IStoreProcedureService spService)
        {
            String connStr = DBSettingsHelper.GetConnectionString(configuration);
            conn = new OracleConnection(connStr);

            _spService = spService;            
        }
        
        public async Task<InvestorTransaction> InsertInvestorTransaction(InvestorTransaction Data)
        {
            await conn.OpenAsync();

            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".INSERT_INVESTOR_TRANSACTION", conn);
            objCmd.CommandType = CommandType.StoredProcedure;

            PopulateParamsFromData(objCmd, Data);
            objCmd.Parameters.Add("P_INVESTOR_TRANSACTION_ID", OracleDbType.Int64, ParameterDirection.Output);

            await objCmd.ExecuteNonQueryAsync();

            // Best way to convert Int64 to int
            Data.InvestorTransactionID = int.Parse(objCmd.Parameters["P_INVESTOR_TRANSACTION_ID"].Value
                                        .ToString());

            await conn.CloseAsync();

            return Data;
        }

        private void PopulateParamsFromData(OracleCommand objCmd, InvestorTransaction Data)
        {
            objCmd.Parameters.Add("P_FOLIO_NUMBER", OracleDbType.Varchar2).Value = Data.FolioNumber;
            objCmd.Parameters.Add("P_NO_OF_UNITS_DOUBLE", OracleDbType.Double).Value = Data.NoOfUnitsDouble;
            objCmd.Parameters.Add("P_UNIT_PRICE_DOUBLE", OracleDbType.Double).Value = Data.UnitPriceDouble;
            objCmd.Parameters.Add("P_MAX_NAV_DATE_FUND", OracleDbType.Date).Value = Data.MaxNavDateFund;
            objCmd.Parameters.Add("P_OTHER_CHARGES", OracleDbType.Double).Value = Data.OtherCharges;
            objCmd.Parameters.Add("P_PLAN_NAME", OracleDbType.NVarchar2).Value =
                Data.PlanName;
            objCmd.Parameters.Add("P_FUND_SHORT_NAME", OracleDbType.NVarchar2).Value = Data.FundShortName;
            objCmd.Parameters.Add("P_TYPE", OracleDbType.NVarchar2).Value = Data.Type;
            objCmd.Parameters.Add("P_RECORD_ID", OracleDbType.NVarchar2).Value = Data.RecordId;
            objCmd.Parameters.Add("P_CGT", OracleDbType.NVarchar2).Value = Data.CGT;
            objCmd.Parameters.Add("P_NO_OF_UNITS", OracleDbType.NVarchar2).Value = Data.NoOfUnits;
            objCmd.Parameters.Add("P_PLAN_OTHER_CHARGES", OracleDbType.NVarchar2).Value = Data.PlanOtherCharges;
            objCmd.Parameters.Add("P_GROSS_AMOUNT_DOUBLE", OracleDbType.Double).Value = Data.GrossAmountDouble;
            objCmd.Parameters.Add("P_LOAD_AMOUNT", OracleDbType.NVarchar2).Value = Data.LoadAmount;
            objCmd.Parameters.Add("P_PLAN_ID", OracleDbType.NVarchar2).Value = Data.PlanId;
            objCmd.Parameters.Add("P_BAL_OTHER_CHARGE", OracleDbType.NVarchar2).Value = Data.BalOthCharge;
            objCmd.Parameters.Add("P_NAV_PER_UNIT", OracleDbType.NVarchar2).Value = Data.NavPerUnit;
            objCmd.Parameters.Add("P_OPENING_BALANCE", OracleDbType.NVarchar2).Value = Data.OpeningBalance;
            objCmd.Parameters.Add("P_UNIT_PRICE", OracleDbType.NVarchar2).Value = Data.UnitPrice;
            objCmd.Parameters.Add("P_ZAKAT_AMOUNT", OracleDbType.NVarchar2).Value = Data.ZakatAmount;
            objCmd.Parameters.Add("P_NAV_DATE", OracleDbType.Date).Value = Data.NavDate;
            objCmd.Parameters.Add("P_NET_AMOUNT", OracleDbType.NVarchar2).Value = Data.NetAmount;
            objCmd.Parameters.Add("P_BALANCE_UNIT", OracleDbType.Double).Value = Data.BalanceUnit;
            objCmd.Parameters.Add("P_REPORT_OPTION", OracleDbType.NVarchar2).Value = Data.ReportOption;
            objCmd.Parameters.Add("P_MAX_NAV_DATE", OracleDbType.Date).Value = Data.MaxNavDate;
            objCmd.Parameters.Add("P_BACK_END_LOAD", OracleDbType.NVarchar2).Value = Data.BackEndLoad;
            objCmd.Parameters.Add("P_TO_DATE", OracleDbType.Date).Value = Data.ToDate;
            objCmd.Parameters.Add("P_PLAN_GROSS_AMOUNT_DOUBLE", OracleDbType.Double).Value = Data.PlanGrossAmountDouble;
            objCmd.Parameters.Add("P_GROSS_AMOUNT", OracleDbType.NVarchar2).Value = Data.GrossAmount;
            objCmd.Parameters.Add("P_NO_OF_UNITS_POSITIVE", OracleDbType.Int16).Value = Data.NoOfUnitPositive;
            objCmd.Parameters.Add("P_UNIT_HOLDER_ID", OracleDbType.NVarchar2).Value = Data.UnitHolderId;
            objCmd.Parameters.Add("P_PLAN_NET_AMOUNT", OracleDbType.NVarchar2).Value = Data.PlanNetAmount;
            objCmd.Parameters.Add("P_SUM_FUND_WISE_INVESTMENT", OracleDbType.NVarchar2).Value = Data.SumFundWiseInvestment;
            objCmd.Parameters.Add("P_PLAN_SHORT_NAME", OracleDbType.NVarchar2).Value = Data.PlanShortName;
            objCmd.Parameters.Add("P_NET_AMOUNT_DOUBLE", OracleDbType.Double).Value = Data.NetAmountDouble;
            objCmd.Parameters.Add("P_PLAN_NET_AMOUNT_DOUBLE", OracleDbType.Double).Value = Data.PlanNetAmountDouble;
            objCmd.Parameters.Add("P_VPS_FUND_NAME", OracleDbType.NVarchar2).Value = Data.VpsFundName;
            objCmd.Parameters.Add("P_INSTRUCTION_ID", OracleDbType.Int64).Value = Data.InstructionId;
            objCmd.Parameters.Add("P_FUND_NAME", OracleDbType.NVarchar2).Value = Data.FundName;
            objCmd.Parameters.Add("P_TRANSACTION", OracleDbType.NVarchar2).Value = Data.Transaction;
            objCmd.Parameters.Add("P_FUND_WISE_INVESTMENT", OracleDbType.NVarchar2).Value = Data.FundWiseInvestment;
            objCmd.Parameters.Add("P_PLAN_GROSS_AMOUNT", OracleDbType.NVarchar2).Value = Data.PlanGrossAmount;
            objCmd.Parameters.Add("P_DIVIDEND_PAYOUT", OracleDbType.NVarchar2).Value = Data.DividendPayout;
           }
    }
}
