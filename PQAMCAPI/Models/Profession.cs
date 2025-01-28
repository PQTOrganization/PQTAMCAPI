using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("amc_profession")]
    public partial class Profession
    {
        [Column("profession_id")]
        public int ProfessionId { get; set; }
        [Column("name")]
        public string Name { get; set; } = "";
    }
}