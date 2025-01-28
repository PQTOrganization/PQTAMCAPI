namespace PQAMCClasses.DTOs
{
    public class DashboardSummaryDTO
    {
        public List<LabelWiseInvestmentDTO> CategoryWiseBreakup { get; set; }   
        public List<LabelWiseInvestmentDTO> FundWiseSummary { get; set; }  
        public InvestmentSummaryDTO InvestmentSummary { get; set; } 
        public List<LabelWiseInvestmentDTO> RiskWiseBreakup { get; set; }   
    }
}
