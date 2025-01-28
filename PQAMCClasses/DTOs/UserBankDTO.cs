namespace PQAMCClasses.DTOs
{
    public partial class UserBankDTO
    {
        public int UserBankId { get; set; }
        
        public int UserId { get; set; }

        public int BankId{ get; set; }

        public string? IBANNumber { get; set; }

        public bool? IsIBANVerified { get; set; }

        public string? OneLinkTitle { get; set; }

        public bool IsOBAccount { get; set; }
    }
}
