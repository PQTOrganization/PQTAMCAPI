using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PQAMCClasses.DTOs
{
    public class TokenModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public int AccountCategoryID { get; set; }
        public string SecurityKeys { get; set; }    
    }
}
