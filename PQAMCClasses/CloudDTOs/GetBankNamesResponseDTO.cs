namespace PQAMCClasses.CloudDTOs
{
    public class GetBankNamesResponseDTO
    {
        public List<BankNameDTO> BankNames { get; set; }
        public string Action { get; set; } = "";
        public string ResponseMessage { get; set; } = "";
        public string ResponseCode { get; set; } = "";
    }
}
