using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PQAMCClasses.DTOs
{
    public class ITMindsResponseDTO
    {
        public List<Object> list { get; set; }
        public string ResponseMessage { get; set; } = "";
        public string ResponseCode { get; set; } = "";
        public string Action { get; set; }
    }
}
