using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using System;
using PQAMCClasses.BlinQ;
using System.Text;
using System.Security.Cryptography;
using PQAMCAPI.Services.TypedClients;
using System.Net;
using System.Security.Policy;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using MailKit.Search;
using Microsoft.AspNetCore.Authorization;

namespace PQAMCAPI.Controller
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "PQAMCAuthScheme")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;

        string CLIENTID = "UMEopMnDkELkxoM";
        string CLIENTSECRET = "NORiNQsgX29uHz2";

        string BlinQURL = "https://staging-ipg.blinq.pk/" + "Payment/PaymentProcess.aspx";
        string ReturnURL = "https://localhost:7051/api/" // "https://pqapp.pakqatar.com.pk/amc-api/"
            + "api/payment/response";

        public PaymentController(HttpClient client, ILogger<PaymentController> logger)
        {
            _client = client;
            _logger = logger;
        }        

        [HttpGet("encrypteddata/{orderid}")]
        public string GetEncrptedData(string orderId)
        {
            //< form action = "https://{BlinQ Base URL}/Payment/PaymentProcess.aspx" method = "post" >
            //    < input type = "hidden" name = "client_id" id = "client_id" value = "0vnmf1T9DCJESiv" />
            //    < input type = "hidden" name = "payment_via" id = "payment_via" value = "BLINQ_VM" />
            //    < input type = "hidden" name = "order_id" id = "order_id" value = "170220211526-09" />
            //    < input type = "hidden" name = "customer_name" id = "customer_name" value = "Abid Zulfiqar" />
            //    < input type = "hidden" name = "customer_email" id = "customer_email" value = "abid.zulfi@domain.com" />
            //    < input type = "hidden" name = "customer_mobile" id = "customer_mobile" value = "03001234567" />
            //    < input type = "hidden" name = "order_amount" id = "order_amount" value = "100.00" />
            //    < input type = "hidden" name = "order_expiry_date_time" id = "order_expiry_date_time" value = "2021-01-
            //    01" />
            //    < input type = "hidden" name = "product_description" id = "product_description" value = "TestInvoice" />
            //    < input type = "hidden" name = "encrypted_form_data" id = "encrypted_form_data"
            //    value = "163155c38965e2c0f55bd76aca14e7cf6" />
            //    < input type = "hidden" name = "return_url" id = "return_url"
            //    value = "{https://www.yourdomain.com/order-confirmation}" />
            //    < button type = "submit" > Pay </ button >
            //</ form >

            var OrderId = orderId; // "PQAMC" + DateTime.Now.ToString("yyyyyMMddhhmmss");

            string userDataPattern = CLIENTID + OrderId + ReturnURL + CLIENTSECRET;

            string encryptedByPakPay = GetMD5HashData(GetSHA256HashData(userDataPattern));

            return encryptedByPakPay;
        }

        [HttpPost("SendPayment")]
        public async Task SendPayment([FromForm] IFormCollection FormData)
        {
            //var CLIENTID = "UMEopMnDkELkxoM";
            //var CLIENTSECRET = "NORiNQsgX29uHz2";

            //var BaseURL = "https://staging-ipg.blinq.pk/";
            //var BlinQURL = BaseURL + "Payment/PaymentProcess.aspx";
            //var ReturnURL = "https://pqapp.pakqatar.com.pk/amc-api/api/payment/response";

            var OrderId = "PQAMC" + DateTime.Now.ToString("yyyyyMMddhhmmss");

            string userDataPattern = CLIENTID + OrderId + ReturnURL + CLIENTSECRET;
            string encryptedByPakPay = GetMD5HashData(GetSHA256HashData(userDataPattern));

            var PostData = new BlinqPostPaymentDTO
            {
                client_id = CLIENTID,
                payment_via = "BLINQ_ACC", //BLINQ_VM, BLINQ_ACC, BLINQ_WALLET
                acc_bank = "ABL",
                order_id = OrderId,
                customer_name = "Waseem Khan",
                customer_email = "wkhan73@gmail.com",
                customer_mobile = "923219224205",
                order_amount = "1000",
                order_expiry_date_time = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd"),
                product_description = "PQAMC Investment Test",
                encrypted_form_data = encryptedByPakPay,
                return_url = ReturnURL,
            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, BlinQURL);                        
            request.Content = new StringContent(JsonConvert.SerializeObject(PostData), Encoding.UTF8, 
                                                "application/json");
           
            HttpResponseMessage ResponseMessage = await _client.SendAsync(request);

            var result = await ResponseMessage.Content.ReadAsStringAsync();
            _logger.LogInformation("Data received from token API: " + result);
        }

        [HttpPost("response")]
        public void ProcessPaymentResponse([FromForm] IFormCollection FormData)
        {
            string[] formColumns = FormData.Keys.ToArray();
            var status = "";
            var message = "";

            foreach (string responseColumn in formColumns)
            {
                if (responseColumn.Trim().ToLower().Equals("status".ToLower()))
                {
                    status = Request.Form[responseColumn];
                }

                if (responseColumn.Trim().ToLower().Equals("message".ToLower()))
                {
                    message = Request.Form[responseColumn];
                }
            }

            _logger.LogInformation("Data received from gateway: " + JsonConvert.SerializeObject(FormData));
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
