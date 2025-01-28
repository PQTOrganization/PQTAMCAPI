using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("amc_country")]
    public partial class Country
    {
        public Country()
        { 
            City = new HashSet<City>();
            Name = "";
            CurrencyName = "";
            CurrencySymbol = "";
            CountryCode = "";
        }

        [Column("country_id")]
        public int CountryId { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("currency_name")] 
        public string CurrencyName { get; set; }
        [Column("currency_symbol")] 
        public string CurrencySymbol { get; set; }
        [Column("country_code")] 
        public string CountryCode { get; set; }
        public virtual ICollection<City> City { get; set; }

    }
}
