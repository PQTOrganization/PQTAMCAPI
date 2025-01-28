using API.Classes;
using AutoMapper;
using Hangfire;
using Org.BouncyCastle.Asn1.Ocsp;
using PQAMCAPI.Interfaces;
using PQAMCAPI.Interfaces.Services;
using PQAMCAPI.Models;
using PQAMCAPI.Services.DB;
using PQAMCClasses;
using PQAMCClasses.CloudDTOs;
using PQAMCClasses.DTOs;
using Services;
using static PQAMCClasses.Globals;
using ExtensionMethods;

namespace PQAMCAPI.Services.Domain
{
    public class InvestmentRequestService : IInvestmentRequestService
    {
        const string PACKAGE_NAME = "AMC_INVESTMENT_REQUEST_PKG";

        private readonly IStoreProcedureService _spService;
        private readonly IInvestmentRequestDBService _dbService;
        private readonly ICloudService _cloudService;
        private readonly IUserApplicationService _userApplicationService;
        private readonly IFundService _fundService;
        private readonly IUserService _userService;
        private readonly IEmailSender _emailSender;
        private readonly ISMSService _smsService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IAccountCategoryService _accountCategoryService;

        private readonly string _blinqInvoicePrefix;

        private readonly IUserContextService _userContextService;

        public InvestmentRequestService(IStoreProcedureService spService, IInvestmentRequestDBService dbService,
            ICloudService cloudService, IUserApplicationService userApplicationService, IEmailSender emailSender, 
            ILogger<InvestmentRequestService> logger, IFundService fundService, IUserService userService, IAccountCategoryService accountCategoryService,
            ISMSService smsService, IConfiguration configuration, IMapper mapper, IUserContextService userContextService)
        {
            _spService = spService;
            _dbService = dbService;
            _cloudService = cloudService;
            _userApplicationService = userApplicationService;
            _emailSender = emailSender;
            _logger = logger;
            _fundService = fundService;
            _userService = userService;
            _smsService = smsService;
            _mapper = mapper;
            _accountCategoryService = accountCategoryService;            
            _blinqInvoicePrefix = configuration.GetValue("BlinqInvoicePrefix", "");
            _userContextService = userContextService;
        }

        public async Task<List<InvestmentRequestDTO>> GetAllInvestmentRequestsAsync()
        {
            var Requests = await _spService.GetAllSP<InvestmentRequestDTO>(PACKAGE_NAME +
                                                                ".GET_ALL_INVESTMENT_REQUESTS_WO_IMAGES", -1);
            return Requests;
        }

        public async Task<InvestmentRequestDTO> GetInvestmentRequestAsync(int InvestmentRequestId)
        {
            var Request = await _spService.GetSP<InvestmentRequestDTO>(PACKAGE_NAME +
                                            ".GET_INVESTMENT_REQUEST", InvestmentRequestId);
            return Request;
        }

        public async Task<InvestmentRequestDTO> GetInvestmentRequestForReferenceAsync(string ReferenceNumber)
        {
            var Request = await _spService.GetSP<InvestmentRequestDTO>(PACKAGE_NAME +
                                            ".GET_INVESTMENT_REQUEST_FOR_REFERENCE", ReferenceNumber);
            return Request;
        }

        public async Task<List<InvestmentRequestDTO>> GetAllInvestmentRequestsForFolioAsync(string FolioNumber)
        {
            var Requests = await _spService.GetAllSP<InvestmentRequestDTO>(PACKAGE_NAME +
                                         ".GET_ALL_INVESTMENT_REQUESTS_WO_IMAGES_FOR_FOLIO", FolioNumber);
            
            return Requests;
        }

