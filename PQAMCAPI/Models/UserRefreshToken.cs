using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("amc_user_refresh_token")]
    public class UserRefreshToken
    {
        [Column("user_refresh_token_id")]
        public int UserRefreshTokenId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }
        [Column("refresh_token")]
        public string RefreshToken { get; set; } = "";
        [Column("token_date")]
        public DateTime TokenDate { get; set; }

        public virtual User User { get; set; }
    }
}
