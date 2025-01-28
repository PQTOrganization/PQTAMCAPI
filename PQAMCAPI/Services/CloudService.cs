using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;
using System.Data;
using AutoMapper;
using PQAMCClasses.CloudDTOs;
using Newtonsoft.Json;
using Classes;
using Microsoft.Extensions.Options;
using PQAMCAPI.Services.TypedClients;
using PQAMCClasses.DTOs;
using System.Text.RegularExpressions;
using PQAMCClasses;
using API.Classes;

namespace PQAMCAPI.Services
{
    public class CloudService : ICloudService
    {
        private readonly IMapper _mapper;
        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;
        private readonly IGenderService _genderService;
        private readonly IOccupationService _occupationService;
        private readonly IEducationService _educationService;
        private readonly IIncomeSourceService _incomeSourceService;
        private readonly IAccountCategoryService _accountCategoryService;
        private readonly IAnnualIncomeService _annualIncomeService;
        private readonly IUserService _userService;
        private readonly IResidentialStatusService _residentialStatusService;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IITMindsRequestDBService _dbService;
        private readonly IITMindsTokenDBService _tokenDBService;
        private HttpClient _client;
        private HttpClientHelper clientHelper;
        private string APIURL = "";
        private ILogger<CloudService> _logger;
        private readonly ITMindsSettings _settings;
        private readonly IITMindsClient _itMindsClient;

        public CloudService(IMapper mapper, ICountryService countryService, ICityService cityService, IGenderService genderService,
            IOccupationService occupationService, IEducationService educationService, IUserService userService, IIncomeSourceService incomeSourceService,
            IAccountCategoryService accountCategoryService, IAnnualIncomeService annualIncomeService,
            IResidentialStatusService residentialStatusService, IHttpClientFactory httpClientFactory, IITMindsRequestDBService dbService,
            IITMindsTokenDBService tokenDBService, ILogger<CloudService> logger, IOptions<ITMindsSettings> settings, IITMindsClient itMindsClient)
        {
            _mapper = mapper;
            _clientFactory = httpClientFactory;
            _client = _clientFactory.CreateClient();

            _cityService = cityService;
            _countryService = countryService;
            _genderService = genderService;
            _occupationService = occupationService;
            _educationService = educationService;
            _userService = userService;
            _incomeSourceService = incomeSourceService;
            _accountCategoryService = accountCategoryService;
            _annualIncomeService = annualIncomeService;
            _residentialStatusService = residentialStatusService;
            _dbService = dbService;
            _tokenDBService = tokenDBService;
            clientHelper = new HttpClientHelper();
            _settings = settings.Value;
            _itMindsClient = itMindsClient;

            _logger = logger;
        }


        private async Task<ITMindsRequest> LogITMindsTxn(string RequestName, object? Request, object? Response, string? Error, int? DBRequestID)
        {
            ITMindsRequest DBRequest = new ITMindsRequest();

            DBRequest.RequestType = RequestName;
            DBRequest.RequestDateTime = DateTime.Now;
            DBRequest.ParentID = DBRequestID;

            DBRequest.Request = JsonConvert.SerializeObject(Request);

            if (Response != null)
            {
                DBRequest.Response = JsonConvert.SerializeObject(Response);
            }
            else
            {
                DBRequest.Response = Error;
            }

            return await _dbService.InsertITMindsRequest(DBRequest);
        }

        private async Task<GetTokenResponseDTO> GetToken()
        {
            string RequestName = "GetToken";
            DateTime RequestDate = DateTime.Now;

            GetTokenResponseDTO tokenResponse;
            try
            {
                string Credentials = await _tokenDBService.GetITMindsCredentials(_settings.KeyFor);
                tokenResponse = await _itMindsClient.GetToken(Credentials);
            }
            catch (Exception ex)
            {
                await LogITMindsTxn(RequestName, null, null, ex.Message, null);
                throw ex;
            }

            ITMindsRequest DBRequest = await LogITMindsTxn(RequestName, null, tokenResponse, null, null);
            if (tokenResponse.ResponseCode != "0")
            {
                throw new MyAPIException("General Error. Please try after some time.");
            }

            ITMindsToken dbToken = new ITMindsToken() { DateTime = RequestDate, Token = tokenResponse.Token };
            await _tokenDBService.InsertITMindsToken(dbToken);
            tokenResponse.DBRequestID = DBRequest.Id;

            return tokenResponse;
        }

        private async Task<GetTokenResponseDTO> RefreshToken()
        {
            DateTime RequestDate = DateTime.Now;


            ITMindsRequest DBRequest = new ITMindsRequest();
            DBRequest.Request = string.Empty;
            DBRequest.RequestType = "RefreshToken";
            DBRequest.RequestDateTime = RequestDate;
            GetTokenResponseDTO tokenResponse;
            try
            {
                tokenResponse = await _itMindsClient.RefreshToken();
            }
            catch (Exception ex)
            {
                DBRequest.Response = ex.Message;
                await _dbService.InsertITMindsRequest(DBRequest);
                throw ex;
            }

            DBRequest.Response = JsonConvert.SerializeObject(tokenResponse);
            DBRequest = await _dbService.InsertITMindsRequest(DBRequest);
            ITMindsToken dbToken = new ITMindsToken() { DateTime = RequestDate, Token = tokenResponse.Token };
            await _tokenDBService.InsertITMindsToken(dbToken);
            tokenResponse.DBRequestID = DBRequest.Id;

            if (tokenResponse.ResponseCode != "0")
            {
                throw new MyAPIException(tokenResponse.ResponseMessage);
            }

            return tokenResponse;
        }

