using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PQAMCClasses.CloudDTOs
{
    public class GetAccountStatementCISResponseDTO
    {
        public List<InvestorAccountTransactionDTO> InvestorAccountTransaction { get; set; }
        public string Action { get; set; } = "";
        public string ResponseMessage { get; set; } = "";
        public string ResponseCode { get; set; } = "";

    }
}
