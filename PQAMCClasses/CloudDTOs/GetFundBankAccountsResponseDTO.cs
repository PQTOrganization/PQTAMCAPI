namespace PQAMCClasses.CloudDTOs
{
    public class GetFundBankAccountsResponseDTO
    {
        public List<FundBankAccountDTO> FundBankAccounts { get; set; }
        public string ResponseMessage { get; set; } = "";
        public string ResponseCode { get; set; } = "";
    }
}