        private async Task<SubmitSaleRequestDTO> MapToSaleRequest(UserApplication Application, User User, InvestmentRequestDTO InitialInvestment,
            FundDTO InvestmentFund, AccountCategory AccountCategory, List<UserBank> UserBanks)
        {
            SubmitSaleRequestDTO Request = _mapper.Map<SubmitSaleRequestDTO>(Application);
            Request.folioType = AccountCategory.FolioType;
            Request.mobCountryCode = User.MobileNumber?.Substring(0, 2);
            Request.mobCityCode = "0" + User.MobileNumber?.Substring(2, 3);
            Request.mobileNumber = User.MobileNumber?.Substring(5);

            if (Request.address1.Length > 120)
            {
                Request.address1 = Request.address1?.Substring(0, 120);
            }
            if (Request.address2.Length > 120)
            {
                Request.address2 = Request.address2?.Substring(0, 120);
            }
            if (Request.address2.Length == 0)
            {
                Request.address2 = null;
            }
            if (Request.mailingAddress1.Length > 120)
            {
                Request.mailingAddress1 = Request.mailingAddress1?.Substring(0, 120);
            }
            if (Request.mailingAddress2.Length > 120)
            {
                Request.mailingAddress2 = Request.mailingAddress2?.Substring(0, 120);
            }

            if (Application.CityOfResidenceId != null)
            {
                Request.city = (await _cityService.FindAsync((int)Application.CityOfResidenceId)).Name;
                Request.mailingCity = Request.city;
            }
            if (Application.CountryOfBirthId != null)
            {
                Request.country = (await _countryService.FindAsync((int)Application.CountryOfBirthId)).Name;
                Request.mailingCountry = Request.country;
            }
            if (Application.CityOfBirthId != null)
            {
                Request.placeOfBirth = (await _cityService.FindAsync((int)Application.CityOfBirthId)).Name;
            }
            if (Application.GenderId != null)
            {
                Request.gender = (await _genderService.FindAsync((int)Application.GenderId)).Name;
            }
            if (Application.OccupationId != null)
            {
                Request.occupation = (await _occupationService.FindAsync((int)Application.OccupationId)).ITMindsName;
            }
            if (Application.SourceOfIncome != null)
            {
                Request.sourceOfIncome = (await _incomeSourceService.FindAsync((int)Application.SourceOfIncome[0])).ITMindsName;
            }

            Request.grossSaleAmount = InitialInvestment.InvestmentAmount.ToString();
            Request.instrumentNumber = "000000"; //InitialInvestment.OnlinePaymentReference;
            Request.instrumentType = Enum.GetName(typeof(Globals.PaymentModes), InitialInvestment.PaymentMode);
            Request.investmentProofUpload = InitialInvestment.ProofOfPayment?.Substring(InitialInvestment.ProofOfPayment.IndexOf(",") + 1);
            Request.planFundId = InvestmentFund.ITMindsFundID;

            Request.nomineeName = null;
            Request.nomineePhnNumber = null;
            Request.nameOfEmployer = null;
            Request.iban = UserBanks.Where(x => x.IsOBAccount).First()?.IBANNumber;
            Request.isRealized = "N";

            GetFundBankAccountsRequestDTO FundBankAccountsRequestDTO = new GetFundBankAccountsRequestDTO();
            FundBankAccountsRequestDTO.accountType = AccountCategory.FolioType;
            FundBankAccountsRequestDTO.fundPlanId = InvestmentFund.ITMindsFundID;
            List<FundBankAccountDTO> FundBankAccounts = await GetFundBankAccounts(FundBankAccountsRequestDTO);
            FundBankAccountDTO SelectedFundAccount = FundBankAccounts.First();

            List<BankNameDTO> BankList = await GetBanksList();
            Request.bankId = BankList.Where(x => x.BankName == SelectedFundAccount.BankName).First().BankId;
            Request.branchId = SelectedFundAccount.BranchId;
            Request.accountNumber = SelectedFundAccount.AccountNumber;

            Request.relationWithMinor = null;
            Request.dividendPayout = null;

            return Request;
        }


