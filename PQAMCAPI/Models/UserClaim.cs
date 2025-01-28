using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("amc_user_claim")]
    public class UserClaim
    {
        [Column("user_claim_id")]
        public int UserClaimId { get; set; }
        [Column("claim_type")]
        public string ClaimType { get; set; } = "";
        [Column("claim_value")]
        public string ClaimValue { get; set; } = "";
        [Column("user_id")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
