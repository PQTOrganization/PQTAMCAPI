using AutoMapper;

using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;
using PQAMCClasses.DTOs;
using Newtonsoft.Json;
using Microsoft.CodeAnalysis;
using PQAMCAPI.Services.DB;
using PQAMCAPI.Controller.Onboarding;
using Hangfire;
using Services;
using static System.Net.WebRequestMethods;

namespace PQAMCAPI.Services.Domain
{
    public class UserApplicationDiscrepancyService : IUserApplicationDiscrepancyService
    {
        private readonly IMapper _mapper;
        private readonly IUserApplicationDiscrepancyDBService _dbService;
        private readonly IUserApplicationService _appService;
        private readonly IUserApplicationDocumentDBService _appDocDBService;
        private readonly IUserBankDBService _appBankDBService;
        private readonly IUserDBService _userDBService;
        private readonly ISMSService _smsService;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;

        public UserApplicationDiscrepancyService(IUserApplicationDiscrepancyDBService dbService,
            IUserApplicationService appService, IUserApplicationDocumentDBService appDocDBService,
            IUserBankDBService appBankDBService, IUserDBService userDBService,
            IEmailSender emailSender, ISMSService smsService,
            ILogger<UserApplicationDiscrepancyController> logger, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _dbService = dbService;
            _appService = appService;
            _appDocDBService = appDocDBService;
            _appBankDBService = appBankDBService;
            _userDBService = userDBService;
            _emailSender = emailSender;
            _smsService = smsService;
        }

        public async Task<UserApplicationDiscrepancyDTO> GetUserApplicationDiscrepancy(
            int UserApplicationDiscrepancyId)
        {
            var Discrepancy = await _dbService.FindAsync(UserApplicationDiscrepancyId);

            return _mapper.Map<UserApplicationDiscrepancyDTO>(Discrepancy);
        }

        public async Task<UserApplicationDiscrepancyDTO> GetDiscrepancyForUserApplication(
            int UserApplicationId)
        {
            var Discrepancy = await _dbService.GetDiscrepancyForUserApplication(UserApplicationId);

            return _mapper.Map<UserApplicationDiscrepancyDTO>(Discrepancy);
        }

        public async Task<UserApplicationDiscrepancyDTO> InsertDiscrepancy(InsertDiscrepancyDTO Data)
        {
            var ExistingApplication = await _appService.GetUserApplication(Data.Application.UserApplicationId);
            var ApplicationJSON = JsonConvert.SerializeObject(ExistingApplication);

            var Disc = new UserApplicationDiscrepancy()
            {
                UserApplicationId = Data.Application.UserApplicationId,
                ApplicationData = ApplicationJSON,
                DiscrepantFields = Data.DiscrepantFields
            };

            var newDiscrepancy = await _dbService.InsertApplicationDiscrepancy(Disc);

            // Doing this here to enforce business rule
            Data.Application.ApplicationStatusId = 2;
            Data.Application.IsFinalSubmit = false;

            // This has to be done 2nd.
            await _appService.UpdateUserApplication(Data.Application.UserApplicationId, Data.Application);

            // Delete discrepant bank
            if (Data.DiscrepantFields.ToLower().Contains("bankid"))
                await _appBankDBService.DeleteUserBank(ExistingApplication.UserId, ExistingApplication.BankId);

            // Delete discrepant documents now
            string[] DiscFieldsArray = Data.DiscrepantFields.Split(",");

            foreach (string DiscField in DiscFieldsArray)
            {
                if (DiscField.Contains("doc_"))
                {
                    int DocumentId = int.Parse(DiscField.Split("_")[1]);

                    await _appDocDBService.DeleteDocument(Data.Application.UserApplicationId, 
                                                          DocumentId);
                }
            }

            User user = await _userDBService.FindAsync(Data.Application.UserId);

            if (user == null)
                _logger.LogError("Cannot find user for discrepant data: {0}",
                    JsonConvert.SerializeObject(Data));
            else
            {
                string JobId = BackgroundJob.Enqueue(() => _smsService.SendSMS(user.MobileNumber,
                   "Dear Customer, your Pak-Qatar AMC account application has some issues. " + 
                   "Please login to the app to fix them."));

                _logger.LogInformation("Data discrepancy SMS sent with job id: {0}", JobId);

                JobId = BackgroundJob.Enqueue(() => 
                    _emailSender.SendApplicationDiscrpancyEmail(user.Email, Disc));

                _logger.LogInformation("Data discprepancy email sent with job id: {0}", JobId);
            }

            return _mapper.Map<UserApplicationDiscrepancyDTO>(newDiscrepancy);
        }
    }
}
