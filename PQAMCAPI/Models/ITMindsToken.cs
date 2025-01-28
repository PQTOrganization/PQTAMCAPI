using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("AMC_ITMINDS_TOKEN")]
    public partial class ITMindsToken
    {
        public ITMindsToken()
        {           
        }

        [Column("DateTime")]
        public DateTime DateTime { get; set; }

        [Column("TOKEN")]
        public string Token { get; set; }
    }
}