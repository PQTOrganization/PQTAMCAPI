using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;
using Oracle.ManagedDataAccess.Client;
using Helper;
using System.Configuration;
using System.Data;
using AutoMapper;
using PQAMCClasses.DTOs;
using PQAMCClasses.CloudDTOs;
using Hangfire;
using Services;
using API.Classes;
using PQAMCClasses;

namespace PQAMCAPI.Services.Domain
{
    public class UserApplicationService : IUserApplicationService
    {
        private readonly IMapper _mapper;
        private readonly IUserApplicationDBService _userApplicationDBService;
        private readonly IUserApplicationDocumentDBService _userAppDocumentDBService;
        private readonly IUserBankDBService _userBankDBService;
        private readonly IUserDBService _userDBService;
        private readonly ICloudService _cloudService;
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;
        private readonly IContactOwnerShipService _contactOwnershipService;
        private readonly IOccupationService _occupationService;
        private readonly IIncomeSourceService _incomeSourceService;
        private readonly IBankService _bankService;
        private readonly IGenderService _genderService;
        private readonly IResidentialStatusService _residentialStatusService;
        private readonly IEducationService _educationService;
        private readonly IAnnualIncomeService _annualIncomeService;
        private readonly IInvestmentRequestDBService _investmentRequestDBService;
        private readonly IAccountCategoryService _accountCategoryService;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly IStoreProcedureService _spService;
        private readonly IFundService _fundService;

        const string INVSTREQ_PACKAGE_NAME = "AMC_INVESTMENT_REQUEST_PKG";
        public UserApplicationService(IMapper mapper, IUserApplicationDBService userAppDBService, IUserApplicationDocumentDBService userAppDocumentDBService,
                                      IUserBankDBService userBankDBService, IUserDBService userDBService, ICloudService cloudService, ICountryService countryService,
                                      ICityService cityService, IContactOwnerShipService contactOwnershipService, IOccupationService occupationService,
                                      IIncomeSourceService incomeSourceService, IBankService bankService, IGenderService genderService,
                                      IResidentialStatusService residentialStatusService, IEducationService educationService, IAnnualIncomeService annualIncomeService,
                                      IInvestmentRequestDBService investmentRequestDBService, IEmailSender emailSender, IStoreProcedureService spService,
                                      IFundService fundService, IAccountCategoryService accountCategoryService, ILogger<UserService> logger)
        {
            _mapper = mapper;
            _userApplicationDBService = userAppDBService;
            _userAppDocumentDBService = userAppDocumentDBService;
            _userBankDBService = userBankDBService;
            _userDBService = userDBService;
            _cloudService = cloudService;
            _countryService = countryService;
            _cityService = cityService;
            _contactOwnershipService = contactOwnershipService;
            _occupationService = occupationService;
            _incomeSourceService = incomeSourceService;
            _bankService = bankService;
            _genderService = genderService;
            _residentialStatusService = residentialStatusService;
            _educationService = educationService;
            _annualIncomeService = annualIncomeService;
            _investmentRequestDBService = investmentRequestDBService;
            _accountCategoryService = accountCategoryService;
            _fundService = fundService;
            _spService = spService;
            _emailSender = emailSender;
            _logger = logger;
        }

        public async Task<List<UserApplicationDTO>> GetAllUserApplications()
        {
            var UserApplications = await _userApplicationDBService.GetAllAsync();
            return _mapper.Map<List<UserApplicationDTO>>(UserApplications); ;
        }

        public async Task<UserApplicationDTO> GetUserApplication(int UserApplicationId)
        {
            var UserApplication = await _userApplicationDBService.FindAsync(UserApplicationId);
            var UserAppDTO = _mapper.Map<UserApplicationDTO>(UserApplication);

            var UserBanks = await _userBankDBService.GetAllForUser(UserApplication.UserId);

            if (UserBanks.Count > 0)
            {
                UserAppDTO.UserBankId = UserBanks[0].UserBankId;
                UserAppDTO.BankId = UserBanks[0].BankId;
                UserAppDTO.OneLinkTitle = UserBanks[0].OneLinkTitle;
                UserAppDTO.IBANNumber = UserBanks[0].IBANNumber;
                UserAppDTO.IsIBANVerified = UserBanks[0].IsIBANVerified;
            }

            return UserAppDTO;
        }

