namespace PQAMCClasses.CloudDTOs
{
    public class GetPlanListResponseDTO
    {
        public List<PlanDTO> PlanList { get; set; }
        public string Action { get; set; } = "";
        public string ResponseMessage { get; set; } = "";
        public string ResponseCode { get; set; } = "";

    }
}
