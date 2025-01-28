namespace PQAMCClasses.CloudDTOs
{
    public class GetAccountStatementCISVPSReportRequestDTO
    {
        public string folioNumber { get; set; } = string.Empty;
        public string statementType { get; set; } = string.Empty;
        public string? asOnDate { get; set; } = string.Empty;
        public string? fromDate { get; set; } = string.Empty;
        public string? toDate { get; set;} = string.Empty;
    }
}