        private async Task<SubmitSaleResponseDTO> PostData(UserApplication Application, User User, InvestmentRequestDTO InitialInvestment,
            List<UserBank> UserBanks, FundDTO InvestementFund, AccountCategory AccountCategory, GetTokenResponseDTO TokenDetails)
        {
            string RequestName = "SubmitSahulatWithSale";

            SubmitSaleRequestDTO SaleRequest = await MapToSaleRequest(Application, User, InitialInvestment, InvestementFund, AccountCategory, UserBanks);
            SubmitSaleResponseDTO SaleResponse;

            try
            {
                SaleResponse = await _itMindsClient.SubmitSale(TokenDetails, SaleRequest);
            }
            catch (Exception ex)
            {
                await LogITMindsTxn(RequestName, SaleRequest, null, ex.Message, TokenDetails.DBRequestID);
                throw;
            }

            await LogITMindsTxn(RequestName, SaleRequest, SaleResponse, null, TokenDetails.DBRequestID);

            if (SaleResponse.ResponseCode != "0")
            {
                throw new MyAPIException(SaleResponse.ResponseMessage);
            }
            return SaleResponse;
        }
        private SubmitDigitalAccountRequestDTO PutDummyValues(UserApplication Application, SubmitDigitalAccountRequestDTO DigitalAccount)
        {
            DigitalAccount.folioType = "CIS";
            DigitalAccount.nomineeName = "Nominee";
            DigitalAccount.nomineePhnNumber = "1234";
            DigitalAccount.minor = "N";
            DigitalAccount.cnicIssueDate = "01/01/2000";
            DigitalAccount.guardianName = DigitalAccount.fatherName;
            DigitalAccount.guardianCnicAllowNull = "Y";
            DigitalAccount.guardianCnic = "222";
            DigitalAccount.guardianCnicIssueDate = "01/01/2000";
            DigitalAccount.guardianCnicExpiryDate = "01/01/2030";
            DigitalAccount.numOfDependents = "0";
            DigitalAccount.address2 = DigitalAccount.address1;
            DigitalAccount.mailingAddress2 = DigitalAccount.mailingAddress1;
            DigitalAccount.mailingCountry = DigitalAccount.country;
            DigitalAccount.mailingCity = DigitalAccount.city;
            DigitalAccount.mobCountryCode = "92";
            DigitalAccount.phnNumber = "2222";
            DigitalAccount.phnCityCode = "348";
            DigitalAccount.phnCountryCode = "92";
            DigitalAccount.mobCityCode = "348";
            DigitalAccount.planFundId = "2";
            DigitalAccount.equitySubFund = "Y";
            DigitalAccount.nameOfEmployer = "ad";
            DigitalAccount.ultimateBeneficiaryName = "Test";
            DigitalAccount.ultimateBeneficiaryCNIC = "2222";
            DigitalAccount.mmSubFund = "33";
            DigitalAccount.debtSubFund = "33";
            DigitalAccount.iban = "PK49ALFH0031001007532231";
            DigitalAccount.salutation = "Mr.";
            DigitalAccount.maritalStatus = "Single";
            DigitalAccount.email = "test@email.com";
            DigitalAccount.seq = "1";
            DigitalAccount.relationWithMinor = "ad";
            DigitalAccount.occupation = "test";
            DigitalAccount.education = "matric";
            DigitalAccount.annualIncome = "1000";
            DigitalAccount.sourceOfIncome = "Tester";
            DigitalAccount.dividendPayout = "[]";
            DigitalAccount.reserved1 = "h";
            DigitalAccount.reserved2 = "w";
            DigitalAccount.reserved3 = "3";
            DigitalAccount.reserved4 = "4";
            DigitalAccount.reserved5 = "5";
            DigitalAccount.vpsFundId = "222";

            DigitalAccount.mobileNumber = "8777887";
            return DigitalAccount;
        }

        private async Task<SubmitDigitalAccountRequestDTO> MapToDigitalAccountRequest(UserApplication Application, List<UserBank> UserBanks)
        {
            SubmitDigitalAccountRequestDTO DigitalAccountRequest = _mapper.Map<SubmitDigitalAccountRequestDTO>(Application);

            if (Application.AccountCategoryId != null)
            {
                //DigitalAccountRequest.folioType = (await _accountCategoryService.FindAsync((int)Application.AccountCategoryId)).Name;
                DigitalAccountRequest.folioType = "CIS";
            }
            if (Application.CityOfResidenceId != null)
            {
                DigitalAccountRequest.city = (await _cityService.FindAsync((int)Application.CityOfResidenceId)).Name;
            }
            if (Application.CountryOfResidenceId != null)
            {
                DigitalAccountRequest.country = (await _countryService.FindAsync((int)Application.CountryOfResidenceId)).Name;
            }
            if (Application.GenderId != null)
            {
                DigitalAccountRequest.gender = (await _genderService.FindAsync((int)Application.GenderId)).Name;
            }
            if (Application.OccupationId != null)
            {
                DigitalAccountRequest.occupation = (await _occupationService.FindAsync((int)Application.OccupationId)).Name;
            }
            if (Application.EducationId != null)
            {
                DigitalAccountRequest.education = (await _educationService.FindAsync((int)Application.EducationId)).Name;
            }
            if (Application.AnnualIncomeId != null)
            {
                string AnnualIncomeText = (await _annualIncomeService.FindAsync((int)Application.AnnualIncomeId)).Name;

                string[] Split = AnnualIncomeText.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                int AnnualIncomeNumeric = 0;
                foreach (string s in Split)
                {
                    if (Regex.IsMatch(s, @"\d+"))
                    {
                        AnnualIncomeNumeric = Int32.Parse(s.Replace(",", ""));
                        break;
                    }
                }

                DigitalAccountRequest.annualIncome = AnnualIncomeNumeric.ToString();
            }
            if (Application.UserId != 0)
            {
                User user = await _userService.FindAsync(Application.UserId);
                DigitalAccountRequest.mobileNumber = user.MobileNumber;
                DigitalAccountRequest.email = user.Email;
            }
            if (Application.SourceOfIncome != null)
            {
                DigitalAccountRequest.sourceOfIncome = (await _incomeSourceService.FindAsync((int)Application.SourceOfIncome[0])).Name;
            }
            if (Application.ResidentialStatusId != null)
            {
                DigitalAccountRequest.residentialStatus = (await _residentialStatusService.FindAsync((int)Application.ResidentialStatusId)).Name;
            }
            DigitalAccountRequest.iban = UserBanks.Where(x => x.IsOBAccount).First()?.IBANNumber;
            //DigitalAccountRequest = PutDummyValues(Application, DigitalAccountRequest);


            return DigitalAccountRequest;
        }

