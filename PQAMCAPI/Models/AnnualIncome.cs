using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("amc_annual_income")]
    public partial class AnnualIncome
    {
        [Column("annual_income_id")]
        public int AnnualIncomeId { get; set; }

        [Column("name")]
        public string Name { get; set; } = "";
    }
}