        public async Task<InvestmentRequestDTO> InsertInvestmentRequestAsync(InvestmentRequestDTO Request)
        {
            SessionSecurityKeys Keys = _userContextService.GetSessionSecurityKeys();

            if (Request.UserId != Keys.UserId)
            {
                throw new MyAPIException(Globals.ErrorMessages.NotFound);

            }

            bool Found = false;
            for (int i = 0; i < Keys.FolioList.Count; i++)
            {
                if (Request.FolioNumber == Keys.FolioList[i].FolioNumber)
                {
                    Found = true;
                }
            }

            if (Found == false)
            {
                throw new MyAPIException(Globals.ErrorMessages.NotFound);
            }

            if (Request.PaymentMode == (short)PaymentModes.OnlineTransfer)
                Request.OnlinePaymentReference = "107784";
            else
                if (Request.PaymentMode == (short)PaymentModes.Blinq)
                    Request.OnlinePaymentReference = _blinqInvoicePrefix;

            var NewRequest = await _dbService.InsertInvestmentRequestAsync(Request);
            var RequestForEmail = await GetInvestmentRequestAsync(Request.InvestmentRequestId);
            var UserInfo = await _userService.FindAsync(RequestForEmail.UserId);

            if (UserInfo != null && !string.IsNullOrEmpty(UserInfo.Email) &&
                !string.IsNullOrEmpty(UserInfo.MobileNumber))
            {
                string JobId = BackgroundJob.Enqueue(() => _emailSender.SendInvestmentRequestEmail(UserInfo.Email,
                                                                RequestForEmail));
                _logger.LogInformation("Payment received email sent with job id: {0A}", JobId);


                var Message = "Dear Client, your investment request in " + RequestForEmail.FundName +
                    " has been received. Your transaction no is " + RequestForEmail.OnlinePaymentReference +
                    ". The updated account statement will be sent to your registered email. " +
                    "In case of queries, please call (021) 111-PQAMCL (772625) " +
                    "or WhatsApp +92 312 0080366 from 9:00 AM to 5:00 PM (Monday to Friday)";

                // Don't SMS blinq requests to customer
                if (Request.PaymentMode != (short)PaymentModes.Blinq)
                {
                    string SMSJobId = BackgroundJob.Enqueue(() => _smsService.SendSMS(UserInfo.MobileNumber, Message));
                    _logger.LogInformation("Payment received SMS sent with job id: {0A}", SMSJobId);
                }
            }
            else
                _logger.LogInformation("User information not found/not complete for user id: {0A}",
                                       RequestForEmail.UserId);
            return NewRequest;
        }

        public async Task UpdateInvestmentRequestStatusAsync(int InvestmentRequestId, short Status)
        {
            if(Status == (short)Globals.RequestStatus.Posted)//if (Status == 4)
            {
                SubmitResponseDTO Response = await SubmitSubSaleRequestToCloud(InvestmentRequestId);
                if (Response.ResponseCode == "0")
                {
                    await _dbService.UpdateInvestmentRequestStatusAsync(InvestmentRequestId, Status);
                }
                else
                { 
                    throw new MyAPIException(Response.ErrorDescription == "" ? "Error! Empty response received" : 
                                             Response.ErrorDescription); 
                }
            }
            else
            {
                await _dbService.UpdateInvestmentRequestStatusAsync(InvestmentRequestId, Status);
            }
        }

        public async Task<SubmitResponseDTO> SubmitSubSaleRequestToCloud(int InvestmentRequestId)
        {
            InvestmentRequestDTO Request = await GetInvestmentRequestAsync(InvestmentRequestId);

            List<FundDTO> funds = await _fundService.GetFundsAsync();
            FundDTO FromFund = funds.Where(x => x.FundId == Request.FundId).FirstOrDefault();
     
            if (string.IsNullOrEmpty(FromFund?.ITMindsFundID))
            {
                throw new MyAPIException("Error! IT Minds From Fund ID is null");
            }

            UserApplicationDTO Application = await _userApplicationService.GetApplicationForUser(Request.UserId);
            AccountCategory AccountCategory = await _accountCategoryService.FindAsync((int)Application.AccountCategoryId);

            SubmitResponseDTO response = await _cloudService.SubmitSubsale(Request, Application, FromFund, AccountCategory);

            return response;
        }

