using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("AMC_USER_APPLICATION_DISCREPANCY")]
    public partial class UserApplicationDiscrepancy
    {
        [Column("USER_APPLICATION_DISCREPANCY_ID")]
        public int UserApplicationDiscrepancyId { get; set; }
        
        [Column("DISCREPANCY_DATE")]
        public DateTime DiscrepancyDate { get; set; }

        [Column("USER_APPLICATION_ID")]
        public int UserApplicationId { get; set; }

        [Column("APPLICATION_DATA")]
        public string ApplicationData { get; set; } = "";

        [Column("DISCREPANT_FIELDS")]
        public string DiscrepantFields { get; set; } = "";
    }
}
