using Classes;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Services
{
    public interface ISMSService
    {
        Task SendTestSMS();

        Task SendSMS(string MobileNumber, string Message);
    }

    public class PQSMSResponse 
    {
        public List<PQResponseContent> Response = null!;
    }

    public class PQResponseContent
{
        public string ResponseCode = "";
        public string ResponseMesage = "";
    }

    public class PQSMSGateway : ISMSService
    {
        private readonly SMSSettings _smsSettings;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger _loggger;

        public PQSMSGateway(IOptions<SMSSettings> smsSettings, IHttpClientFactory clientFactory, 
            ILogger<PQSMSGateway> logger)
        {
            _clientFactory = clientFactory;
            _loggger = logger;

            _smsSettings = smsSettings.Value;
        }

        public async Task SendTestSMS()
        {
            await SendSMS(_smsSettings.TestingNumber, "This is a test message from api.");
        }

        public async Task SendSMS(string MobileNumber, string Message)
        {
            if (String.IsNullOrEmpty(MobileNumber))
                throw new Exception("No mobile number was provided.");

            if (String.IsNullOrEmpty(Message))
                throw new Exception("No message was provided.");

            await SendMessageToGateway(MobileNumber, Message);
        }

        private async Task SendMessageToGateway(string MobileNumber, string Message)
        {
            string RequestUrl = "http://172.16.6.19:8080/SMSService.aspx?UserId=" + _smsSettings.UserId + 
                                "&Password=" + _smsSettings.Password + "&MobileNo=" + MobileNumber + 
                                "&Message=" + Message;

            var Request = new HttpRequestMessage(HttpMethod.Get, RequestUrl);

            var client = _clientFactory.CreateClient();
            var Response = await client.SendAsync(Request);

            string ResponseStr = await Response.Content.ReadAsStringAsync();

            if (Response.IsSuccessStatusCode)
            {
                try
                {
                    var ServiceResponse = JsonConvert.DeserializeObject<List<PQResponseContent>>(ResponseStr);

                    if (ServiceResponse != null && ServiceResponse.Count() > 0)
                        if (ServiceResponse[0].ResponseCode != "00")
                            _loggger.LogError("Error sending SMS to {0}. Response: {1}", MobileNumber, ResponseStr);
                        else
                            _loggger.LogInformation("SMS Sent. Response Content {0}", ResponseStr);
                    else
                        _loggger.LogInformation("Error parsing SMS response. Response Content {0}", ResponseStr);
                }
                catch (Exception ex)
                {
                    _loggger.LogError(ex, "Error parsing SMS response. Response: {0}", ResponseStr);
                }
            }
            else
            {
                _loggger.LogError("Error contacting SMS service with Status Code {0} and Content {1}", 
                                  Response.StatusCode, ResponseStr);

                throw new Exception("Error contacting SMS Service.");
            }
        }
    }
}


//<response xmlns:i="http://www.w3.org/2001/XMLSchema-instance">
//<action>sendmessage</action>
//<data>
//<acceptreport>
//<statuscode>0</statuscode>
//<statusmessage>Message accepted for delivery</statusmessage>
//<messageid>379015430</messageid>
//<originator>8622</originator>
//<recipient>923219224205</recipient>
//<messagetype>SMS:TEXT</messagetype>
//<messagedata>rosca testing</messagedata>
//</acceptreport>
//</data>
//</response>