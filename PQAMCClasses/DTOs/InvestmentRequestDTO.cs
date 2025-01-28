namespace PQAMCClasses.DTOs
{
    public class InvestmentRequestDTO
    {
        public int InvestmentRequestId { get; set; }
        public DateTime RequestDate { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = "";
        public string FolioNumber { get; set; }
        public string CNIC { get; set; } = "";
        public int FundId { get; set; }
        public string FundName { get; set; } = "";
        public decimal InvestmentAmount { get; set; }
        public short PaymentMode { get; set; }
        public decimal FrontEndLoad { get; set; }
        public decimal NavApplied { get; set; }
        public string? ProofOfPayment { get; set; } = "";
        public int? BankId { get; set; }
        public string? BranchId { get; set; }
        public string? BankName { get; set; }
        public string? AccountNumber { get; set; } = "";
        public string? OnlinePaymentReference { get; set; }
        public short RequestStatus { get; set; }
        public bool IsInitialInvestment { get; set; } = false;

        public string? ITMindsBankID { get; set; } = "";
    }
}
