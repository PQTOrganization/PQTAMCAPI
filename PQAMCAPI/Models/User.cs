using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("amc_user")]
    public class User
    {
        public User()
        {
            UserRefreshToken = new HashSet<UserRefreshToken>();
            UserToken = new HashSet<UserToken>();
            UserClaim = new HashSet<UserClaim>();
        }

        [Column("user_id")]
        public int UserId { get; set; }
        
        [Column("first_name")]
        public string? FirstName { get; set; }
        
        [Column("last_name")]
        public string? LastName { get; set; }
        
        [Column("mobile_number")]
        public string MobileNumber { get; set; }
        
        [Column("email")]
        public string Email { get; set; }

        [Column("email_confirmed")]
        public bool EmailConfirmed { get; set; }
        
        [Column("otp")]
        public string? OTP { get; set; }
        
        [Column("profile_image")]
        public string? ProfileImage { get; set; }
        
        [Column("registration_date")]
        public DateTime RegistrationDate { get; set; }
        [Column("folio_number")]
        public string FolioNumber { get; set; }
        [Column("cnic")]
        public string? CNIC { get; set; }

        [Column("LAST_OTP_GENERATE_DATETIME")]
        public DateTime? LastOTPGenerateDateTime { get; set; }
        
        [Column("INCORRECT_OTP_ATTEMPTS")]
        public int IncorrectOTPAttempts { get; set; }

        [Column("USER_ACCOUNT_STATUS")]
        public string AccountStatus { get; set; }
        public virtual ICollection<UserRefreshToken> UserRefreshToken { get; set; }
        public virtual ICollection<UserToken> UserToken { get; set; }
        public virtual ICollection<UserClaim> UserClaim { get; set; }
    }
}