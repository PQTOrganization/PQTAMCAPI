namespace PQAMCClasses.DTOs
{
    public class FundTransferRequestDTO
    {
        public int FundTransferRequestId { get; set; }
        public DateTime RequestDate { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = "";
        public string FolioNumber { get; set; }
        public string CNIC { get; set; } = "";

        public int FromFundId { get; set; }
        public string FromFundName { get; set; } = "";
        public decimal TransferAmount { get; set; }
        public decimal FromNavApplied { get; set; }
        public decimal FromNumOfUnits { get; set; }
        public int ToFundId { get; set; }
        public string ToFundName { get; set; } = "";
        public decimal ToNavApplied { get; set; }
        public decimal ToNumOfUnits { get; set; }
        public short RequestStatus { get; set; }
    }
}
