using PQAMCAPI.Models;
using PQAMCClasses.CloudDTOs;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface ICloudService
    {
        Task<SubmitSaleResponseDTO> PostUserApplicationData(UserApplication Application, User User, InvestmentRequestDTO InitialInvestment,
            List<UserBank> UserBanks, FundDTO InvestmentFund, AccountCategory AccountCategory);
        Task<List<FolioDTO>> GetFolioList();
        Task<List<AMCFundNAVDTO>> GetAMCFundNAVList();
        Task<List<InvestorAccountTransactionDTO>> GetAccountStatementCIS(GetAccountStatementCISRequestDTO ActStmtReq);
        Task<List<BankNameDTO>> GetBanksList();
        Task<AccountStatementReportDTO> GetAccountStatementReport(GetAccountStatementReportRequestDTO ActStmtReportReq);
        Task<List<InvestorAccountTransactionDTO>> GetAccountStatementVPS(GetAccountStatementVPSRequestDTO ActStmtReq);
        Task<SubmitDigitalAccountResponseDTO> SubmitDigitalAccount(UserApplication Application, List<UserBank> UserBanks);
        Task<SubmitResponseDTO> SubmitRedemption(RedemptionRequestDTO RedemptionRequest, UserApplicationDTO Application,
             FundDTO Fund, User User);
        Task<SubmitResponseDTO> SubmitConversion(FundTransferRequestDTO FundTransferRequest, FundDTO FromFund, FundDTO ToFund);
        Task<SubmitResponseDTO> SubmitSubsale(InvestmentRequestDTO InvestmentRequest, UserApplicationDTO Application, FundDTO Fund, AccountCategory AccountCategory);
        Task<List<FundBankAccountDTO>> GetFundBankAccounts(GetFundBankAccountsRequestDTO FundBankAccountsRequest);
        Task<List<PlanDTO>> GetPlanList(GetPlanListRequestDTO PlanListRequest);
        Task<List<CISFundDTO>> GetCISFundList(GetCISFundListRequestDTO CISFundListRequest);
        Task<List<int>> GetVPSPlanSequence();
        Task<FundNAVDTO> GetFundNAV(GetFundNAVRequestDTO FundNAVRequestDTO);
        Task<FundNAVDTO> GetFundNAV(string FundShortName);
        Task<List<FolioBankDTO>> GetFolioBankListForUser(User User);
        Task<List<InvestorAccountTransactionDTO>> GetAccountStatementCISFromCloud(string FolioNumber);
        Task<AccountStatementReportDTO> GetAccountStatementCISVPSReport(GetAccountStatementCISVPSReportRequestDTO ActStmtReportReq);
    }
}
