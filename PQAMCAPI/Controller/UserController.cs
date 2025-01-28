using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using PQAMCAPI.Models;
using PQAMCClasses.DTOs;
using PQAMCAPI.Interfaces.Services;
using API.Classes;
using Services;
using Hangfire;
using PQAMCAPI.Services;
using PQAMCAPI.Services.DB;
using PQAMCClasses;

namespace PQAMCAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IOTPService _otpService;       
        private readonly ISMSService _smsService;
        private readonly IEmailSender _emailSender;
        private readonly ITokenService _tokenService;
        private readonly ISMSLogDBService _SMSLogDBService;
        private readonly ILogger _logger;

        public UserController(IUserService userService, IOTPService otpService, ISMSService smsService, IEmailSender emailSender, ITokenService tokenService,
            ISMSLogDBService SMSLogDBService, ILogger<UserController> logger)
        {
            _userService = userService;
            _smsService = smsService;
            _otpService = otpService;
            _emailSender = emailSender;
            _tokenService = tokenService;
            _SMSLogDBService = SMSLogDBService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes= "PQAMCAuthScheme")]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            var Users = await _userService.GetAllAsync();
            return Users.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.FindAsync(id);

            if (user == null)
                return NotFound();

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Register([FromBody] RegisterData model)
        {
            string OTP = await _otpService.GenerateOTP();

            var user = new User
            {
                Email = model.Email,
                MobileNumber = model.MobileNumber,
                OTP = OTP
            };
            
            User newUser = await _userService.InsertUser(user);
            newUser.OTP = null;

            return newUser;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserAuthData>> Login([FromBody] LoginData model)
        {
            return await _userService.Login(model);
        }

        [HttpPost("logout")]
        [Authorize(AuthenticationSchemes= "PQAMCAuthScheme")]
        public async Task<ActionResult<bool>> Logout()
        {
            return await _userService.Logout();
        }

        [HttpPost("sendOTP")]
        public async Task<ActionResult<bool>> SendOTP([FromBody] LoginData model)
        {
            DateTime Current = DateTime.UtcNow;
            DateTime Previous = Current.AddMinutes(-2);

            int Count = await _SMSLogDBService.GetSMSCount(model.Mobile_Number, Current, Previous);

            if (Count <= Globals.Constants.SMSCount)
            {
                User _user = await _userService.GetUserWith("", model.Mobile_Number);

                if (_user.AccountStatus == Globals.AccountStatus.Locked)
                {
                    throw new MyAPIException(Globals.ErrorMessages.AccountLockedDueToIncorrectOTPAttempts);
                }

                string OTP = await _otpService.GenerateOTP();

                await _userService.UpdateOTP(_user.UserId, OTP);

                _user.OTP = OTP;

                SMSLog SMSLog = new SMSLog
                {
                    MobileNumber = model.Mobile_Number,
                    Operation = (int)Globals.SMSOperations.OTP,
                    UserID = null
                };

                var newSMSLog = _SMSLogDBService.InsertSMSLog(SMSLog);
                string JobId = BackgroundJob.Enqueue(() => _smsService.SendSMS(model.Mobile_Number,
                                   "Dear Customer, your Pak-Qatar AMC app access OTP is: " + OTP));

                _logger.LogInformation("OTP SMS sent with job id: {0}", JobId);

                string EmailJobId = BackgroundJob.Enqueue(() =>
                                        _emailSender.SendOTPEmail(_user));

                _logger.LogInformation("OTP Email sent with job id: {0}", JobId);

                return true;
            }
            else
            {
                throw new MyAPIException(Globals.ErrorMessages.TooManyOTPRequests);
            }
        }

        [HttpPost("sendOTPAMC")]
        public async Task<ActionResult<bool>> SendOTPAMC([FromBody] LoginData model)
        {
            AMCUserDTO _user = await _userService.GetAMCUser(model.Mobile_Number);

            string OTP = await _otpService.GenerateOTP();

            //await _userService.UpdateOTP(_user.UserId, int.Parse(OTP));

            string JobId = BackgroundJob.Enqueue(() => _smsService.SendSMS(model.Mobile_Number,
                               "Dear Customer, your Pak-Qatar AMC app access OTP is: " + OTP));
            _logger.LogInformation("OTP sent with job id: {0}", JobId);

            return true;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }
            try
            {
                User updatedUser = await _userService.UpdateUser(id, user);
                return updatedUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUser(id);
            return deleted;
        }

        [HttpPost("refreshToken")]
        [Authorize(AuthenticationSchemes = "PQAMCAuthScheme")]
        public async Task<ActionResult<AccessToken>> RefreshToken([FromBody] RefreshAccessTokenDTO CurrentAccessToken)
        {
            return await _tokenService.RefreshAccessToken(CurrentAccessToken);
        }
    }
}
