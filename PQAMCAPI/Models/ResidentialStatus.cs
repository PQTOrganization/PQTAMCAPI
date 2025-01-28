using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("amc_residential_status")]
    public partial class ResidentialStatus
    {
        [Column("residential_status_id")]
        public int ResidentialStatusId { get; set; }
        [Column("name")]
        public string Name { get; set; } = "";
    }
}