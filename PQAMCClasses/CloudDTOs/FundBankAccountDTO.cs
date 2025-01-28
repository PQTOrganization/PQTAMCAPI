namespace PQAMCClasses.CloudDTOs
{
    public class FundBankAccountDTO
    {
        public string BranchId { get; set; }
        public string FundPlanId { get; set; }
        public string Name { get; set; } = "";
        public string FundName { get; set; } = "";
        public string BankName { get; set; } = "";
        public string AccountNumber { get; set; } = "";
    }
}
