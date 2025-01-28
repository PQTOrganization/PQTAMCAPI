using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("amc_area")]
    public partial class Area
    {
        [Column("area_id")]
        public int AreaId { get; set; }
        [Column("city_id")]
        public int CityId { get; set; }
        [Column("name")]
        public string Name { get; set; } = "";

        public virtual City City { get; set; }
    }
}