        private SubmitRedemptionRequestDTO PutDummyRedemptionRequest(SubmitRedemptionRequestDTO Request)
        {
            Request.schemeOption = "F";
            Request.allUnits = "N";
            Request.redemptionInTermsOf = "A";
            Request.paymentMode = "Electronic";

            return Request;
        }

        public async Task<List<FolioBankDTO>> GetFolioBankListForUser(User User)
        {
            List<FolioBankDTO> FolioBankList = new List<FolioBankDTO>();

            FolioDTO UserFolio = (await GetFolioList()).FirstOrDefault(x => x.FolioNumber == Globals.GetFolioNumber(User.FolioNumber));
            if (UserFolio != null)
                FolioBankList = await GetFolioBankList(UserFolio.UnitHolderId);

            return FolioBankList;
        }

        private async Task<SubmitRedemptionRequestDTO> MapToRedemptionRequest(RedemptionRequestDTO RedemptionRequest, UserApplicationDTO Application,
           User User, FundDTO Fund)
        {
            SubmitRedemptionRequestDTO SubmitRedemptionRequestDTO = _mapper.Map<SubmitRedemptionRequestDTO>(RedemptionRequest);

            SubmitRedemptionRequestDTO.bankAccountNo = RedemptionRequest.BankAccountNo;
            SubmitRedemptionRequestDTO.bankId = RedemptionRequest.BankID;

            SubmitRedemptionRequestDTO.formReceivingDateTime = RedemptionRequest.RequestDate.ToString(Globals.Constants.ITMindsDateTimeFormat);
            SubmitRedemptionRequestDTO.planFundId = Fund.ITMindsFundID;

            if (Application.AccountCategoryId != null)
            {
                SubmitRedemptionRequestDTO.folioType = (await _accountCategoryService.FindAsync((int)Application.AccountCategoryId)).Name;
            }

            SubmitRedemptionRequestDTO = PutDummyRedemptionRequest(SubmitRedemptionRequestDTO);

            return SubmitRedemptionRequestDTO;
        }
        private SubmitConversionRequestDTO MapToConversionRequest(FundTransferRequestDTO FundTransferRequest, FundDTO FromFund, FundDTO ToFund)
        {
            SubmitConversionRequestDTO SubmitConversionRequest = _mapper.Map<SubmitConversionRequestDTO>(FundTransferRequest);
            SubmitConversionRequest.fromPlanFundId = FromFund.ITMindsFundID;
            SubmitConversionRequest.toPlanFundId = ToFund.ITMindsFundID;
            SubmitConversionRequest.schemeOption = "F";
            SubmitConversionRequest.conversionInTermsOf = "A";
            //SubmitConversionRequest.conversionTermValue = "10";

            return SubmitConversionRequest;
        }

        private async Task<SubmitSubSaleRequestDTO> MapToSubSaleRequest(InvestmentRequestDTO investmentRequest, UserApplicationDTO Application, FundDTO Fund, AccountCategory AccountCategory)
        {
            SubmitSubSaleRequestDTO SubmitSubSaleRequest = _mapper.Map<SubmitSubSaleRequestDTO>(investmentRequest);

            if (Fund != null)
            {
                SubmitSubSaleRequest.planFundId = Fund.ITMindsFundID;
            }

            if (Application.AccountCategoryId != null)
            {
                SubmitSubSaleRequest.folioType = AccountCategory.FolioType;
            }

            GetFundBankAccountsRequestDTO FundBankAccountsRequestDTO = new GetFundBankAccountsRequestDTO();
            FundBankAccountsRequestDTO.accountType = AccountCategory.FolioType;
            FundBankAccountsRequestDTO.fundPlanId = Fund.ITMindsFundID;
            //List<FundBankAccountDTO> FundBankAccounts = await GetFundBankAccounts(FundBankAccountsRequestDTO);
            //FundBankAccountDTO SelectedFundAccount = FundBankAccounts.First();

            //List<BankNameDTO> BankList = await GetBanksList();
            //SubmitSubSaleRequest.bankId = BankList.Where(x => x.BankName == SelectedFundAccount.BankName).First().BankId;
            //SubmitSubSaleRequest.branchId = SelectedFundAccount.BranchId;
            //SubmitSubSaleRequest.accountNumber = SelectedFundAccount.AccountNumber;

            return SubmitSubSaleRequest;
        }
        private void GenerateReport(GetAccountStatementReportResponseDTO Report)
        {
            byte[] bArray = Convert.FromBase64String(Report.ReportData);
            string ReportName = "acctstmt" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";

            System.IO.FileStream stream =
            new FileStream("Reports\\" + ReportName, FileMode.CreateNew);
            System.IO.BinaryWriter writer =
                new BinaryWriter(stream);
            writer.Write(bArray, 0, bArray.Length);
            writer.Close();
        }

