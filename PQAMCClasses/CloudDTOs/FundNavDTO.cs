namespace PQAMCClasses.CloudDTOs
{
    public class FundNAVDTO
    {
        public string NAVDate { get; set; } = "";
        public string NotApplicableCheck { get; set; } = "";
        public string totalAssets { get; set; } = "";
        public string navSuspend { get; set; } = "";
        public string applicableNavDate { get; set; } = "";
        public string netAssets { get; set; } = "";
        public string exNav { get; set; } = "";
        public string totalLiabilities { get; set; } = "";
        public string fundShortName { get; set; } = "";
        public string navOrExnav { get; set; } = "";
        public string navPerUnit { get; set; } = "";
        public string totalOutstandingUnits { get; set; } = "";
    }
}
