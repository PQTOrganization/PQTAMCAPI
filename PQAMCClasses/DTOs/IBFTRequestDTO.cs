using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PQAMCClasses.DTOs
{
    public class IBFTRequestDTO
    {
        public string IBANNumber { get; set; } = "";
        public string CNIC { get; set; } = "";
        public int BankID { get; set; }
        public int UserApplicationId { get; set; }
    }
}
