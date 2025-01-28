using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("amc_income_source")]
    public partial class IncomeSource
    {
        [Column("income_source_id")]
        public int IncomeSourceId { get; set; }
        [Column("name")]
        public string Name { get; set; } = "";
        [Column("ITMINDS_NAME")]
        public string ITMindsName { get; set; }
    }
}