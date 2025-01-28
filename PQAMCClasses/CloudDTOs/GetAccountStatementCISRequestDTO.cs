namespace PQAMCClasses.CloudDTOs
{
    public class GetAccountStatementCISRequestDTO
    {
        public string folioNo { get; set; } = string.Empty;
        public string statementType { get; set; } = string.Empty;
        public string fundPlanId { get; set; } = string.Empty;
        public string fromDate { get; set; } = string.Empty;
        public string toDate { get; set; } = string.Empty;
      
    }
}
