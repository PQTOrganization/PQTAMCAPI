namespace PQAMCClasses.DTOs
{
    public class PendingTransactionDTO
    {
        public string RequestType { get; set; } = "";
        public DateTime RequestDate { get; set; }
        public int FundId { get; set; }
        public string FundName { get; set; } = "";
        public string ToFundName { get; set; } = "";
        public decimal Amount { get; set; }
        public int? PaymentMode { get; set; }
        public string? PaymentReference { get; set; }
        public string? BankID { get; set; } = "";
        public string? BankAccountNo { get; set; } = "";
    }
}
