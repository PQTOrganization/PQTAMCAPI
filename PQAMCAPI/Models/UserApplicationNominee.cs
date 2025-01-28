using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("AMC_USER_APPLICATION_NOMINEE")]
    public partial class UserApplicationNominee
    {
        public UserApplicationNominee()
        {
            
        }

        [Column("USER_APPLICATION_NOMINEE_ID")]
        public int UserApplicationNomineeId { get; set; }

        [Column("USER_APPLICATION_ID")]
        public int UserApplicationId { get; set; }

        [Column("SERIAL_NUMBER")]
        public int SerialNumber { get; set; }

        [Column("NAME")]
        public string? Name { get; set; }

        [Column("RELATIONSHIP")]
        public string? Relationship { get; set; }

        [Column("SHARE")]
        public int? Share { get; set; }

        [Column("RESIDENTIAL_ADDRESS")]
        public string? ResidentialAddress { get; set; }

        [Column("TELEPHONE_NUMBER")]
        public string? TelephoneNumber { get; set; }

        [Column("BANK_ACCOUNT_DETAIL")]
        public string? BankAccountDetail { get; set; }

        [Column("CNIC")]
        public string? CNIC { get; set; }
    }
}