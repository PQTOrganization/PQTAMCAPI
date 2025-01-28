using Microsoft.Extensions.Options;

using MailKit.Net.Smtp;
using MimeKit;

using API.Classes;
using PQAMCAPI.Interfaces.Services;
using PQAMCAPI.Models;
using PQAMCAPI.Interfaces;
using PQAMCClasses.DTOs;
using static PQAMCClasses.Globals;

namespace Services
{
    public interface IEmailSender
    {
        Task<string> SendTestEmail(string EmailAddress);        
        Task<string> SendUserRegistrationEmail(User DataModel);
        Task<string> SendUserApplicationCompletedEmail(UserApplicationEmailDTO DataModel);
        Task<string> SendApplicationDiscrpancyEmail(string UserEmail, UserApplicationDiscrepancy DataModel);

        Task<string> SendInvestmentRequestEmail(string UserEmail, InvestmentRequestDTO DataModel);
        Task<string> SendRedemptionRequestEmail(RedemptionRequestDTO DataModel);
        Task<string> SendFundTransferRequestEmail(FundTransferRequestDTO DataModel);

        Task<string> SendPaymentSuccessEmail(string UserEmail, InvestmentRequestDTO DataModel);

        Task SendEmailAsync(string email, string subject, string htmlMessage, string CC = "",
            string BCC = "", List<Attachment> Attachments = null);

        Task<string> SendOTPEmail(User User);

        Task<string> SendUserApplicationCompletedEmailToCustomer(User User);

        Task<string> SendUserApplicationApprovedEmailToCustomer(User DataModel);
        Task<string> SendInitialInvestmentReceivedToCustomer(User DataModel);

        Task<string> SendInitialInvestmentRejectedToCustomer(User DataModel);
    }

    public class EmailSender : IEmailSender
    {
        private readonly string _adminEmailAddress;
        private readonly ISystemSettingsService _settingsService;
        private readonly EmailSettings _emailSettings;
        private readonly ITemplateService _TemplateService;
        private readonly ILogger _logger;

        public EmailSender(IOptions<EmailSettings> emailSettings, ITemplateService TemplateService, IConfiguration Configuration,
                           ILogger<EmailSender> logger, ISystemSettingsService settingsService)
        {
            _emailSettings = emailSettings.Value;
            _TemplateService = TemplateService;
            _settingsService = settingsService;

            _logger = logger;

            _adminEmailAddress = Configuration.GetValue<String>("AdminEmail");
        }

        public async Task<string> SendTestEmail(string EmailAddress)
        {
            String ViewName = "TestEmail";
            String Subject = "Email to test health";

            try
            {
                var Message = await _TemplateService.RenderTemplateAsync(ViewName, new Object());

                List<Attachment> Attachments = new List<Attachment>();
                Attachments.Add(new Attachment
                {
                    Type = "image",
                    Extension = "png",
                    Filename = "ProfileImage.png",
                    Base64Content = "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"
                });

                await SendEmailAsync(EmailAddress, Subject, Message, "", "", Attachments);

                return "Email Sent Successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending Test email to {0}", EmailAddress);

                return ex.Message;
            }
        }