        public async Task<UserApplicationDTO> GetUserApplicationDocuments(int UserApplicationId)
        {
            var UserApplication = await _userApplicationDBService.FindAsync(UserApplicationId);
            return _mapper.Map<UserApplicationDTO>(UserApplication);
        }

        public async Task<UserApplicationDTO> GetApplicationForUser(int UserId)
        {
            var UserApplication = await _userApplicationDBService.GetApplicationForUser(UserId);
            return _mapper.Map<UserApplicationDTO>(UserApplication);
        }

        public async Task<UserApplicationDTO> InsertUserApplication(UserApplicationDTO Data)
        {
            var UserApp = _mapper.Map<UserApplication>(Data);
            var NewUserApp = await _userApplicationDBService.InsertUserApplication(UserApp);

            return _mapper.Map<UserApplicationDTO>(NewUserApp);
        }

        public async Task<UserApplicationDTO> UpdateUserApplication(int UserApplicationId,
                                                                    UserApplicationDTO Data)
        {
            var UserApp = _mapper.Map<UserApplication>(Data);
            UserApp = await _userApplicationDBService.UpdateUserApplication(UserApplicationId, UserApp);

            if (UserApp.IsFinalSubmit)
            {
                User User = await _userDBService.FindAsync(Data.UserId);

                UserApplicationEmailDTO UserAppEmailData = _mapper.Map<UserApplicationEmailDTO>(Data);
                if (Data.CountryOfBirthId != null)
                {
                    UserAppEmailData.CountryOfBirth = (await _countryService.FindAsync((int)Data.CountryOfBirthId)).Name;
                }
                if (Data.CountryOfResidenceId != null)
                {
                    UserAppEmailData.CountryOfResidence = (await _countryService.FindAsync((int)Data.CountryOfResidenceId)).Name;
                }
                if (Data.CityOfBirthId != null)
                {
                    UserAppEmailData.CityOfBirth = (await _cityService.FindAsync((int)Data.CityOfBirthId)).Name;
                }
                if (Data.CityOfResidenceId != null)
                {
                    UserAppEmailData.CityOfResidence = (await _cityService.FindAsync((int)Data.CityOfResidenceId)).Name;
                }
                if (Data.ContactOwnershipId != null)
                {
                    UserAppEmailData.ContactOwnershipName = (await _contactOwnershipService.FindAsync((int)Data.ContactOwnershipId)).Name;
                }
                UserAppEmailData.MobileNumber = User.MobileNumber;
                UserAppEmailData.EmailAddress = User.Email;

                if (Data.OccupationId != null)
                {
                    UserAppEmailData.OccupationName = (await _occupationService.FindAsync((int)Data.OccupationId)).Name;
                }
                if (Data.SourceOfIncome.Count > 0)
                {
                    UserAppEmailData.SourceOfIncomeName = (await _incomeSourceService.FindAsync(Data.SourceOfIncome[0])).Name;
                }
                if (Data.BankId != null)
                {
                    UserAppEmailData.BankName = (await _bankService.FindAsync((int)Data.BankId)).Name;
                }
                if (Data.GenderId != null)
                {
                    UserAppEmailData.GenderName = (await _genderService.FindAsync((int)Data.GenderId)).Name;
                }
                if (Data.ResidentialStatusId != null)
                {
                    UserAppEmailData.ResidentialStatusName = (await _residentialStatusService.FindAsync((int)Data.ResidentialStatusId)).Name;
                }
                if (Data.DateOfBirth != null)
                {
                    UserAppEmailData.Age = Globals.GetAgeFromDOB(Data.DateOfBirth);
                }
                if (Data.EducationId != null)
                {
                    UserAppEmailData.EducationName = (await _educationService.FindAsync((int)Data.EducationId)).Name;
                }
                if (Data.AnnualIncomeId != null)
                {
                    UserAppEmailData.AnnualIncomeName = (await _annualIncomeService.FindAsync((int)Data.AnnualIncomeId)).Name;
                }
                if (UserAppEmailData.CountryOfTaxId != null)
                {
                    UserAppEmailData.CountryOfTaxName = (await _countryService.FindAsync((int)Data.CountryOfTaxId)).Name;
                }

                string JobId = BackgroundJob.Enqueue(() =>
                 _emailSender.SendUserApplicationCompletedEmail(UserAppEmailData)
                    );
                _logger.LogInformation("User Application final submit email sent with job id: {0}", JobId);

                string JobIdCustomer = BackgroundJob.Enqueue(() =>
                                   _emailSender.SendUserApplicationCompletedEmailToCustomer(User));
                //await _emailSender.SendUserApplicationCompletedEmailToCustomer(User);
                _logger.LogInformation("User Application final submit email sent to Customer with job id: {0}", JobIdCustomer);
            }

            return _mapper.Map<UserApplicationDTO>(UserApp);
        }

