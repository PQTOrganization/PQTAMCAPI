using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("AMC_USER_BANK")]
    public partial class UserBank
    {
        [Column("USER_BANK_ID")]
        public int UserBankId { get; set; }
        
        [Column("USER_ID")]
        public int UserId { get; set; }

        [Column("BANK_ID")]
        public int BankId{ get; set; }

        [Column("IBAN_NUMBER")]
        public string? IBANNumber { get; set; }

        [Column("IS_IBAN_VERIFIED")]
        public bool? IsIBANVerified { get; set; }

        [Column("ONE_LINK_TITLE")]
        public string? OneLinkTitle { get; set; }

        [Column("IS_OB_ACCOUNT")]
        public bool IsOBAccount { get; set; }
    }
}
