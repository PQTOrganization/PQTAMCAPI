using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PQAMCClasses.CloudDTOs
{
    public class InvestorAccountTransactionDTO
    {
        public string NarrationText { get; set; } = "";
        public List<int> DividendPayout { get; set; }
        public List<InvestorTransactionDTO> InvTranscation { get; set;}
        public double ClosingBalance { get; set; }
        public string PlanName { get; set; }
        public string FundShortName { get; set; }
        public string VPSFundName { get; set; }
        public string OpeningBalanceDouble { get; set; }
        public string FundName { get; set; }
        public double TotalNavAmount { get; set; }
        public double OpeningBalance { get; set; }
    }
}
