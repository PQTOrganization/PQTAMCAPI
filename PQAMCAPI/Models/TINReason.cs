using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("TIN_REASON")]
    public partial class TINReason
    {
        [Column("TIN_REASON_ID")]
        public int TINReasonId { get; set; }

        [Column("NAME")]
        public string Name { get; set; } = "";
    }
}