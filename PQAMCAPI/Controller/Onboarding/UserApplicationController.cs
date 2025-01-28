using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PQAMCAPI.Interfaces.Services;
using PQAMCAPI.Models;
using PQAMCClasses;
using PQAMCClasses.CloudDTOs;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Controller.Onboarding
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "PQAMCAuthScheme")]
    [ApiController]
    public class UserApplicationController : ControllerBase
    {
        private readonly IUserApplicationService _service;
        private readonly IUserBankDBService _userBankDBService;
        private readonly IUserApplicationDocumentDBService _docDBService;
        private readonly IMapper _mapper;

        public UserApplicationController(IUserApplicationService service, IUserBankDBService userBankDBService,
                                         IUserApplicationDocumentDBService docDBService, IMapper mapper)
        {
            _service = service;
            _userBankDBService = userBankDBService;
            _docDBService = docDBService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<UserApplicationDTO>> Get()
        {
            return await _service.GetAllUserApplications();
        }

        [HttpGet("{id}")]
        public async Task<UserApplicationDTO> Get(int id)
        {
            UserApplicationDTO UserApp = await _service.GetUserApplication(id);
            return UserApp;
        }

        [HttpPost]
        public async Task<UserApplicationDTO> Post([FromBody] UserApplicationDTO data)
        {
            var newUserApp = await _service.InsertUserApplication(data);

            await UpsertBankInfo(newUserApp);

            return newUserApp;
        }
        
        [HttpPut("{id}")]
        public async Task<UserApplicationDTO> Put(int id, [FromBody] UserApplicationDTO data)
        {

            try
            {
                var newUserApp = await _service.UpdateUserApplication(id, data);

                newUserApp.UserBankId = data.UserBankId;
                newUserApp.BankId = data.BankId;
                newUserApp.OneLinkTitle = data.OneLinkTitle;
                newUserApp.IBANNumber = data.IBANNumber;
                newUserApp.IsIBANVerified = data.IsIBANVerified;

                await UpsertBankInfo(newUserApp);

                return newUserApp;
            }
            catch(Exception ex)
            { }
            return null;
        }

        [HttpPut("Approve/{id}")]
        public async Task<UserApplicationDTO> ApproveUserApplication(int id)
        {
            return await _service.UpdateUserApplicationStatus(id, 3);
        }

        [HttpPut("ApproveInvestment/{id}")]
        public async Task<UserApplicationDTO> ApproveUserInitialInvestment(int id)
        {
            return await _service.UpdateUserApplicationStatus(id, (short)Globals.ApplicationStatus.InvestmentApproved);
        }

        [HttpPut("RejectInvestment/{id}")]
        public async Task<UserApplicationDTO> RejectUserInitialInvestment(int id)
        {
            return await _service.UpdateUserApplicationStatus(id, (short)Globals.ApplicationStatus.InvestmentRejected);
        }

        [HttpPut("Post/{id}")]
        public async Task<UserApplicationDTO> PostUserApplication(int id)
        {
            return await _service.UpdateUserApplicationStatus(id, 5);
        }

        [HttpPut("Reject/{id}")]
        public async Task<UserApplicationDTO> RejectUserApplication(int id)
        {
            return await _service.UpdateUserApplicationStatus(id, 4);
        }

        [HttpPost("CheckCNICReuse")]
        public async Task<CNICCheckResponseDTO> CheckCNICReuse([FromBody] CNICCheckRequestDTO Data)
        {
            return await _service.CheckCNICReuse(Data.CellNo, Data.CNIC);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost("digitalAccount/{id}")]
        public async Task<SubmitDigitalAccountResponseDTO> PostDigitalAccount(int id)
        {
            return await _service.SubmitDigitalAccount(id);
        }

        [HttpPut("PostCloud/{id}")]
        public async Task<SubmitSaleResponseDTO> PostUserApplicationToCloud(int id)
        {
            return await _service.PostApplicationDataToCloud(id);
        }

        private async Task UpsertBankInfo(UserApplicationDTO UserApp)
        {
            if (UserApp.BankId != null && UserApp.BankId != 0)
            {
                UserBankDTO BankInfo = new UserBankDTO()
                {
                    UserBankId = UserApp.UserBankId,
                    BankId = UserApp.BankId ?? 0,
                    UserId = UserApp.UserId,
                    OneLinkTitle = UserApp.OneLinkTitle,
                    IBANNumber = UserApp.IBANNumber,
                    IsIBANVerified = UserApp.IsIBANVerified,
                    IsOBAccount = true
                };

                if (UserApp.UserBankId == 0)
                {
                    UserBank NewUserBank = await _userBankDBService.InsertUserBank(
                                                                        _mapper.Map<UserBank>(BankInfo));
                    UserApp.UserBankId = NewUserBank.UserBankId;
                }
                else
                    await _userBankDBService.UpdateUserBank(UserApp.UserBankId,
                                                            _mapper.Map<UserBank>(BankInfo));
            }
        }

    }
}
