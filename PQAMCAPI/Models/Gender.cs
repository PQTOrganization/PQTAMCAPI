using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("amc_gender")]
    public partial class Gender
    {
        [Column("gender_id")]
        public int GenderId { get; set; }
        [Column("name")]
        public string Name { get; set; } = "";
    }
}