namespace PQAMCClasses.CloudDTOs
{
    public class GetCISFundListResponseDTO
    {
        public List<CISFundDTO> FundList { get; set; }
        public string ResponseMessage { get; set; } = "";
        public string ResponseCode { get; set; } = "";

    }
}
