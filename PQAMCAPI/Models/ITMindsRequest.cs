using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("AMC_ITMINDS_REQUEST")]
    public partial class ITMindsRequest
    {
        public ITMindsRequest()
        {           
        }

        [Column("ID")]
        public int Id { get; set; }

        [Column("USER_APPLICATION_ID")]
        public int UserApplicationID { get; set; }

        [Column("REQUEST_TYPE")]
        public string RequestType { get; set; }

        [Column("REQUEST")]
        public string Request { get; set; }

        [Column("RESPONSE")]
        public string Response { get; set; }

        [Column("REQUEST_DATETIME")]
        public DateTime RequestDateTime { get; set; }

        [Column("PARENT_ID")]
        public int? ParentID { get; set; }
    }
}