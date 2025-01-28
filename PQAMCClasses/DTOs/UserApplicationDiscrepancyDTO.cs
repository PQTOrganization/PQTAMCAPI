namespace PQAMCClasses.DTOs
{
    public class UserApplicationDiscrepancyDTO
    {
        public int UserApplicationDiscrepancyId { get; set; }
        
        public DateTime DiscrepancyDate { get; set; }

        public int UserApplicationId { get; set; }

        public string ApplicationData { get; set; } = "";

        public string DiscrepantFields { get; set; } = "";
    }
}
