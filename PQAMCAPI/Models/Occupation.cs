using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("amc_occupation")]
    public partial class Occupation
    {
        [Column("occupation_id")]
        public int OccupationId { get; set; }
        [Column("name")]
        public string Name { get; set; } = "";
        [Column("ITMINDS_NAME")]
        public string ITMindsName { get; set; }
    }
}