        public async Task<bool> UpdateInvestmentRequestBlinqResponse(string ReferenceNumber, string Response, 
            short Status)
        {
            var Payment = await _dbService.UpdateInvestmentRequestBlinqResponse(ReferenceNumber, Response, Status);

            if (Payment && Status == (short)RequestStatus.Paid)
            {
                var InvestmentRequest = await GetInvestmentRequestForReferenceAsync(ReferenceNumber);
                var UserInfo = await _userService.FindAsync(InvestmentRequest.UserId);

                if (UserInfo != null && !string.IsNullOrEmpty(UserInfo.Email) && 
                    !string.IsNullOrEmpty(UserInfo.MobileNumber))
                {
                    string JobId = BackgroundJob.Enqueue(() => _emailSender.SendPaymentSuccessEmail(UserInfo.Email,
                                                                    InvestmentRequest));
                    _logger.LogInformation("Payment received email sent with job id: {0A}", JobId);


                    var Message = "Dear Client, your investment request in " + InvestmentRequest.FundName + 
                        " has been received. Your transaction no is " + InvestmentRequest.OnlinePaymentReference + 
                        ". The updated account statement will be sent to your registered email. " + 
                        "In case of queries, please call (021) 111-PQAMCL (772625) " + 
                        "or WhatsApp +92 312 0080366 from 9:00 AM to 5:00 PM (Monday to Friday)";

                    string SMSJobId = BackgroundJob.Enqueue(() => _smsService.SendSMS(UserInfo.MobileNumber, Message));
                    _logger.LogInformation("Payment received SMS sent with job id: {0A}", SMSJobId);
                }
                else
                    _logger.LogInformation("User information not found/not complete for user id: {0A}", 
                                           InvestmentRequest.UserId);
            }

            return Payment;
        }

        public async Task<InvestmentRequestDTO> InsertInitialInvestmentRequestAsync(InitialInvestmentRequestDTO InitialRequest)
        {
            SessionSecurityKeys Keys = _userContextService.GetSessionSecurityKeys();

            if (InitialRequest.UserId != Keys.UserId || InitialRequest.CNIC != Keys.CNIC ||
                InitialRequest.UserApplicationID.ToString() != Keys.UserApplicationID)
            {
                throw new MyAPIException(Globals.ErrorMessages.NotFound);

            }
           
            if (InitialRequest.PaymentMode == (short)PaymentModes.OnlineTransfer)
                InitialRequest.OnlinePaymentReference = "107784";
            else
                if (InitialRequest.PaymentMode == (short)PaymentModes.Blinq)
                InitialRequest.OnlinePaymentReference = _blinqInvoicePrefix;

            InvestmentRequestDTO Request = _mapper.Map<InvestmentRequestDTO>(InitialRequest);
            Request.IsInitialInvestment = true;
            var NewRequest = await _dbService.InsertInvestmentRequestAsync(Request);
            var UpdatedUserApplication = await _userApplicationService.UpdateInitialInvestment(InitialRequest.UserApplicationID, NewRequest.InvestmentRequestId);
            //var RequestForEmail = await GetInvestmentRequestAsync(Request.InvestmentRequestId);
            
          
            return NewRequest;
        }

        //public async Task UpdateInitialInvestmentRequestStatusAsync(int InvestmentRequestId, short Status)
        //{
        //    if (Status == (short)Globals.RequestStatus.Posted)//if (Status == 4)
        //    {
        //        SubmitResponseDTO Response = await SubmitSubSaleRequestToCloud(InvestmentRequestId);
        //        if (Response.ResponseCode == "0")
        //        {
        //            await _dbService.UpdateInvestmentRequestStatusAsync(InvestmentRequestId, Status);
        //        }
        //        else
        //        {
        //            throw new MyAPIException(Response.ErrorDescription == "" ? "Error! Empty response received" :
        //                                     Response.ErrorDescription);
        //        }
        //    }
        //    else
        //    {
        //        await _dbService.UpdateInvestmentRequestStatusAsync(InvestmentRequestId, Status);
        //    }
        //}
    }
}
