namespace PQAMCClasses.CloudDTOs
{
    public class SubmitSubSaleRequestDTO
    {
        public string? folioType { get; set; }
        public string? folioId { get; set; }
        public string? planFundId { get; set; }
        public string? investmentAmount { get; set; }
        public string? investmentType { get; set; }
        public string? bankId { get; set; }
        public string? branchId { get; set; }
        public string? accountNumber { get; set; }
        public string? isRealized { get; set; }
        public string formReceivingDateTime { get; set; }
        public string? investmentProofUpload { get; set; }

    }  
}
