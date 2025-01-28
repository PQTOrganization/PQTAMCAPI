namespace PQAMCClasses.CloudDTOs
{
    public class SubmitRedemptionRequestDTO
    {
        public string? folioType { get; set; }
        public string? schemeOption { get; set; }
        public string? folioNo { get; set; }
        public string? planFundId { get; set; }
        public string? redemptionAmount { get; set; }
        public string? redemptionInTermsOf { get; set; }
        public string? allUnits { get; set; }
        public string? paymentMode { get; set; }
        public string? bankId { get; set; }
        public string? bankAccountNo { get; set; }
        public string? formReceivingDateTime { get; set; }
        public string? fileUpload { get; set; }
    }
}