        public async Task<UserApplicationDTO> UpdateUserApplicationStatus(int UserApplicationId, short Status)
        {
            if (Status == (short)Globals.ApplicationStatus.Posted)//if (Status == 5)
            {
                //SubmitDigitalAccountResponseDTO response = await SubmitDigitalAccount(UserApplicationId);
                SubmitSaleResponseDTO response = await PostApplicationDataToCloud(UserApplicationId);
                if (response.ResponseCode == "0")
                {
                    var UserApp = await _userApplicationDBService.UpdateUserApplicationStatus(UserApplicationId,
                        Status);

                    return _mapper.Map<UserApplicationDTO>(UserApp);
                }
                else
                {
                    throw new MyAPIException(response.ResponseMessage == "" ? "Error! Empty response received" : response.ResponseMessage);
                }
            }
            else if (Status == (short)Globals.ApplicationStatus.Approved)//else if (Status == 3)
            {
                var UserApp = await _userApplicationDBService.UpdateUserApplicationStatus(UserApplicationId,
                    Status);
                //Update User
                await UpdateUser(UserApp);

                User User = await _userDBService.FindAsync(UserApp.UserId);

                string JobIdCustomer = BackgroundJob.Enqueue(() =>
                                   _emailSender.SendUserApplicationApprovedEmailToCustomer(User));

                //await _emailSender.SendUserApplicationApprovedEmailToCustomer(User);
                _logger.LogInformation("User Application final submit email sent to Customer with job id: {0}", JobIdCustomer);

                return _mapper.Map<UserApplicationDTO>(UserApp);
            }
            else if (Status == (short)Globals.ApplicationStatus.Rejected)
            {
                var UserApp = await _userApplicationDBService.UpdateUserApplicationStatus(UserApplicationId,
                   Status);

                User User = await _userDBService.FindAsync(UserApp.UserId);

                string JobIdCustomer = BackgroundJob.Enqueue(() => _emailSender.SendInitialInvestmentRejectedToCustomer(User));
                _logger.LogInformation("User Application final submit email sent to Customer with job id: {0}", JobIdCustomer);

                return _mapper.Map<UserApplicationDTO>(UserApp);
            }
            else if (Status == (short)Globals.ApplicationStatus.InvestmentApproved)
            {
                var UserApp = await _userApplicationDBService.UpdateUserApplicationStatus(UserApplicationId, Status);
                await _investmentRequestDBService.UpdateInvestmentRequestStatusAsync((int)UserApp.InitialInvestmentRequestID, (short)Globals.RequestStatus.Approved);

                return _mapper.Map<UserApplicationDTO>(UserApp);
            }
            else if (Status == (short)Globals.ApplicationStatus.InvestmentRejected)
            {
                var UserApp = await _userApplicationDBService.UpdateUserApplicationStatus(UserApplicationId, Status);
                await _investmentRequestDBService.UpdateInvestmentRequestStatusAsync((int)UserApp.InitialInvestmentRequestID, (short)Globals.RequestStatus.Rejected);                

                return _mapper.Map<UserApplicationDTO>(UserApp);
            }
            else
            {
                var UserApp = await _userApplicationDBService.UpdateUserApplicationStatus(UserApplicationId,
                    Status);

                return _mapper.Map<UserApplicationDTO>(UserApp);
            }
        }

