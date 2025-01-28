using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("amc_city")]
    public partial class City
    {
        City()
        {
            Area = new HashSet<Area>();
            Name = "";
        }

        [Column("city_id")]
        public int CityId { get; set; }
        [Column("country_id")] 
        public int CountryId { get; set; }
        [Column("name")]
        public string Name { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Area> Area { get; set; }
    }
}