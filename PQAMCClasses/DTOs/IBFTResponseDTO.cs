using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PQAMCClasses.DTOs
{
    public class IBFTResponseDTO
    {
        public Boolean IsSuccess { get; set; }
        public string ErrorMessage { get; set; } = "";
        public string AccountTitle { get; set; } = "";
    }
}
