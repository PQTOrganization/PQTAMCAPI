namespace PQAMCClasses.DTOs
{
    public class FundBankDTO
    {
        public int FundBankId { get; set; }
        public int FundId { get; set; }
        public string BranchId { get; set; }
        public string BankName { get; set; } = "";
        public string BranchName { get; set; } = "";
        public string Location { get; set; } = "";
        public string AccountTitle { get; set; } = "";
        public string AccountNo { get; set; } = "";
        public string IBANNumber { get; set; } = "";

        public string? BankId { get; set; } = "";
    }
}
