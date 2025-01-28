using AutoMapper;
using PQAMCAPI.Interfaces.Services;
using PQAMCClasses.CloudDTOs;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Services.Domain
{
    public class FundService : IFundService
    {
        const string PACKAGE_NAME = "AMC_INVESTMENT_PKG";

        private readonly IStoreProcedureService _spService;
        private readonly ICloudService _cloudService;
        private readonly IFundDBService _fundDBService;
        private readonly IMapper _mapper;
        public FundService(IStoreProcedureService spService, ICloudService cloudService, IFundDBService fundDBService, IMapper mapper)
        {
            _spService = spService;
            _fundDBService = fundDBService;
            _cloudService = cloudService;
            _mapper = mapper;
        }

        public async Task<List<FundDTO>> GetFundsAsync()
        {
            List<FundDTO> FundList =  await _spService.GetAllSP<FundDTO>(PACKAGE_NAME + ".GET_FUNDS", -1);
            return FundList;
        }

        public async Task<List<FundDTO>> GetFundsByAccountCategoryAsync(int AccountCategoryID)
        {
            List<FundDTO> FundList = await _spService.GetAllSP<FundDTO>(PACKAGE_NAME + ".GET_FUNDS_WITH_ACCTCATEGORY", AccountCategoryID);
            return FundList;
        }
        
        public async Task<List<FundBankDTO>> GetAllFundsWithBankAsync()
        {
            return await _spService.GetAllSP<FundBankDTO>(PACKAGE_NAME + ".GET_FUNDS_WITH_BANKS", -1);
        }

        public async Task<List<FundBankDTO>> GetAllFundsWithBankFromCloudAsync()
        {
            List<FundBankDTO> Response = new List<FundBankDTO>();
            List<AMCFundNAVDTO> AMCFundList = await _cloudService.GetAMCFundNAVList();
            List<FundDTO> Funds = await GetFundsAsync();

            List<BankNameDTO> BankList = await _cloudService.GetBanksList();

            GetFundBankAccountsRequestDTO FundBankRequest = new GetFundBankAccountsRequestDTO();
            FundBankRequest.accountType = "CIS";

            foreach(AMCFundNAVDTO AMCFund in AMCFundList)
            {
                FundDTO FundDB = Funds.Where(x => x.ITMindsFundShortName == AMCFund.FundShortName).FirstOrDefault();

                if (FundDB != null)
                {
                    FundBankRequest.fundPlanId = FundDB.ITMindsFundID;

                    List<FundBankAccountDTO> Accounts = await _cloudService.GetFundBankAccounts(FundBankRequest);
                    if (Accounts != null)
                    {
                        foreach (FundBankAccountDTO acct in Accounts)
                        {
                            FundBankDTO FundBank = _mapper.Map<FundBankDTO>(acct);

                            FundBank.BankId = BankList.Where(x => x.BankName == acct.BankName)?.FirstOrDefault()?.BankId;
                            FundBank.FundId = FundDB.FundId;
                            Response.Add(FundBank);
                        }
                    }
                }
            }
            
            return Response;
        }
        public async Task<List<FundDTO>> GetFundsFromCloudAsync()
        {
            List<AMCFundNAVDTO> AMCFundList = await _cloudService.GetAMCFundNAVList();
            List<FundDTO> FundDB = await GetFundsAsync();
            List<FundDTO> FundList = new List<FundDTO>();
            
            foreach(AMCFundNAVDTO AMCFund in AMCFundList)
            {               
                //FundNAVDTO FundNAV = await _cloudService.GetFundNAV(AMCFund.FundShortName);

                FundDTO Fund = FundDB.Where(x => x.ITMindsFundShortName == AMCFund.FundShortName).FirstOrDefault();
                
                if (Fund != null)
                {
                    FundNAVDTO FundNAV = await GetFundNAVAsync(Fund.FundId);

                    //Fund.FundId = index++;
                    Fund.OfferNav = decimal.Parse(FundNAV.navPerUnit);
                    Fund.LastNav = decimal.Parse(FundNAV.navPerUnit);
                    Fund.FundName = AMCFund.FundName;
                    Fund.FundShortName = AMCFund.FundShortName;

                    FundList.Add(Fund);
                }
            }
            return FundList;
        }
        public async Task<FundNAVDTO> GetFundNAVAsync(int FundID)
        {
            FundNAVDTO fundNAVDTO = new FundNAVDTO();

            decimal FundNAV = await _fundDBService.GetFundNAV(FundID);

            fundNAVDTO.navPerUnit = FundNAV.ToString();

            return fundNAVDTO;
        }
    }
}
