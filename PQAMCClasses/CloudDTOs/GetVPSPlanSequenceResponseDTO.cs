namespace PQAMCClasses.CloudDTOs
{
    public class GetVPSPlanSequenceResponseDTO
    {
        public List<int> VPSPlanSeqeunce { get; set; }
        public string ResponseMessage { get; set; } = "";
        public string ResponseCode { get; set; } = "";
        public string Action { get; set; } = "";
    }
}
