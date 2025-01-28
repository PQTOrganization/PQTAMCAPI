namespace PQAMCClasses.DTOs
{
    public class FundDTO
    {
        public int FundId { get; set; }
        public string FundName { get; set; } = "";
        public string FundShortName { get; set; } = "";
        public decimal FrontEndLoadPercentage { get; set; }
        public decimal BackEndLoadPercentage { get; set; }
        public int RiskCategoryId { get; set; }
        public string RiskCategoryName { get; set; } = "";
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = "";
        public decimal LastNav { get; set; }
        public decimal OfferNav { get; set; }
        public decimal PurchaseNav { get; set; }
        public decimal MinInvestmentLimit { get; set; }
        public string ITMindsFundID { get; set; } = "";
        public string ITMindsFundShortName { get; set; } = "";
    }
}
