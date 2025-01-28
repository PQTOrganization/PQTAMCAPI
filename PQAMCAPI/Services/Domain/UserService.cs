using AutoMapper;

using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;
using PQAMCClasses.DTOs;
using API.Classes;
using Services;
using Hangfire;
using static System.Net.WebRequestMethods;
using PQAMCClasses;
using Newtonsoft.Json;

namespace PQAMCAPI.Services.Domain
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IUserDBService _userDBService;
        private readonly IUserApplicationDBService _userAppDBService;
        private readonly ISMSLogDBService _SMSLogDBService;
        private readonly ISMSService _smsService;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;

        private bool _allowOTPBypass = false;

        public UserService(IUserDBService userDBService, IUserApplicationDBService userApplicationDBService,
                           ITokenService tokenService, IMapper mapper, ISMSService smsService, IEmailSender emailSender,
                           ISMSLogDBService SMSLogDBService, ILogger<UserService> logger, IConfiguration configuration)
        {
            _mapper = mapper;
            _userDBService = userDBService;
            _tokenService = tokenService;
            _userAppDBService = userApplicationDBService;
            _SMSLogDBService = SMSLogDBService;
            _smsService = smsService;
            _emailSender = emailSender;
            _logger = logger;

            _allowOTPBypass = configuration.GetValue("AllowOTPBypass", false);
        }

        public Task<int> DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindAsync(int userId)
        {
            return await _userDBService.FindAsync(userId);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _userDBService.GetAllAsync();
        }

        public async Task<UserAuthData> Login(LoginData model)
        {
            User _user = await _userDBService.GetUserWith("", model.Mobile_Number);

            if (_user.OTP == model.Otp || (_allowOTPBypass && model.Otp == "12345"))
            {
                _user.IncorrectOTPAttempts = 0;
                _user.AccountStatus = Globals.AccountStatus.Active;

                UserAuthData authData = _mapper.Map<UserAuthData>(_user);
                UserApplication UserApp = await _userAppDBService.GetApplicationForUser(_user.UserId);

                string AccountCategoryID = Globals.AccountCategories.CIS.ToString();

                if (UserApp != null)
                {
                    authData.UserApplicationId = UserApp.UserApplicationId;
                    authData.ApplicationStatusId = UserApp.ApplicationStatusId;
                    authData.AccountCategoryID = UserApp.AccountCategoryId;
                    AccountCategoryID = UserApp.AccountCategoryId.ToString();
                }

                List<UserFolioDTO> FolioList = await _userDBService.GetFoliosOfUser(model.Mobile_Number);
                authData.FolioList = FolioList;

                string key = JsonConvert.SerializeObject(new SessionSecurityKeys
                {
                    FolioList = FolioList,
                    IsAdmin = false,
                    UserApplicationID = UserApp == null ? "" : UserApp.UserApplicationId.ToString(),
                    CNIC = UserApp == null ? "" : UserApp.CNIC.ToString(),
                    UserId = _user.UserId
                });
                authData.TokenInfo = await _tokenService.GenerateAccessToken(_user, AccountCategoryID, key);

                return authData;
            }
            else
            {
                _user.IncorrectOTPAttempts = _user.IncorrectOTPAttempts + 1;

                if(_user.IncorrectOTPAttempts >= Globals.Constants.OTPRetryLimit)
                {
                    _user.AccountStatus = Globals.AccountStatus.Locked;
                }

                await _userDBService.UpdateOTPRetryAttempts(_user.UserId, _user.IncorrectOTPAttempts, _user.AccountStatus);

                if(_user.IncorrectOTPAttempts >= Globals.Constants.OTPRetryLimit)
                {
                    throw new MyAPIException(Globals.ErrorMessages.AccountLockedDueToIncorrectOTPAttempts);
                }
                throw new MyAPIException("OTP Doesn't Match");
            }
        }


        public async Task<bool> Logout()
        {
            return true;

            //return await InsertExpireToken(new UserToken()
            //{
            //    UserId = int.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier)?.Value),
            //    Token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", ""),
            //    TokenDate = DateTime.UtcNow,
            //});
        }

        public async Task<User> InsertUser(User user)
        {
            var NewUser = await _userDBService.InsertUser(user);

            SMSLog SMSLog = new SMSLog
            {
                MobileNumber = NewUser.MobileNumber,
                Operation = (int)Globals.SMSOperations.OTP,
                UserID = NewUser.UserId
            };

            var newSMSLog = _SMSLogDBService.InsertSMSLog(SMSLog);
            string JobId = BackgroundJob.Enqueue(() => _smsService.SendSMS(NewUser.MobileNumber,
                "Dear Customer, your Pak-Qatar AMC app access OTP is: " + NewUser.OTP));
            _logger.LogInformation("OTP SMS sent with job id: {0}", JobId);

            string EmailJobId = BackgroundJob.Enqueue(() => _emailSender.SendOTPEmail(NewUser));
            _logger.LogInformation("OTP Email sent with job id: {0}", EmailJobId);

            JobId = BackgroundJob.Enqueue(() => _emailSender.SendUserRegistrationEmail(user));
            _logger.LogInformation("User registeration email sent with job id: {0}", JobId);

            return NewUser;
        }

        public Task<User> UpdateUser(int userId, User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserWith(string email, string number)
        {
            return await _userDBService.GetUserWith(email, number);
        }

        public async Task<AMCUserDTO> GetAMCUser(string number)
        {
            return await _userDBService.GetAMCUser(number);
        }

        public async Task<bool> IfUserExist(User user)
        {
            return await _userDBService.IfUserExist(user);
        }

        public async Task<bool> UpdateOTP(int UserId, string otp)
        {
            return await _userDBService.UpdateOTP(UserId, otp);
        }
    }
}