        public async Task<SubmitSaleResponseDTO> PostUserApplicationData(UserApplication Application, User User, InvestmentRequestDTO InitialInvestment,
            List<UserBank> UserBanks, FundDTO InvestmentFund, AccountCategory AccountCategory)
        {
            GetTokenResponseDTO TokenResponse = await GetToken();

            return await PostData(Application, User, InitialInvestment, UserBanks, InvestmentFund, AccountCategory, TokenResponse);
        }

        public async Task<List<FolioDTO>> GetFolioList()
        {
            GetTokenResponseDTO TokenResponse = await GetToken();
            string RequestName = "GetFolioList";

            GetFolioListResponseDTO FolioListResponse;
            try
            {
                FolioListResponse = await _itMindsClient.GetFolioList(TokenResponse);
            }
            catch (Exception ex)
            {
                await LogITMindsTxn(RequestName, null, null, ex.Message, TokenResponse.DBRequestID);
                throw;
            }

            await LogITMindsTxn(RequestName, null, FolioListResponse, null, TokenResponse.DBRequestID);

            if (FolioListResponse.ResponseCode != "0")
            {
                throw new MyAPIException(FolioListResponse.ResponseMessage);
            }

            return FolioListResponse.FolioList;
        }

        public async Task<List<AMCFundNAVDTO>> GetAMCFundNAVList()
        {
            GetTokenResponseDTO TokenResponse = await GetToken();
            string RequestName = "GetAMCFundNAVList";

            GetAMCFundNAVListResponseDTO FundNAVList;
            try
            {
                FundNAVList = await _itMindsClient.GetAMCFundNAVList(TokenResponse);
            }
            catch (Exception ex)
            {
                await LogITMindsTxn(RequestName, null, null, ex.Message, TokenResponse.DBRequestID);
                throw;
            }

            await LogITMindsTxn(RequestName, null, FundNAVList, null, TokenResponse.DBRequestID);

            if (FundNAVList.ResponseCode != "0")
            {
                throw new MyAPIException(FundNAVList.ResponseMessage);
            }

            return FundNAVList.AMCFundNavList;
        }

        public async Task<List<InvestorAccountTransactionDTO>> GetAccountStatementCIS(GetAccountStatementCISRequestDTO ActStmtReq)
        {
            string RequestName = "GetAccountStatementCIS";
            GetTokenResponseDTO TokenResponse = await GetToken();
            GetAccountStatementCISResponseDTO ActStmt;
            try
            {
                ActStmt = await _itMindsClient.GetAccountStatementCIS(TokenResponse, ActStmtReq);
            }
            catch (Exception ex)
            {
                await LogITMindsTxn(RequestName, ActStmtReq, null, ex.Message, TokenResponse.DBRequestID);
                throw;
            }

            await LogITMindsTxn(RequestName, ActStmtReq, ActStmt, null, TokenResponse.DBRequestID);

            if (ActStmt.ResponseCode != "0")
            {
                throw new MyAPIException(ActStmt.ResponseMessage);
            }

            return ActStmt.InvestorAccountTransaction;
        }

        public async Task<List<BankNameDTO>> GetBanksList()
        {
            GetTokenResponseDTO TokenResponse = await GetToken();
            string RequestName = "GetBanksList";

            GetBankNamesResponseDTO BankNamesResponse;
            try
            {
                BankNamesResponse = await _itMindsClient.GetBanksList(TokenResponse);
            }
            catch (Exception ex)
            {
                await LogITMindsTxn(RequestName, null, null, ex.Message, TokenResponse.DBRequestID);
                throw;
            }

            await LogITMindsTxn(RequestName, null, BankNamesResponse, null, TokenResponse.DBRequestID);

            if (BankNamesResponse.ResponseCode != "0")
            {
                throw new MyAPIException(BankNamesResponse.ResponseMessage);
            }

            return BankNamesResponse.BankNames;
        }

