namespace PQAMCClasses.DTOs
{
    public class FundPositionDTO
    {
        public int FundId { get; set; }
        public string FundName { get; set; } = "";
        public decimal CurrentValue { get; set; }
        public decimal NumberOfUnits { get; set; }
        public decimal ProfitLoss { get; set; } = 1;
        public decimal LastNav { get; set; }
        public decimal OfferNav { get; set; }
        public decimal PurchaseNav { get; set; }
        public decimal FrontEndLoadPercentage { get; set; }
        public decimal BackEndLoadPercentage { get; set; }
    }
}
