using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("AMC_INVESTOR_TRANSACTION")]
    public partial class InvestorTransaction
    {
        public InvestorTransaction()
        {

        }

        [Column("INVESTOR_TRANSACTION_ID")]
        public int InvestorTransactionID { get; set; }

        [Column("FOLIO_NUMBER")]
        public string FolioNumber { get; set; }

        [Column("NO_OF_UNITS_DOUBLE")]
        public double NoOfUnitsDouble { get; set; }

        [Column("UNIT_PRICE_DOUBLE")]
        public double UnitPriceDouble { get; set; }

        [Column("MAX_NAV_DATE_FUND")]
        public DateTime? MaxNavDateFund { get; set; }

        [Column("OTER_CHARGES")]
        public int? OtherCharges { get; set; }

        [Column("PLAN_NAME")]
        public string PlanName { get; set; } = string.Empty;

        [Column("FUND_SHORT_NAME")]
        public string FundShortName { get; set; } = string.Empty;

        [Column("TYPE")]
        public string Type { get; set; } = string.Empty;

        [Column("RECORD_ID")]
        public string RecordId { get; set; } = string.Empty;

        [Column("CGT")]
        public string CGT { get; set; } = string.Empty;

        [Column("NO_OF_UNITS")]
        public string NoOfUnits { get; set; }

        [Column("PLAN_OTHER_CHARGES")]
        public string PlanOtherCharges { get; set; } = string.Empty;

        [Column("GROSS_AMOUNT_DOUBLE")]
        public double GrossAmountDouble { get; set; }

        [Column("LOAD_AMOUNT")]
        public string LoadAmount { get; set; } = string.Empty;

        [Column("PLAN_ID")]
        public string PlanId { get; set; } = string.Empty;

        [Column("BAL_OTHER_CHARGE")]
        public string BalOthCharge { get; set; } = string.Empty;

        [Column("NAV_PER_UNIT")]
        public string NavPerUnit { get; set; } = string.Empty;

        [Column("OPENING_BALANCE")]
        public string OpeningBalance { get; set; } = string.Empty;

        [Column("UNIT_PRICE")]
        public string UnitPrice { get; set; } = string.Empty;

        [Column("ZAKAT_AMOUNT")]
        public string ZakatAmount { get; set; } = string.Empty;

        [Column("NAV_DATE")]
        public DateTime? NavDate { get; set; }

        [Column("NET_AMOUNT")]
        public string NetAmount { get; set; } = string.Empty;

        [Column("BALANCE_UNIT")]
        public double BalanceUnit { get; set; }

        [Column("REPORT_OPTION")]
        public string ReportOption { get; set; } = string.Empty;

        [Column("MAX_NAV_DATE")]
        public DateTime? MaxNavDate { get; set; }

        [Column("BACK_END_LOAD")]
        public string BackEndLoad { get; set; } = string.Empty;

        [Column("TO_DATE")]
        public DateTime? ToDate { get; set; }

        [Column("PLAN_GROSS_AMOUNT_DOUBLE")]
        public double PlanGrossAmountDouble { get; set; }

        [Column("GROSS_AMOUNT")]
        public string GrossAmount { get; set; } = string.Empty;

        [Column("NO_OF_UNITS_POSITIVE")]
        public bool NoOfUnitPositive { get; set; }

        [Column("UNIT_HOLDER_ID")]
        public string UnitHolderId { get; set; } = string.Empty;

        [Column("PLAN_NET_AMOUNT")]
        public string PlanNetAmount { get; set; } = string.Empty;

        [Column("SUM_FUND_WISE_INVESTMENT")]
        public string SumFundWiseInvestment { get; set; } = string.Empty;

        [Column("DIVIDEND_PAYOUT")]
        public string DividendPayout { get; set; }

        [Column("PLAN_SHORT_NAME")]
        public string PlanShortName { get; set; } = string.Empty;

        [Column("NET_AMOUNT_DOUBLE")]
        public double NetAmountDouble { get; set; }

        [Column("PLAN_NET_AMOUNT_DOUBLE")]
        public double PlanNetAmountDouble { get; set; }

        [Column("VPS_FUND_NAME")]
        public string VpsFundName { get; set; } = string.Empty;

        [Column("INSTRUCTION_ID")]
        public int InstructionId { get; set; }

        [Column("FUND_NAME")]
        public string FundName { get; set; } = string.Empty;

        [Column("TRANSACTION")]
        public string Transaction { get; set; } = string.Empty;

        [Column("FUND_WISE_INVESTMENT")]
        public string FundWiseInvestment { get; set; } = string.Empty;

        [Column("PLAN_GROSS_AMOUNT")]
        public string PlanGrossAmount { get; set; } = string.Empty;
        
    }
}