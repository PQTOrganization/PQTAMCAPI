namespace PQAMCClasses.CloudDTOs
{
    public class GetFolioBankListResponseDTO
    {
        public List<FolioBankDTO> BankList { get; set; }
        public string ResponseMessage { get; set; } = "";
        public string ResponseCode { get; set; } = "";
        public string Action { get; set; }

    }
}