        private async Task<User> UpdateUser(UserApplication Application)
        {
            User User = new User();
            User.UserId = Application.UserId;
            User.CNIC = Application.CNIC;

            string[] Names = Application?.Name?.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (Names?.Length > 2)
            {
                User.LastName = Names[Names.Length - 1];
                User.FirstName = string.Join(" ", Names.Take(Names.Length - 1).ToArray());
            }
            else if (Names?.Length == 2)
            {
                User.LastName = Names[1];
                User.FirstName = Names[0];
            }
            else
            {
                User.FirstName = Names?[0];
            }

            List<UserApplicationDocumentDTO> Documents = await _userAppDocumentDBService.GetAllUserApplicationDocuments(Application.UserApplicationId);
            User.ProfileImage = Documents.Find(x => x.DocumentId == 1)?.Document;

            await _userDBService.UpdateUser(User.UserId, User);
            return User;
        }

        public async Task<CNICCheckResponseDTO> CheckCNICReuse(string CellNo, string CNIC)
        {
            bool result = await _userApplicationDBService.DoesCNICExistsForOtherApplication(
                CellNo, CNIC);

            return new CNICCheckResponseDTO()
            {
                IsSuccess = result,
                ErrorMessage = result ? "CNIC already exists in another application." : ""
            };
        }

        public async Task<int> DeleteUserApplication(int UserApplicationId)
        {
            return await _userApplicationDBService.DeleteUserApplication(UserApplicationId); ;
        }

        public async Task<SubmitSaleResponseDTO> PostApplicationDataToCloud(int UserApplicationId)
        {
            UserApplication UserApplication = await _userApplicationDBService.FindAsync(UserApplicationId);
            User User = await _userDBService.FindAsync(UserApplication.UserId);           

            List<UserBank> UserBanks = await _userBankDBService.GetAllForUser(UserApplication.UserId);
            InvestmentRequestDTO InitialInvestment = await _spService.GetSP<InvestmentRequestDTO>(INVSTREQ_PACKAGE_NAME +
                                            ".GET_INVESTMENT_REQUEST", (int)UserApplication.InitialInvestmentRequestID);

            List<FundDTO> funds = await _fundService.GetFundsAsync();
            FundDTO FromFund = funds.Where(x => x.FundId == InitialInvestment.FundId).FirstOrDefault();

            if (string.IsNullOrEmpty(FromFund?.ITMindsFundID))
            {
                throw new MyAPIException("Error! IT Minds From Fund ID is null");
            }

            AccountCategory AccountCategory = await _accountCategoryService.FindAsync((int)UserApplication.AccountCategoryId);

            SubmitSaleResponseDTO response = await _cloudService.PostUserApplicationData(UserApplication, User, InitialInvestment, UserBanks, FromFund, AccountCategory);

            if(response.ResponseCode != "0")
            {
                throw new MyAPIException(response.ResponseMessage);
            }
            return response;
        }

        public async Task<SubmitDigitalAccountResponseDTO> SubmitDigitalAccount(int UserApplicationId)
        {
            UserApplication UserApplication = await _userApplicationDBService.FindAsync(UserApplicationId);

            List<UserBank> UserBanks = await _userBankDBService.GetAllForUser(UserApplication.UserId);

            SubmitDigitalAccountResponseDTO response = await _cloudService.SubmitDigitalAccount(UserApplication, UserBanks);

            return response;
        }

        /// <summary>
        /// When User submits Initial Investment then this function is called to update User Application with investment details
        /// </summary>
        /// <param name="UserApplicationId"></param>
        /// <param name="InvestmentRequestID"></param>
        /// <returns></returns>
        public async Task<UserApplicationDTO> UpdateInitialInvestment(int UserApplicationId, int InvestmentRequestID)
        {
            var UserApp = await _userApplicationDBService.UpdateInitialInvestment(UserApplicationId, InvestmentRequestID);

            User User = await _userDBService.FindAsync(UserApp.UserId);

            string JobIdCustomer = BackgroundJob.Enqueue(() => _emailSender.SendInitialInvestmentReceivedToCustomer(User));
            _logger.LogInformation("User Application final submit email sent to Customer with job id: {0}", JobIdCustomer);

            return _mapper.Map<UserApplicationDTO>(UserApp);

        }

    }
}