        public async Task<AccountStatementReportDTO> GetAccountStatementReport(GetAccountStatementReportRequestDTO ActStmtReportReq)
        {
            GetTokenResponseDTO TokenResponse = await GetToken();
            string RequestName = "GetAccountStatementReport";

            GetAccountStatementReportResponseDTO AccountStatementReportResponse;
            try
            {
                AccountStatementReportResponse = await _itMindsClient.GetAccountStatementReport(TokenResponse, ActStmtReportReq);
            }
            catch (Exception ex)
            {
                await LogITMindsTxn(RequestName, ActStmtReportReq, null, ex.Message, TokenResponse.DBRequestID);
                throw;
            }

            await LogITMindsTxn(RequestName, ActStmtReportReq, AccountStatementReportResponse, null, TokenResponse.DBRequestID);

            if (AccountStatementReportResponse.ResponseCode != "0")
            {
                throw new MyAPIException(AccountStatementReportResponse.ResponseMessage);
            }

            return _mapper.Map<AccountStatementReportDTO>(AccountStatementReportResponse);
        }
        public async Task<AccountStatementReportDTO> GetAccountStatementCISVPSReport(GetAccountStatementCISVPSReportRequestDTO ActStmtReportReq)
        {
            GetTokenResponseDTO TokenResponse = await GetToken();
            string RequestName = "getAccountStatementCISVPSReport";

            GetAccountStatementReportResponseDTO AccountStatementReportResponse;
            try
            {
                AccountStatementReportResponse = await _itMindsClient.GetAccountStatementCISVPSReport(TokenResponse, ActStmtReportReq);
            }
            catch (Exception ex)
            {
                await LogITMindsTxn(RequestName, ActStmtReportReq, null, ex.Message, TokenResponse.DBRequestID);
                throw;
            }

            await LogITMindsTxn(RequestName, ActStmtReportReq, AccountStatementReportResponse, null, TokenResponse.DBRequestID);

            if (AccountStatementReportResponse.ResponseCode != "0")
            {
                throw new MyAPIException(AccountStatementReportResponse.ResponseMessage);
            }

            return _mapper.Map<AccountStatementReportDTO>(AccountStatementReportResponse);
        }
        public async Task<List<InvestorAccountTransactionDTO>> GetAccountStatementVPS(GetAccountStatementVPSRequestDTO ActStmtReq)
        {
            GetTokenResponseDTO TokenResponse = await GetToken();
            string RequestName = "GetAccountStatementVPS";

            GetAccountStatementCISResponseDTO ActStmt;
            try
            {
                ActStmt = await _itMindsClient.GetAccountStatementVPS(TokenResponse, ActStmtReq);
            }
            catch (Exception ex)
            {
                await LogITMindsTxn(RequestName, ActStmtReq, null, ex.Message, TokenResponse.DBRequestID);
                throw;
            }

            await LogITMindsTxn(RequestName, ActStmtReq, ActStmt, null, TokenResponse.DBRequestID);

            if (ActStmt.ResponseCode != "0")
            {
                throw new MyAPIException(ActStmt.ResponseMessage);
            }

            return ActStmt.InvestorAccountTransaction;
        }

        public async Task<SubmitDigitalAccountResponseDTO> SubmitDigitalAccount(UserApplication Application, List<UserBank> UserBanks)
        {
            GetTokenResponseDTO TokenResponse = await GetToken();
            string RequestName = "SubmitDigitalAccount";

            SubmitDigitalAccountRequestDTO DigitalAccountRequest = await MapToDigitalAccountRequest(Application, UserBanks);

            SubmitDigitalAccountResponseDTO DigitalAccountResponse;
            try
            {
                DigitalAccountResponse = await _itMindsClient.SubmitDigitalAccountRequest(TokenResponse, DigitalAccountRequest);
            }
            catch (Exception ex)
            {
                await LogITMindsTxn(RequestName, DigitalAccountRequest, null, ex.Message, TokenResponse.DBRequestID);
                throw;
            }

            await LogITMindsTxn(RequestName, DigitalAccountRequest, DigitalAccountResponse, null, TokenResponse.DBRequestID);

            if (DigitalAccountResponse.ResponseCode != "0")
            {
                throw new MyAPIException(DigitalAccountResponse.ResponseMessage);
            }

            return DigitalAccountResponse;
        }

        public async Task<SubmitResponseDTO> SubmitRedemption(RedemptionRequestDTO RedemptionRequest, UserApplicationDTO Application,
             FundDTO Fund, User User)
        {
            GetTokenResponseDTO TokenResponse = await GetToken();
            string RequestName = "SubmitRedemption";

            SubmitRedemptionRequestDTO SubmitRedemptionRequest = await MapToRedemptionRequest(RedemptionRequest, Application, User, Fund);

            SubmitResponseDTO SubmitResponse;
            try
            {
                SubmitResponse = await _itMindsClient.SubmitRedemptionRequest(TokenResponse, SubmitRedemptionRequest);
            }
            catch (Exception ex)
            {
                await LogITMindsTxn(RequestName, SubmitRedemptionRequest, null, ex.Message, TokenResponse.DBRequestID);
                throw;
            }

            await LogITMindsTxn(RequestName, SubmitRedemptionRequest, SubmitResponse, null, TokenResponse.DBRequestID);

            if (SubmitResponse.ResponseCode != "0")
            {
                throw new MyAPIException(SubmitResponse.ResponseMessage);
            }

            return SubmitResponse;
        }