        public async Task<string> SendUserRegistrationEmail(User DataModel)
        {
            SystemSettings Settings = await _settingsService.GetSystemSettingsAsync("REGISTER");

            String ViewName = "UserRegistration";
            String Subject = "New User Registration";

            try
            {
                var Message = await _TemplateService.RenderTemplateAsync(ViewName, DataModel);

                
                await SendEmailAsync(DataModel.Email, Subject, Message,
                                     Settings.NotificationToEmail, Settings.NotificationCCEmail);

                return "Email Sent Successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending user registeration email to {0}",
                                 DataModel.Email);

                return ex.Message;
            }
        }       
        public async Task<string> SendUserApplicationCompletedEmail(UserApplicationEmailDTO DataModel)
        {
            SystemSettings Settings = await _settingsService.GetSystemSettingsAsync("FINAL_SUBMIT");

            String ViewName = "UserApplicationCompleted";
            String Subject = "New User Application Completed";

            try
            {
                var Message = await _TemplateService.RenderTemplateAsync(ViewName, DataModel);


                await SendEmailAsync(Settings.NotificationToEmail, Subject, Message,
                                     Settings.NotificationCCEmail, "");

                return "Email Sent Successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending user application completion email to {0}",
                                 Settings.NotificationToEmail);

                return ex.Message;
            }
        }

        public async Task<string> SendUserApplicationCompletedEmailToCustomer(User DataModel)
        {
            SystemSettings Settings = await _settingsService.GetSystemSettingsAsync("FINAL_SUBMIT");

            String ViewName = "UserApplicationCompletedCustomer";
            String Subject = "E- Transaction (Online Digital Account)";

            try
            {
                var Message = await _TemplateService.RenderTemplateAsync(ViewName, DataModel);


                await SendEmailAsync(DataModel.Email, Subject, Message, Settings.NotificationToEmail,
                                     Settings.NotificationCCEmail);

                return "Email Sent Successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending user application completion email to {0}",
                                 Settings.NotificationToEmail);

                return ex.Message;
            }
        }

        public async Task<string> SendUserApplicationApprovedEmailToCustomer(User DataModel)
        {
            SystemSettings Settings = await _settingsService.GetSystemSettingsAsync("APPROVED");

            String ViewName = "UserApplicationApprovedCustomer";
            String Subject = "E- Transaction (Online Digital Account)";

            try
            {
                var Message = await _TemplateService.RenderTemplateAsync(ViewName, DataModel);


                await SendEmailAsync(DataModel.Email, Subject, Message, Settings.NotificationToEmail,
                                     Settings.NotificationCCEmail);

                return "Email Sent Successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending user application completion email to {0}",
                                 Settings.NotificationToEmail);

                return ex.Message;
            }
        }

        public async Task<string> SendApplicationDiscrpancyEmail(string UserEmail, 
            UserApplicationDiscrepancy DataModel)
        {
            SystemSettings Settings = await _settingsService.GetSystemSettingsAsync("DISCREPENCY");

            String ViewName = "UserApplicationDiscrepant";
            String Subject = "PQAMC Application Discrepancy";

            try
            {
                var Message = await _TemplateService.RenderTemplateAsync(ViewName, DataModel);

                await SendEmailAsync(UserEmail, Subject, Message, Settings.NotificationToEmail,
                                     Settings.NotificationCCEmail);

                return "Email Sent Successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending application discrepancy email to {0}",
                                 Settings.NotificationToEmail);

                return ex.Message;
            }
        }

        public async Task<string> SendInvestmentRequestEmail(string UserEmail, InvestmentRequestDTO DataModel)
        {
            SystemSettings Settings = await _settingsService.GetSystemSettingsAsync("INVEST_REQUEST");

            String ViewName = "InvestmentRequest";
            String Subject = "New Investment Request";

            try
            {
                var Message = await _TemplateService.RenderTemplateAsync(ViewName, DataModel);

                List<Attachment> Attachments = null!;

                if (!string.IsNullOrEmpty(DataModel.ProofOfPayment))
                {
                    Attachments = new List<Attachment>()
                    {
                        ConvertBase64StringToAttachment(DataModel.ProofOfPayment, "ProofOfPayment")
                    };
                }

                // Don't send blinq requests to customer
                if (DataModel.PaymentMode == (short)PaymentModes.Blinq)
                    await SendEmailAsync(Settings.NotificationToEmail, Subject, Message, Settings.NotificationCCEmail, 
                                         "", Attachments);
                else
                    await SendEmailAsync(UserEmail, Subject, Message, Settings.NotificationToEmail,
                                     Settings.NotificationCCEmail, Attachments);

                return "Email Sent Successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending investment request email to {0}",
                                 Settings.NotificationToEmail);

                return ex.Message;
            }
        }

        public async Task<string> SendPaymentSuccessEmail(string UserEmail, InvestmentRequestDTO DataModel)
        {
            SystemSettings Settings = await _settingsService.GetSystemSettingsAsync("PAYMENT_SUCCESS");

            String ViewName = "PaymentSuccess";
            String Subject = "Investment Payment Received";

            try
            {
                var Message = await _TemplateService.RenderTemplateAsync(ViewName, DataModel);

                List<Attachment> Attachments = null!;

                await SendEmailAsync(UserEmail, Subject, Message, Settings.NotificationToEmail,
                                     Settings.NotificationCCEmail, Attachments);

                return "Email Sent Successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending investment payment email to {0}",
                                 Settings.NotificationToEmail);

                return ex.Message;
            }
        }

        public async Task<string> SendRedemptionRequestEmail(RedemptionRequestDTO DataModel)
        {
            SystemSettings Settings = await _settingsService.GetSystemSettingsAsync("REDEEM_REQUEST");

            String ViewName = "RedemptionRequest";
            String Subject = "New Redemption Request";

            try
            {
                var Message = await _TemplateService.RenderTemplateAsync(ViewName, DataModel);

                await SendEmailAsync(Settings.NotificationToEmail, Subject, Message,
                                     Settings.NotificationCCEmail);

                return "Email Sent Successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending redemption request email to {0}",
                                 Settings.NotificationToEmail);

                return ex.Message;
            }
        }

        public async Task<string> SendFundTransferRequestEmail(FundTransferRequestDTO DataModel)
        {
            SystemSettings Settings = await _settingsService.GetSystemSettingsAsync("TRANSFER_REQUEST");

            String ViewName = "FundTransferRequest";
            String Subject = "New Fund Transfer Request";

            try
            {
                var Message = await _TemplateService.RenderTemplateAsync(ViewName, DataModel);

                await SendEmailAsync(Settings.NotificationToEmail, Subject, Message,
                                     Settings.NotificationCCEmail);

                return "Email Sent Successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending fund transfer request email to {0}",
                                 Settings.NotificationToEmail);

                return ex.Message;
            }
        }

        public async Task<string> SendInitialInvestmentReceivedToCustomer(User DataModel)
        {
            SystemSettings Settings = await _settingsService.GetSystemSettingsAsync("INITIAL_INVEST_REQUEST");

            String ViewName = "AfterInvestmentCustomer";
            String Subject = "E- Transaction (Online Digital Account)";

            try
            {
                var Message = await _TemplateService.RenderTemplateAsync(ViewName, DataModel);


                await SendEmailAsync(DataModel.Email, Subject, Message, Settings.NotificationToEmail,
                                     Settings.NotificationCCEmail);

                return "Email Sent Successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending user application completion email to {0}",
                                 Settings.NotificationToEmail);

                return ex.Message;
            }
        }

        public async Task<string> SendInitialInvestmentRejectedToCustomer(User DataModel)
        {
            SystemSettings Settings = await _settingsService.GetSystemSettingsAsync("INITIAL_INVEST_REJECT");

            String ViewName = "InitialInvestmentRejectedCustomer";
            String Subject = "E- Transaction (Online Digital Account)";

            try
            {
                var Message = await _TemplateService.RenderTemplateAsync(ViewName, DataModel);


                await SendEmailAsync(DataModel.Email, Subject, Message, Settings.NotificationToEmail,
                                     Settings.NotificationCCEmail);

                return "Email Sent Successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending user application completion email to {0}",
                                 Settings.NotificationToEmail);

                return ex.Message;
            }
        }

        public async Task SendEmailAsync(string email, string subject, string message,
            string CC = "", string BCC = "", List<Attachment> Attachments = null)
        {
            try
            {
                var mimeMessage = new MimeMessage();

                mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.Sender));

                mimeMessage.To.Add(MailboxAddress.Parse(email));

                if (!string.IsNullOrEmpty(CC))
                    mimeMessage.Cc.Add(MailboxAddress.Parse(CC));


                if (!string.IsNullOrEmpty(BCC))
                    mimeMessage.Bcc.Add(MailboxAddress.Parse(BCC));

                mimeMessage.Subject = subject;

                if (Attachments != null)
                {
                    // now create the multipart/mixed container to hold the message text and the
                    // attachments
                    var multipart = new Multipart("mixed");

                    var body = new TextPart("html")
                    {
                        Text = message
                    };

                    multipart.Add(body);

                    for (int i = 0; i < Attachments.Count; i++)
                    {
                        // create an image attachment for the file located at path
                        var attachment = new MimePart(Attachments[i].Type, Attachments[i].Extension)
                        {
                            Content = new MimeContent(
                                new MemoryStream(Convert.FromBase64String(Attachments[i].Base64Content)),
                                                      ContentEncoding.Default),
                            FileName = Attachments[i].Filename,
                            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                            ContentTransferEncoding = ContentEncoding.Base64
                        };

                        multipart.Add(attachment);
                    }

                    mimeMessage.Body = multipart;
                }
                else
                    mimeMessage.Body = new TextPart("html")
                    {
                        Text = message
                    };

                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    if (_emailSettings.UseTLS)
                        await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort,
                                                  MailKit.Security.SecureSocketOptions.StartTls);
                    else
                        await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort,
                                                  _emailSettings.UseSSL);

                    if (_emailSettings.AuthRequired)
                    {
                        // Note: since we don't have an OAuth2 token, disable
                        // the XOAUTH2 authentication mechanism.
                        client.AuthenticationMechanisms.Remove("XOAUTH2");
                        client.Authenticate(_emailSettings.Username, _emailSettings.Password);
                    }

                    await client.SendAsync(mimeMessage);

                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<string> SendOTPEmail(User User)
        {
            //SystemSettings Settings = await _settingsService.GetSystemSettingsAsync();

            String ViewName = "OTP";
            String Subject = "OTP";

            try
            {
                var Message = await _TemplateService.RenderTemplateAsync(ViewName, User);


                await SendEmailAsync(User.Email, Subject, Message,
                                     "", "");

                return "Email Sent Successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending OTP Email to {0}",
                                 User.Email);

                return ex.Message;
            }
        }

        private Attachment ConvertBase64StringToAttachment(string Base64String, string Filename)
        {
            var ImageArray = Base64String.Split(",");
            var Extension = ImageArray[0].Split(";")[0].Split("/")[1];

            return new Attachment
            {
                Type = ImageArray[0].Split(";")[0].Split("/")[0],
                Extension = Extension,
                Filename = Filename + "." + Extension,
                Base64Content = ImageArray[1]
            };
        }

    }
}