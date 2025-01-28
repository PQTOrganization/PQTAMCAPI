namespace PQAMCClasses.DTOs
{
    public class VPSPlanDTO
    {
        public int VPSPlanId { get; set; }
        public string AllocationScheme { get; set; } = "";
        public int AccountCategoryID { get; set; }
        public int DebtSubFund { get; set; }
        public int EquitySubFund { get; set; }
        public int MoneyMarketSubFund { get; set; }
    }
}
