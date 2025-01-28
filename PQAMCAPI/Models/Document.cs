using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("AMC_DOCUMENT")]
    public partial class Document
    {
        [Column("DOCUMENT_ID")]
        public int DocumentId { get; set; }

        [Column("DOCUMENT_CODE")]
        public string DocumentCode { get; set; } = "";

        [Column("SHORT_NAME")]
        public string ShortName { get; set; } = "";
        
        [Column("LONG_NAME")]
        public string LongName { get; set; } = "";

        [Column("IS_MANDATORY")]        
        public bool IsMandatory { get; set; }
    }
}