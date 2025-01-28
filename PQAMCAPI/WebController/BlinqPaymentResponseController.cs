using Microsoft.AspNetCore.Mvc;
using static PQAMCClasses.Globals;
using System.Security.Cryptography;
using System.Web;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Text;
using Newtonsoft.Json;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.WebController
{
    public class BlinqPaymentResponseController : Microsoft.AspNetCore.Mvc.Controller   
    {
        private readonly IInvestmentRequestService _service;
        private readonly ILogger _logger;
        
        private const string ClientSecret = "HvefOMHjq0emOA8";

        private string WebAppURL = "";

        public BlinqPaymentResponseController(IInvestmentRequestService service, ILogger<BlinqPaymentResponseController> logger, 
                                              IConfiguration configuration)
        {
            _service = service;
            _logger = logger;
            WebAppURL = configuration.GetValue("WebAppUrl", "");
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] IFormCollection FormData)
        {
            string Status = "";
            string Message = "";
            string EncryptedFormData = "";
            string OrdId = "";
            string PaymentCode = "";
            string PaymentId = "";

            string[] formColumns = FormData.Keys.ToArray();

            string ResponseJSON = JsonConvert.SerializeObject(FormData.ToArray());
            _logger.LogInformation("Data received from Blinq: {0}", ResponseJSON);


            //status, message, amountPaid, encryptedFormData, netAmount, ordId, paidOn, paymentCode
            //paymentVia, pBank, refNumber, txnFee           

            foreach (string responseColumn in formColumns)
            {
                if (responseColumn.Trim().ToLower().Equals("encryptedformdata"))
                    EncryptedFormData = Request.Form[responseColumn];

                if (responseColumn.Trim().ToLower().Equals("status"))
                    Status = Request.Form[responseColumn];

                if (responseColumn.Trim().ToLower().Equals("message"))
                    Message = Request.Form[responseColumn];

                if (responseColumn.Trim().ToLower().Equals("ordid"))
                    OrdId = Request.Form[responseColumn];

                if (responseColumn.Trim().ToLower().Equals("paymentcode"))
                    PaymentCode = Request.Form[responseColumn];

                if (responseColumn.Trim().ToLower().Equals("paymentid"))
                    PaymentId = Request.Form[responseColumn];
            }

            short PaymentStatus = (Status == "success") ? (short)RequestStatus.Paid : (short)RequestStatus.Cancelled;

            if (!await _service.UpdateInvestmentRequestBlinqResponse(PaymentId, ResponseJSON, PaymentStatus))
                _logger.LogError("Could not update Blinq response in DB");

            if (Status == "success")
            {
                //if (ValidateFormData(EncryptedFormData, Status, OrdId, PaymentCode))
                return Redirect(WebAppURL + "finalizepayment?status=" + Status + "&message=" + Message);
            }
            else
                return Redirect(WebAppURL + "finalizepayment?status=" + Status + "&message=" + Message);

            return View();
        }

        private bool ValidateFormData(string EncryptedFormData, string Status, string OrdId, string PaymentCode)
        {
            if (string.IsNullOrEmpty(EncryptedFormData))
                return false;

            string Stringbeforehash = Status + OrdId + PaymentCode + ClientSecret;

            string Stringafterhash = GetMD5HashData(GetSHA256HashData(Stringbeforehash));

            return Stringafterhash.Equals(EncryptedFormData);
        }

        private string GetSHA256HashData(string data)
        {
            SHA256 sha256 = SHA256.Create();
            //convert the input text to array of bytes
            byte[] hashData = sha256.ComputeHash(Encoding.Default.GetBytes(data));
            //create new instance of StringBuilder to save hashed data
            StringBuilder returnValue = new StringBuilder();
            //loop for each byte and add it to StringBuilder
            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString("x2"));
            }
            // return hexadecimal string
            return returnValue.ToString();
        }

        private string GetMD5HashData(string data)
        {
            //create new instance of md5
            MD5 md5 = MD5.Create();
            //convert the input text to array of bytes
            byte[] hashData = md5.ComputeHash(Encoding.Default.GetBytes(data));
            //create new instance of StringBuilder to save hashed data
            StringBuilder returnValue = new StringBuilder();
            //loop for each byte and add it to StringBuilder
            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString("x2"));
            }
            // return hexadecimal string
            return returnValue.ToString();
        }
    }
}
