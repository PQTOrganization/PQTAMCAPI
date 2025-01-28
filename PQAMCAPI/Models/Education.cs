using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("amc_education")]
    public partial class Education
    {
        [Column("education_id")]
        public int EducationId { get; set; }
        [Column("name")]
        public string Name { get; set; } = "";
        [Column("ITMINDS_NAME")]
        public string ITMindsName { get; set; }
    }
}