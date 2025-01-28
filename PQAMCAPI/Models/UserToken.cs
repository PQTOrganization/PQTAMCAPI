using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("amc_user_token")]
    public class UserToken
    {
        [Column("user_token_id")]
        public int UserTokenId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }
        [Column("token")]
        public string Token { get; set; }
        [Column("token_date")]
        public DateTime TokenDate { get; set; }

        public virtual User User { get; set; }
    }
}
