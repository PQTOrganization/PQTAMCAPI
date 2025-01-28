using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PQAMCClasses.CloudDTOs
{
    public class InvestorTransactionDTO
    {
        public double NoOfUnitsDouble { get; set; }
        public double UnitPriceDouble { get; set; }
        //public DateTime? MaxNavDateFund { get; set; }
        public DateTime? MaxNavDateFund { get; set; }
        public int? OtherCharges { get; set; }
        public string PlanName { get; set; } = string.Empty;
        public string FundShortName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string RecordId { get; set; } = string.Empty;
        public string CGT { get; set; } = string.Empty;
        public string NoOfUnits { get; set; }
        public string PlanOtherCharges { get; set; } = string.Empty;
        public double GrossAmountDouble { get; set; }
        public string LoadAmount { get; set; } = string.Empty;
        public string PlanId { get; set; } = string.Empty;
        public string BalOthCharge { get; set; } = string.Empty;
        public string NavPerUnit { get; set; } = string.Empty;
        public string OpeningBalance { get; set; } = string.Empty;
        public string UnitPrice { get; set; } = string.Empty;
        public string ZakatAmount { get; set; } = string.Empty;
        public DateTime? NavDate { get; set; }
        public string NetAmount { get; set; } = string.Empty;
        public double BalanceUnit { get; set; }
        public string ReportOption { get; set; } = string.Empty;
        public DateTime? MaxNavDate { get; set; }
        public string BackEndLoad { get; set; } = string.Empty;
        public DateTime? ToDate { get; set; }
        public double PlanGrossAmountDouble { get; set; }
        public string GrossAmount { get; set; } = string.Empty;
        public bool NoOfUnitPositive { get; set; }
        public string UnitHolderId { get; set; } = string.Empty;
        public string PlanNetAmount { get; set; } = string.Empty;
        public string SumFundWiseInvestment { get; set; } = string.Empty;
        public int[] DividendPayout { get; set; }
        public string PlanShortName { get; set; } = string.Empty;
        public double NetAmountDouble { get; set; }
        public double PlanNetAmountDouble { get; set; }
        public string VpsFundName { get; set; } = string.Empty;
        public int InstructionId { get; set; }
        public string FundName { get; set; } = string.Empty;
        public string Transaction { get; set; } = string.Empty;
        public string FundWiseInvestment { get; set; } = string.Empty;
        public string PlanGrossAmount { get; set; } = string.Empty;

    }
}
