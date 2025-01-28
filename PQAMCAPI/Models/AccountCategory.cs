using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("amc_account_category")]
    public partial class AccountCategory
    {
        [Column("account_category_id")]
        public int AccountCategoryId { get; set; }

        [Column("name")]
        public string Name { get; set; } = "";

        [Column("title")]
        public string Title { get; set; } = "";

        [Column("subtitle")]
        public string SubTitle { get; set; } = "";

        [Column("description")]
        public string Description { get; set; } = "";

        [Column("display_order")]
        public int DisplayOrder { get; set; }

        [Column("folio_type")]
        public string FolioType { get; set; }
    }
}