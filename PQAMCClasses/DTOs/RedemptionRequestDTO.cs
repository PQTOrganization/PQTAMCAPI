namespace PQAMCClasses.DTOs
{
    public class RedemptionRequestDTO
    {
        public int RedemptionRequestId { get; set; }
        public DateTime RequestDate { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = "";
        public string FolioNumber { get; set; }
        public string CNIC { get; set; } = "";

        public int FundId { get; set; }
        public string FundName { get; set; } = "";
        public decimal RedemptionAmount { get; set; }
        public decimal BackEndLoad { get; set; }
        public decimal NavApplied { get; set; }
        public short RequestStatus { get; set; }
        public string BankID { get; set; } = "";
        public string BankAccountNo { get; set; } = "";
    }
}
