namespace PQAMCClasses.DTOs
{
    public class TransactionDTO
    {
        public string TransactionID { get; set; } = "";
        public string Fundame { get; set; } = "";
        public DateTime ProcessDate { get; set; }        
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public string Transaction { get; set; } = "";
    }
}
