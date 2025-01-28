using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("amc_contact_ownership")]
    public partial class ContactOwnerShip
    {

        [Column("contact_ownership_id")]
        public int ContactOwnerShipId { get; set; }
        [Column("name")]
        public string Name { get; set; } = "";
    }
}