namespace PQAMCClasses.DTOs
{
    public class InitialInvestmentRequestDTO
    {
        public int InvestmentRequestId { get; set; }
        public DateTime RequestDate { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = "";
        public int UserApplicationID { get; set; }
        public string CNIC { get; set; } = "";
        public int FundId { get; set; }
        public string FundName { get; set; } = "";
        public decimal InvestmentAmount { get; set; }
        public short PaymentMode { get; set; }
        public decimal FrontEndLoad { get; set; }
        public decimal NavApplied { get; set; }
        public string? ProofOfPayment { get; set; } = "";
        public int? BankId { get; set; }
        public string? OnlinePaymentReference { get; set; }
        public short RequestStatus { get; set; }
    }
}
