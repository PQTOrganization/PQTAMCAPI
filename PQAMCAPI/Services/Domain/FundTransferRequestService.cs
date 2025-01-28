using API.Classes;
using Hangfire;
using PQAMCAPI.Interfaces;
using PQAMCAPI.Interfaces.Services;
using PQAMCClasses;
using PQAMCClasses.CloudDTOs;
using PQAMCClasses.DTOs;
using Services;
using ExtensionMethods;

namespace PQAMCAPI.Services.Domain
{
    public class FundTransferRequestService : IFundTransferRequestService
    {
        const string PACKAGE_NAME = "AMC_FUND_TRANSFER_REQUEST_PKG";

        private readonly IStoreProcedureService _spService;
        private readonly IFundTransferRequestDBService _dbService;
        private readonly ICloudService _cloudService;
        private readonly IFundService _fundService;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly IUserContextService _userContextService;
        public FundTransferRequestService(IStoreProcedureService spService,
            IFundTransferRequestDBService dbService, ICloudService cloudService, 
            IEmailSender emailSender, ILogger<FundTransferRequestService> logger, IFundService fundService,
            IUserContextService userContextService)
        {
            _spService = spService;
            _dbService = dbService;
            _cloudService = cloudService;
            _emailSender = emailSender;
            _logger = logger;
            _fundService = fundService;
            _userContextService = userContextService;
        }

        public async Task<List<FundTransferRequestDTO>> GetAllFundTransferRequestsAsync()
        {
            var Requests = await _spService.GetAllSP<FundTransferRequestDTO>(PACKAGE_NAME + 
                                                             ".GET_ALL_FUND_TRANSFER_REQUESTS" , -1);
            return Requests;
        }

        public async Task<FundTransferRequestDTO> GetFundTransferRequestAsync(
            int FundTransferRequestId)
        {
            var Request = await _spService.GetSP<FundTransferRequestDTO>(PACKAGE_NAME + 
                                            ".GET_FUND_TRANSFER_REQUEST", FundTransferRequestId);
            return Request;
        }

        public async Task<List<FundTransferRequestDTO>> GetAllFundTransferRequestsForFolioAsync(
            string FolioNumber)
        {
            var Requests = await _spService.GetAllSP<FundTransferRequestDTO>(PACKAGE_NAME +
                                   ".GET_ALL_FUND_TRANSFER_REQUESTS_FOR_FOLIO", FolioNumber);
            return Requests;
        }

        public async Task<FundTransferRequestDTO> InsertFundTransferRequestAsync(
            FundTransferRequestDTO Request)
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

            //bool IsFundTransferAllowed = await IsFundTransferRequestAllowed(Request);

            //if (IsFundTransferAllowed)
            //{
                var NewRequest = await _dbService.InsertFundTransferRequestAsync(Request);
                var RequestForEmail = await GetFundTransferRequestAsync(Request.FundTransferRequestId);

            string JobId = BackgroundJob.Enqueue(() => _emailSender.SendFundTransferRequestEmail(RequestForEmail));
            _logger.LogInformation("Fund Transfer request email sent with job id: {0}", JobId);

            return NewRequest;
        }

        public async Task UpdateFundTransferRequestStatusAsync(int FundTransferRequestId, 
            short Status)
        {
            if (Status == (short)Globals.RequestStatus.Posted)//if (Status == 4)
            {
                SubmitResponseDTO Response = await SubmitFundTransferRequesToCloud(FundTransferRequestId);

                if(Response.ResponseCode == "0")
                {
                    await _dbService.UpdateFundTransferRequestStatusAsync(FundTransferRequestId, Status);
                }
                else
                {
                    throw new MyAPIException(Response.ResponseMessage == "" ? "Error! Empty response received" : Response.ResponseMessage);
                }
            }
            else
            {
                await _dbService.UpdateFundTransferRequestStatusAsync(FundTransferRequestId, Status);
            }
        }

        public async Task<SubmitResponseDTO> SubmitFundTransferRequesToCloud(int FundTransferRequestId)
        {
            FundTransferRequestDTO FundTransferRequest = await GetFundTransferRequestAsync(FundTransferRequestId);

            List<FundDTO> funds = await _fundService.GetFundsAsync();
            FundDTO FromFund = funds.Where(x => x.FundId == FundTransferRequest.FromFundId).FirstOrDefault();
            FundDTO ToFund = funds.Where(x => x.FundId == FundTransferRequest.ToFundId).FirstOrDefault();

            if (FromFund?.ITMindsFundID == null)
            {
                throw new MyAPIException("Error! IT Minds From Fund ID is null");
            }
            if(ToFund?.ITMindsFundID == null)
            {
                throw new MyAPIException("Error! IT Minds To Fund ID is null");
            }

            SubmitResponseDTO Response = await _cloudService.SubmitConversion(FundTransferRequest, FromFund, ToFund);

            return Response;
        }
    }
}
