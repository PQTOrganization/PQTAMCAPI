using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCClasses.DTOs
{
    public class UserApplicationDocumentDTO
    {
        public int DocumentId { get; set; }
        public string DocumentCode { get; set; } = "";
        public string ShortName { get; set; } = "";
        public string LongName { get; set; } = "";
        public bool IsMandatory { get; set; }
        public int? UserApplicationDocumentId { get; set; }
        public int? UserApplicationId { get; set; }
        public string? Document { get; set; } = "";
        public string? DocType { get; set; } = "";
        public string? Filename { get; set; } = "";
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