        public async Task<SubmitResponseDTO> SubmitConversion(FundTransferRequestDTO FundTransferRequest, FundDTO FromFund, FundDTO ToFund)
        {
            GetTokenResponseDTO TokenResponse = await GetToken();
            string RequestName = "SubmitConversion";

            SubmitConversionRequestDTO SubmitConversionRequest = MapToConversionRequest(FundTransferRequest, FromFund, ToFund);


            GetPlanListRequestDTO PlanListRequest = new GetPlanListRequestDTO();
            PlanListRequest.folioNumber = SubmitConversionRequest.folioNo;
            PlanListRequest.schemeOption = "F";
            PlanListRequest.transactionType = "RP";

            List<PlanDTO> PlanList = await GetPlanList(PlanListRequest);
            PlanDTO FromPlan = PlanList.Where(x => x.PlanId == SubmitConversionRequest.fromPlanFundId).FirstOrDefault();

            if (FromPlan != null)
            {
                SubmitConversionRequest.fromUnitTypeClass = FromPlan.UnitTypeClass;
            }

            SubmitResponseDTO SubmitResponse;
            try
            {
                SubmitResponse = await _itMindsClient.SubmitConversionRequest(TokenResponse, SubmitConversionRequest);
            }
            catch (Exception ex)
            {
                await LogITMindsTxn(RequestName, SubmitConversionRequest, null, ex.Message, TokenResponse.DBRequestID);
                throw;
            }

            await LogITMindsTxn(RequestName, SubmitConversionRequest, SubmitResponse, null, TokenResponse.DBRequestID);

            if (SubmitResponse.ResponseCode != "0")
            {
                throw new MyAPIException(SubmitResponse.ResponseMessage);
            }

            return SubmitResponse;
        }

        public async Task<SubmitResponseDTO> SubmitSubsale(InvestmentRequestDTO InvestmentRequest, UserApplicationDTO Application, FundDTO Fund, AccountCategory AccountCategory)
        {
            GetTokenResponseDTO TokenResponse = await GetToken();
            string RequestName = "SubmitSubsale";

            SubmitSubSaleRequestDTO SubmitSubSaleRequest = await MapToSubSaleRequest(InvestmentRequest, Application, Fund, AccountCategory);

            SubmitResponseDTO SubmitResponse;
            try
            {
                SubmitResponse = await _itMindsClient.SubmitSubsaleRequest(TokenResponse, SubmitSubSaleRequest);
            }
            catch (Exception ex)
            {
                await LogITMindsTxn(RequestName, SubmitSubSaleRequest, null, ex.Message, TokenResponse.DBRequestID);
                throw;
            }

            await LogITMindsTxn(RequestName, SubmitSubSaleRequest, SubmitResponse, null, TokenResponse.DBRequestID);

            if (SubmitResponse.ResponseCode != "0")
            {
                throw new MyAPIException(SubmitResponse.ResponseMessage);
            }

            return SubmitResponse;
        }

        public async Task<List<FundBankAccountDTO>> GetFundBankAccounts(GetFundBankAccountsRequestDTO FundBankAccountsRequest)
        {
            GetTokenResponseDTO TokenResponse = await GetToken();
            string RequestName = "GetFundBankAccounts";

            GetFundBankAccountsResponseDTO FundBankAccountsResponse;
            try
            {
                FundBankAccountsResponse = await _itMindsClient.GetFundBankAccounts(TokenResponse, FundBankAccountsRequest);
            }
            catch (Exception ex)
            {
                await LogITMindsTxn(RequestName, FundBankAccountsRequest, null, ex.Message, TokenResponse.DBRequestID);
                throw;
            }

            await LogITMindsTxn(RequestName, FundBankAccountsRequest, FundBankAccountsResponse, null, TokenResponse.DBRequestID);

            //if(FundBankAccountsResponse.ResponseCode == "5")
            //{
            //    throw new MyAPIException(Globals.ErrorMessages.NoBanksFound);
            //}
            //if (FundBankAccountsResponse.ResponseCode != "0")
            //{
            //    throw new MyAPIException(FundBankAccountsResponse.ResponseMessage);
            //}

            return FundBankAccountsResponse.FundBankAccounts;
        }

