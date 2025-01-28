using API.Classes;
using ExtensionMethods;
using Hangfire;
using PQAMCAPI.Interfaces;
using PQAMCAPI.Interfaces.Services;
using PQAMCAPI.Models;
using PQAMCClasses;
using PQAMCClasses.CloudDTOs;
using PQAMCClasses.DTOs;
using Services;

namespace PQAMCAPI.Services.Domain
{
    public class RedemptionRequestService : IRedemptionRequestService
    {
        const string PACKAGE_NAME = "AMC_REDEMPTION_REQUEST_PKG";

        private readonly IStoreProcedureService _spService;
        private readonly IRedemptionRequestDBService _dbService;
        private readonly ICloudService _cloudService;
        private readonly IUserApplicationService _userApplicationService;
        private readonly IUserBankDBService _userBankDBService;
        private readonly IUserService _userService;
        private readonly IFundService _fundService;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly IUserContextService _userContextService;

        public RedemptionRequestService(IStoreProcedureService spService,
            IRedemptionRequestDBService dbService, ICloudService cloudService, IUserApplicationService userApplicationService,
            IUserBankDBService userBankDBService, IEmailSender emailSender, IUserService userService,
            ILogger<RedemptionRequestService> logger, IFundService fundService, IUserContextService userContextService)
        {
            _spService = spService;
            _dbService = dbService;
            _cloudService = cloudService;
            _userApplicationService = userApplicationService;
            _userBankDBService = userBankDBService;
            _userService = userService;
            _emailSender = emailSender;
            _logger = logger;
            _fundService = fundService;
            _userContextService = userContextService;
        }

        public async Task<List<RedemptionRequestDTO>> GetAllRedemptionRequestsAsync()
        {
            var Requests = await _spService.GetAllSP<RedemptionRequestDTO>(PACKAGE_NAME +
                                                                ".GET_ALL_REDEMPTION_REQUESTS" , -1);
            return Requests;
        }

        public async Task<RedemptionRequestDTO> GetRedemptionRequestAsync(int RedemptionRequestId)
        {
            var Request = await _spService.GetSP<RedemptionRequestDTO>(PACKAGE_NAME +
                                            ".GET_REDEMPTION_REQUEST", RedemptionRequestId);
            return Request;
        }

        public async Task<List<RedemptionRequestDTO>> GetAllRedemptionRequestsForFolioAsync(string FolioNumber)
        {
            var Requests = await _spService.GetAllSP<RedemptionRequestDTO>(PACKAGE_NAME +
                                         ".GET_ALL_REDEMPTION_REQUESTS_FOR_FOLIO", FolioNumber);
            return Requests;
        }

        public async Task<RedemptionRequestDTO> InsertRedemptionRequestAsync(
            RedemptionRequestDTO Request)
        {
            SessionSecurityKeys Keys = _userContextService.GetSessionSecurityKeys();

            if(Request.UserId != Keys.UserId) 
            { 
                throw new MyAPIException(Globals.ErrorMessages.NotFound); 
            
            }

            bool Found = false;
            for (int i = 0; i < Keys.FolioList.Count; i++)
            {
                if(Request.FolioNumber == Keys.FolioList[i].FolioNumber)
                {
                    Found = true;
                }
            }

            if (Found == false)
            {
                throw new MyAPIException(Globals.ErrorMessages.NotFound);
            }
            
            var NewRequest = await _dbService.InsertRedemptionRequestAsync(Request);
            var RequestForEmail = await GetRedemptionRequestAsync(Request.RedemptionRequestId);

            string JobId = BackgroundJob.Enqueue(() => _emailSender.SendRedemptionRequestEmail(RequestForEmail));
            _logger.LogInformation("Redemption request email sent with job id: {0}", JobId);

            return NewRequest;
        }

        public async Task UpdateRedemptionRequestStatusAsync(int RedemptionRequestId, short Status)
        {
            if (Status == (short)Globals.RequestStatus.Posted)//if (Status == 4) 
            {
                SubmitResponseDTO Response = await SubmitRedemptionRequestToCloud(RedemptionRequestId);
                if (Response.ResponseCode == "0")
                {
                    await _dbService.UpdateRedemptionRequestStatusAsync(RedemptionRequestId, Status);
                }
                else
                { throw new MyAPIException(Response.ResponseMessage == "" ? "Error! Empty response received" : Response.ResponseMessage); }
            }
            else
            {
                await _dbService.UpdateRedemptionRequestStatusAsync(RedemptionRequestId, Status);
            }
        }

        public async Task<SubmitResponseDTO> SubmitRedemptionRequestToCloud(int RedemptionRequestId)
        {
            RedemptionRequestDTO Request = await GetRedemptionRequestAsync(RedemptionRequestId);

            User User = await _userService.FindAsync(Request.UserId);
            UserApplicationDTO Application = await _userApplicationService.GetApplicationForUser(Request.UserId);
            //List<UserBank> UserBanks = await _userBankDBService.GetAllForUser(Request.UserId);

            List<FundDTO> funds = await _fundService.GetFundsAsync();
            FundDTO RedemptionFund = funds.Where(x => x.FundId == Request.FundId).FirstOrDefault();

            if (RedemptionFund?.ITMindsFundID == null)
            {
                throw new MyAPIException("Error! IT Minds Fund ID is null");
            }

            SubmitResponseDTO response = await _cloudService.SubmitRedemption(Request, Application, RedemptionFund, User);

            return response;
        }

        public async Task<FolioBankResponseDTO> GetFolioBankList(int UserId)
        {
            User User = await _userService.FindAsync(UserId);

            List<FolioBankDTO> folioBankList = await _cloudService.GetFolioBankListForUser(User);
            FolioBankResponseDTO Response = new FolioBankResponseDTO();
            Response.BankList = folioBankList;
            return Response;
        }
    }
}
