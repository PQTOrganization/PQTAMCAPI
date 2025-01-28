using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("AMC_FOLIO_NUMBER")]
    public partial class AMCFolioNumber
    {
        public AMCFolioNumber()
        {

        }

        [Column("FOLIO_NUMBER")]
        public string FolioNumber { get; set; }
    }
}