        public async Task<List<PlanDTO>> GetPlanList(GetPlanListRequestDTO PlanListRequest)
        {
            GetTokenResponseDTO TokenResponse = await GetToken();
            string RequestName = "GetPlanList";

            GetPlanListResponseDTO PlanListResponse;
            try
            {
                PlanListResponse = await _itMindsClient.GetPlanList(TokenResponse, PlanListRequest);
            }
            catch (Exception ex)
            {
                await LogITMindsTxn(RequestName, PlanListRequest, null, ex.Message, TokenResponse.DBRequestID);
                throw;
            }

            await LogITMindsTxn(RequestName, PlanListRequest, PlanListResponse, null, TokenResponse.DBRequestID);

            if (PlanListResponse.ResponseCode != "0")
            {
                throw new MyAPIException(PlanListResponse.ResponseMessage);
            }

            return PlanListResponse.PlanList;
        }
        public async Task<List<CISFundDTO>> GetCISFundList(GetCISFundListRequestDTO CISFundListRequest)
        {
            GetTokenResponseDTO TokenResponse = await GetToken();
            string RequestName = "GetPlanList";

            GetCISFundListResponseDTO FundListResponse;
            try
            {
                FundListResponse = await _itMindsClient.GetCISFundList(TokenResponse, CISFundListRequest);
            }
            catch (Exception ex)
            {
                await LogITMindsTxn(RequestName, CISFundListRequest, null, ex.Message, TokenResponse.DBRequestID);
                throw;
            }

            await LogITMindsTxn(RequestName, CISFundListRequest, FundListResponse, null, TokenResponse.DBRequestID);

            if (FundListResponse.ResponseCode != "0")
            {
                throw new MyAPIException(FundListResponse.ResponseMessage);
            }

            return FundListResponse.FundList;
        }
        public async Task<List<int>> GetVPSPlanSequence()
        {
            GetTokenResponseDTO TokenResponse = await GetToken();
            string RequestName = "GetVPSPlanSequence";

            GetVPSPlanSequenceResponseDTO VPSPlanSequenceResponse;
            try
            {
                VPSPlanSequenceResponse = await _itMindsClient.GetVPSPlanSequence(TokenResponse);
            }
            catch (Exception ex)
            {
                await LogITMindsTxn(RequestName, null, null, ex.Message, TokenResponse.DBRequestID);
                throw;
            }

            await LogITMindsTxn(RequestName, null, VPSPlanSequenceResponse, null, TokenResponse.DBRequestID);

            if (VPSPlanSequenceResponse.ResponseCode != "0")
            {
                throw new MyAPIException(VPSPlanSequenceResponse.ResponseMessage);
            }

            return VPSPlanSequenceResponse.VPSPlanSeqeunce;
        }

        public async Task<FundNAVDTO> GetFundNAV(GetFundNAVRequestDTO FundNAVRequestDTO)
        {
            GetTokenResponseDTO TokenResponse = await GetToken();
            string RequestName = "GetFundNAV";

            GetFundNAVResponseDTO FundNAVResponse;
            try
            {
                FundNAVResponse = await _itMindsClient.getFundNAV(TokenResponse, FundNAVRequestDTO);
            }
            catch (Exception ex)
            {
                await LogITMindsTxn(RequestName, FundNAVRequestDTO, null, ex.Message, TokenResponse.DBRequestID);
                throw;
            }

            await LogITMindsTxn(RequestName, FundNAVRequestDTO, FundNAVResponse, null, TokenResponse.DBRequestID);

            if (FundNAVResponse.ResponseCode == "5")
            {
                throw new MyAPIException(Globals.ErrorMessages.NAVNotAvailable);
            }
            if (FundNAVResponse.ResponseCode != "0")
            {
                throw new MyAPIException(FundNAVResponse.ResponseMessage);
            }

            return FundNAVResponse.getFundNAV[0];
        }

        public async Task<FundNAVDTO> GetFundNAV(string FundShortName)
        {
            GetFundNAVRequestDTO request = new GetFundNAVRequestDTO();
            request.fundShortName = FundShortName;
            request.applicableNavDate = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");

            return await GetFundNAV(request);
        }
        public async Task<List<FolioBankDTO>> GetFolioBankList(string UnitHolderID)
        {
            GetTokenResponseDTO TokenResponse = await GetToken();
            string RequestName = "getFolioBankList";

            GetFolioBankListRequestDTO Request = new GetFolioBankListRequestDTO();
            Request.unitHolderId = UnitHolderID;

            GetFolioBankListResponseDTO FolioBankResponseDTO;
            try
            {
                FolioBankResponseDTO = await _itMindsClient.GetFolioBankList(TokenResponse, Request);
            }
            catch (Exception ex)
            {
                await LogITMindsTxn(RequestName, Request, null, ex.Message, TokenResponse.DBRequestID);
                throw;
            }

            await LogITMindsTxn(RequestName, Request, FolioBankResponseDTO, null, TokenResponse.DBRequestID);

            if (FolioBankResponseDTO.ResponseCode != "0")
            {
                throw new MyAPIException(FolioBankResponseDTO.ResponseMessage);
            }

            return FolioBankResponseDTO.BankList;
        }

        public async Task<List<InvestorAccountTransactionDTO>> GetAccountStatementCISFromCloud(string FolioNumber)
        {
            GetAccountStatementCISRequestDTO request = new GetAccountStatementCISRequestDTO();
            request.folioNo = PQAMCClasses.Globals.GetFolioNumber(FolioNumber);
            request.statementType = "T";
            request.fundPlanId = "*ALL";
            request.fromDate = "01/01/2022"; // + DateTime.Now.Year.ToString();
            request.toDate = DateTime.Now.Date.ToString("dd/MM/yyyy");

            return await GetAccountStatementCIS(request);
        }
    }
}
