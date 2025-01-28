using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    public partial class UserApplicationDocument
    {
        [Column("USER_APPLICATION_DOCUMENT_ID")]
        public int UserApplicationDocumentId { get; set; }

        [Column("DOCUMENT_ID")]
        public int DocumentId { get; set; }

        [Column("USER_APPLICATION_ID")]
        public int UserApplicationId { get; set; }

        [Column("DOCUMENT")]
        public string? Document { get; set; } = "";

        [Column("DOC_TYPE")]
        public string? DocType { get; set; } = "";

        [Column("FILENAME")]
        public string? Filename { get; set; } = "";

        [Column("CREATED_DATE")]
        public DateTime CreatedDate { get; set; }

        [Column("MODIFIED_DATE")]
        public DateTime? ModifiedDate { get; set; }
    }
}
