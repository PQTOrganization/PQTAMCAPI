using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("AMC_VPS_PLAN")]
    public partial class VPSPlan
    {
        [Column("VPS_PLAN_ID")]
        public int VPSPlanId { get; set; }

        [Column("ACCOUNT_CATEGORY_ID")] 
        public int? AccountCategoryID { get; set; } = null;

        [Column("ALLOCATION_SCHEME")]
        public string AllocationScheme { get; set; } = "";

        [Column("DEBT_SUB_FUND")]
        public int? DebtSubFund { get; set; } = null;

        [Column("EQUITY_SUB_FUND")]
        public int? EquitySubFund { get; set; } = null;

        [Column("MONEY_MARKET_SUB_FUND")]
        public int? MoneyMarketSubFund { get; set; } = null;
    }
}