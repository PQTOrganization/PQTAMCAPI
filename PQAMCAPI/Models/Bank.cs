using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("amc_bank")]
    public partial class Bank
    {
        [Column("bank_id")]
        public int BankId { get; set; }
        [Column("name")]
        public string Name { get; set; } = "";
    }
}