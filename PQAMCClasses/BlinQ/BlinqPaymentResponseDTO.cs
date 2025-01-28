namespace PQAMCClasses.BlinQ
{
    public class BlinqPaymentResponseDTO
    {
        public string status { get; set; }
        public string message { get; set; }
        public string ordId { get; set; }
        public string paymentCode { get; set; }
        public int refNumber { get; set; }
        public string pBank { get; set; }
        public decimal amountPaid { get; set; }
        public string paidOn { get; set; }
        public decimal txnFee { get; set; }
        public decimal netAmount { get; set; }
        public string encryptedFormData { get; set; }
    }
}
