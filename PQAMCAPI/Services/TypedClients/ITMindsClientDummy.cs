﻿using Classes;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using PQAMCAPI.Models;
using PQAMCClasses.CloudDTOs;
using System.Security.Cryptography;
using System.Text;

namespace PQAMCAPI.Services.TypedClients
{
    public class ITMindsClientDummy : IITMindsClient
    {
        private HttpClient _client;
        private HttpClientHelper _clientHelper;
        private readonly ITMindsSettings _settings;
        private readonly ILogger _logger;
        private string Path = @"D:\Work\CompassDX\PakQatar\PQ AMC\Documents\API Responses\";

        public ITMindsClientDummy(HttpClient client, IHttpClientFactory httpClientFactory, IOptions<ITMindsSettings> settings, ILogger<ITMindsClient> logger)
        {
            _client = client;
            _clientHelper = new HttpClientHelper();
            _settings = settings.Value;
            _logger = logger;
        }

        private byte[] GetKey(string ProvidedKey)
        {
            byte[] TempKey;
            byte[] FinalKey = new byte[16];
            SHA256 SHA = SHA256.Create();
            TempKey = SHA.ComputeHash(Encoding.ASCII.GetBytes(ProvidedKey));
            Array.Copy(TempKey, FinalKey, 16);
            return FinalKey;
        }
        private string GetCredentials()
        {
            CredentialsDTO credentials = new CredentialsDTO();

            credentials.userId = _settings.userId;
            credentials.password = _settings.password;
            credentials.requestTime = DateTime.Now.Millisecond.ToString();
            credentials.brandName = _settings.brandName;
            credentials.channel = _settings.channel;

            Aes aes = Aes.Create();
            byte[] textToEncrypt = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(credentials));
            aes.Key = GetKey(_settings.AES);

            byte[] EncryptedText = aes.EncryptEcb(textToEncrypt, PaddingMode.PKCS7);
            string Encoded = Convert.ToBase64String(EncryptedText);
            return Encoded;
        }
        public async Task<GetTokenResponseDTO> GetToken(string Credentials)
        {
            string route = "getToken";
            string URL = _settings.APIUrl + route;
            StreamReader reader = new StreamReader(Path + "GetToken.txt");
            string result = await reader.ReadToEndAsync();

            _logger.LogInformation("Data received from token API: " + result);

            return JsonConvert.DeserializeObject<GetTokenResponseDTO>(result);
        }

        public async Task<GetTokenResponseDTO> RefreshToken()
        {
            string route = "getRefreshToken";
            string URL = _settings.APIUrl + route;
            string Credentials = "woL1jX0lCHMbniLcPBKbMZq0DLP6mnVYvbwB4p/HWgfTLTVQaJEkD6Y2cgA0R2HtB8Iw2VaBp/2+eWDWTN+e/B3UhEog5fGuxnEy0zQEQNrfyR+S63hZAk4tEP0z03rDD+od1zz5Za19CHCGY1xqbzSPYrYakqTo4iwkX3zHE4n1gMfxdCVQxn+HqVf9e2U7";
            GetRefreshTokenRequestDTO RefreshToken = new GetRefreshTokenRequestDTO();
            RefreshToken.token = "woL1jX0lCHMbniLcPBKbMZq0DLP6mnVYvbwB4p/HWgfTLTVQaJEkD6Y2cgA0R2HtB8Iw2VaBp/2+eWDWTN+e/B3UhEog5fGuxnEy0zQEQNrfyR+S63hZAk4tEP0z03rDD+od1zz5Za19CHCGY1xqbzSPYrYakqTo4iwkX3zHE4n1gMfxdCVQxn+HqVf9e2U7";

            StreamReader reader = new StreamReader(Path + "GetRefreshToken.txt");
            string result = await reader.ReadToEndAsync();

            //HttpRequestMessage request = _clientHelper.CreateHttpRequestMessage(HttpMethod.Post, URL, Credentials, JsonConvert.SerializeObject(RefreshToken));
            //HttpResponseMessage ResponseMessage = await _client.SendAsync(request);

            //var result = await ResponseMessage.Content.ReadAsStringAsync();
            _logger.LogInformation("Data received from token API: " + result);

            return JsonConvert.DeserializeObject<GetTokenResponseDTO>(result);
        }
        public async Task<SubmitSaleResponseDTO> SubmitSale(GetTokenResponseDTO TokenDetails, SubmitSaleRequestDTO SaleRequest)
        {
            string route = "submitSahulatWithSale";
            string URL = _settings.APIUrl + route;

            var json = JsonConvert.SerializeObject(SaleRequest);
            HttpRequestMessage request = _clientHelper.CreateHttpRequestMessage(HttpMethod.Post, URL, TokenDetails.Token, json);
            HttpResponseMessage ResponseMessage = await _client.SendAsync(request);

            var result = await ResponseMessage.Content.ReadAsStringAsync();
            _logger.LogInformation("Data received from submitSahulatWithSale API: " + result);

            return JsonConvert.DeserializeObject<SubmitSaleResponseDTO>(result);
        }
        public async Task<GetFolioListResponseDTO> GetFolioList(GetTokenResponseDTO TokenDetails)
        {
            string route = "getFolioList";
            string URL = _settings.APIUrl + route;

            StreamReader reader = new StreamReader(Path + "getFolioList.txt");
            string result = await reader.ReadToEndAsync();

            _logger.LogInformation("Data received from getFolioList API: " + result);

            GetFolioListResponseDTO FolioResponse = JsonConvert.DeserializeObject<GetFolioListResponseDTO>(result);

            return FolioResponse;
        }
        public async Task<GetFundBankAccountsResponseDTO> GetFundBankAccounts(GetTokenResponseDTO TokenDetails, GetFundBankAccountsRequestDTO FundBankAccountsRequest)
        {
            string route = "getFundBankAccounts";
            string URL = _settings.APIUrl + route;

            string requestJSON = JsonConvert.SerializeObject(FundBankAccountsRequest);

            StreamReader reader = new StreamReader(Path + "getFundBankAccounts - Bank List.txt");
            string result = await reader.ReadToEndAsync();

            _logger.LogInformation("Data received from getFundBankAccounts API: " + result);

            GetFundBankAccountsResponseDTO FundBankAccountsResponse = JsonConvert.DeserializeObject<GetFundBankAccountsResponseDTO>(result);

            return FundBankAccountsResponse;
        }
        public async Task<GetAMCFundNAVListResponseDTO> GetAMCFundNAVList(GetTokenResponseDTO TokenDetails)
        {
            string route = "getAMCFundNAVList";
            string URL = _settings.APIUrl + route;

            StreamReader reader = new StreamReader(Path + "getAMCFundNavList.txt");
            string result = await reader.ReadToEndAsync();

            _logger.LogInformation("Data received from getAMCFundNAVList API: " + result);

            GetAMCFundNAVListResponseDTO FundListResponse = JsonConvert.DeserializeObject<GetAMCFundNAVListResponseDTO>(result);

            return FundListResponse;
        }

        public async Task<GetAccountStatementCISResponseDTO> GetAccountStatementCIS(GetTokenResponseDTO TokenDetails, GetAccountStatementCISRequestDTO ActStmtReq)
        {
            string route = "getAccountStatementCIS";
            string URL = _settings.APIUrl + route;

            string requestJSON = JsonConvert.SerializeObject(ActStmtReq);

            StreamReader reader = new StreamReader(Path + "GetAccountStatementCIS.txt");

            string result = await reader.ReadToEndAsync();
            _logger.LogInformation("Data received from getAccountStatementCIS API: " + result);

            GetAccountStatementCISResponseDTO ActStmtResponse = JsonConvert.DeserializeObject<GetAccountStatementCISResponseDTO>(result);

            return ActStmtResponse;
        }

        public async Task<GetBankNamesResponseDTO> GetBanksList(GetTokenResponseDTO TokenDetails)
        {
            string route = "getBankNames";
            string URL = _settings.APIUrl + route;

            StreamReader reader = new StreamReader(Path + "getBankNames.txt");
            string result = await reader.ReadToEndAsync();

            _logger.LogInformation("Data received from getBankNames API: " + result);

            GetBankNamesResponseDTO BankNamesResponse = JsonConvert.DeserializeObject<GetBankNamesResponseDTO>(result);

            return BankNamesResponse;
        }
        public async Task<GetPlanListResponseDTO> GetPlanList(GetTokenResponseDTO TokenDetails, GetPlanListRequestDTO PlanListRequest)
        {
            string route = "getPlanList";
            string URL = _settings.APIUrl + route;

            string requestJSON = JsonConvert.SerializeObject(PlanListRequest);

            StreamReader reader = new StreamReader(Path + "getPlanList.txt");
            string result = await reader.ReadToEndAsync();

            _logger.LogInformation("Data received from getPlanList API: " + result);

            GetPlanListResponseDTO PlanListResponse = JsonConvert.DeserializeObject<GetPlanListResponseDTO>(result);

            return PlanListResponse;
        }
        public async Task<GetAccountStatementReportResponseDTO> GetAccountStatementReport(GetTokenResponseDTO TokenDetails, GetAccountStatementReportRequestDTO Request)
        {
            string route = "getAccountStatementReport";
            string URL = _settings.APIUrl + route;

            string requestJSON = JsonConvert.SerializeObject(Request);

            StreamReader reader = new StreamReader(Path + "getAccountStatementReport.txt");
            string result = await reader.ReadToEndAsync();

            _logger.LogInformation("Data received from getAccountStatementReport API: " + result);

            GetAccountStatementReportResponseDTO Report = JsonConvert.DeserializeObject<GetAccountStatementReportResponseDTO>(result);

            return Report;
        }
        public async Task<GetAccountStatementReportResponseDTO> GetAccountStatementCISVPSReport(GetTokenResponseDTO TokenDetails, GetAccountStatementCISVPSReportRequestDTO Request)
        {
            string route = "getAccountStatementCISVPSReport";
            string URL = _settings.APIUrl + route;

            string requestJSON = JsonConvert.SerializeObject(Request);

            StreamReader reader = new StreamReader(Path + "getAccountStatementCISVPSReport.txt");
            string result = await reader.ReadToEndAsync();
            _logger.LogInformation("Data received from getAccountStatementCISVPSReport API: " + result);

            GetAccountStatementReportResponseDTO Report = JsonConvert.DeserializeObject<GetAccountStatementReportResponseDTO>(result);

            return Report;
        }
        public async Task<GetAccountStatementCISResponseDTO> GetAccountStatementVPS(GetTokenResponseDTO TokenDetails, GetAccountStatementVPSRequestDTO ActStmtReq)
        {
            string route = "getAccountStatementVPS";
            string URL = _settings.APIUrl + route;

            string requestJSON = JsonConvert.SerializeObject(ActStmtReq);
            StreamReader reader = new StreamReader(Path + "GetAccountStatementVPS.txt");

            string result = await reader.ReadToEndAsync();

            _logger.LogInformation("Data received from getAccountStatementVPS API: " + result);

            GetAccountStatementCISResponseDTO ActStmtResponse = JsonConvert.DeserializeObject<GetAccountStatementCISResponseDTO>(result);

            return ActStmtResponse;
        }

        public async Task<SubmitDigitalAccountResponseDTO> SubmitDigitalAccountRequest(GetTokenResponseDTO TokenDetails, SubmitDigitalAccountRequestDTO DigitalAccountRequest)
        {
            string route = "submitDigitalAccount";
            string URL = _settings.APIUrl + route;

            string requestJSON = JsonConvert.SerializeObject(DigitalAccountRequest);

            HttpRequestMessage request = _clientHelper.CreateHttpRequestMessage(HttpMethod.Post, URL, TokenDetails.Token, JsonConvert.SerializeObject(DigitalAccountRequest));
            HttpResponseMessage ResponseMessage = await _client.SendAsync(request);

            var result = await ResponseMessage.Content.ReadAsStringAsync();
            _logger.LogInformation("Data received from submitDigitalAccount API: " + result);

            SubmitDigitalAccountResponseDTO Response = JsonConvert.DeserializeObject<SubmitDigitalAccountResponseDTO>(result);

            return Response;
        }

        public async Task<SubmitResponseDTO> SubmitRedemptionRequest(GetTokenResponseDTO TokenDetails, SubmitRedemptionRequestDTO SubmitRedemptionRequest)
        {
            string route = "submitRedemption";
            string URL = _settings.APIUrl + route;

            string requestJSON = JsonConvert.SerializeObject(SubmitRedemptionRequest);

            HttpRequestMessage request = _clientHelper.CreateHttpRequestMessage(HttpMethod.Post, URL, TokenDetails.Token, JsonConvert.SerializeObject(SubmitRedemptionRequest));
            HttpResponseMessage ResponseMessage = await _client.SendAsync(request);

            var result = await ResponseMessage.Content.ReadAsStringAsync();
            _logger.LogInformation("Data received from submitRedemption API: " + result);

            SubmitResponseDTO Response = JsonConvert.DeserializeObject<SubmitResponseDTO>(result);

            return Response;
        }

        public async Task<SubmitResponseDTO> SubmitConversionRequest(GetTokenResponseDTO TokenDetails, SubmitConversionRequestDTO SubmitConversionRequest)
        {
            string route = "submitConversion";
            string URL = _settings.APIUrl + route;

            string requestJSON = JsonConvert.SerializeObject(SubmitConversionRequest);

            StreamReader reader = new StreamReader(Path + "submitConversion.txt");
            string result = await reader.ReadToEndAsync();

            //HttpRequestMessage request = _clientHelper.CreateHttpRequestMessage(HttpMethod.Post, URL, TokenDetails.Token, JsonConvert.SerializeObject(SubmitConversionRequest));
            //HttpResponseMessage ResponseMessage = await _client.SendAsync(request);

            //var result = await ResponseMessage.Content.ReadAsStringAsync();
            _logger.LogInformation("Data received from submitConversion API: " + result);

            SubmitResponseDTO Response = JsonConvert.DeserializeObject<SubmitResponseDTO>(result);

            return Response;
        }

        public async Task<SubmitResponseDTO> SubmitSubsaleRequest(GetTokenResponseDTO TokenDetails, SubmitSubSaleRequestDTO SubmitSubsaleRequest)
        {
            string route = "submitSubSale";
            string URL = _settings.APIUrl + route;

            string requestJSON = JsonConvert.SerializeObject(SubmitSubsaleRequest);

            StreamReader reader = new StreamReader(Path + "SubmitSubSaleResponse.txt");
            string result = await reader.ReadToEndAsync();

            _logger.LogInformation("Data received from submitSubSale API: " + result);

            SubmitResponseDTO Response = JsonConvert.DeserializeObject<SubmitResponseDTO>(result);

            return Response;
        }
        public async Task<GetCISFundListResponseDTO> GetCISFundList(GetTokenResponseDTO TokenDetails, GetCISFundListRequestDTO CISFundListRequest)
        {
            string route = "getCISFundList";
            string URL = _settings.APIUrl + route;

            string requestJSON = JsonConvert.SerializeObject(CISFundListRequest);

            HttpRequestMessage request = _clientHelper.CreateHttpRequestMessage(HttpMethod.Post, URL, TokenDetails.Token, JsonConvert.SerializeObject(CISFundListRequest));
            HttpResponseMessage ResponseMessage = await _client.SendAsync(request);

            var result = await ResponseMessage.Content.ReadAsStringAsync();
            _logger.LogInformation("Data received from getCISFundList API: " + result);

            GetCISFundListResponseDTO Response = JsonConvert.DeserializeObject<GetCISFundListResponseDTO>(result);

            return Response;
        }
        public async Task<GetVPSPlanSequenceResponseDTO> GetVPSPlanSequence(GetTokenResponseDTO TokenDetails)
        {
            string route = "getVPSPlanSequence";
            string URL = _settings.APIUrl + route;

            HttpRequestMessage request = _clientHelper.CreateHttpRequestMessage(HttpMethod.Get, URL, TokenDetails.Token);
            HttpResponseMessage ResponseMessage = await _client.SendAsync(request);

            var result = await ResponseMessage.Content.ReadAsStringAsync();
            _logger.LogInformation("Data received from getVPSPlanSequence API: " + result);

            GetVPSPlanSequenceResponseDTO VPSPlanSequenceResponse = JsonConvert.DeserializeObject<GetVPSPlanSequenceResponseDTO>(result);

            return VPSPlanSequenceResponse;
        }

        public async Task<GetFundNAVResponseDTO> getFundNAV(GetTokenResponseDTO TokenDetails, GetFundNAVRequestDTO FundNAVRequestDTO)
        {
            string route = "getFundNAV";
            string URL = _settings.APIUrl + route;

            string requestJSON = JsonConvert.SerializeObject(FundNAVRequestDTO);

            string fileName = Path + "getFundNAV - " + FundNAVRequestDTO.fundShortName + ".txt";
            StreamReader reader = new StreamReader(fileName);
            string result = await reader.ReadToEndAsync();
            _logger.LogInformation("Data received from getFundNAV API: " + result);

            GetFundNAVResponseDTO FundListResponse = JsonConvert.DeserializeObject<GetFundNAVResponseDTO>(result);
            reader.Close();
            return FundListResponse;
        }

        public async Task<GetFolioBankListResponseDTO> GetFolioBankList(GetTokenResponseDTO TokenDetails, GetFolioBankListRequestDTO GetFolioBankListRequestDTO)
        {
            string route = "getFolioBankList";
            string URL = _settings.APIUrl + route;

            string requestJSON = JsonConvert.SerializeObject(GetFolioBankListRequestDTO);

            StreamReader reader = new StreamReader(Path + "getFolioBankList.txt");
            string result = await reader.ReadToEndAsync();

            _logger.LogInformation("Data received from getFolioBankList API: " + result);

            GetFolioBankListResponseDTO Response = JsonConvert.DeserializeObject<GetFolioBankListResponseDTO>(result);

            return Response;
        }

        //public async Task<GetAccountStatementReportResponseDTO> GetAccountStatementReportVPS(GetTokenResponseDTO TokenDetails, GetAccountStatementReportVPSRequestDTO Request)
        //{
        //    string route = "getAccountStatementReportVPS";
        //    string URL = _settings.APIUrl + route;

        //    string requestJSON = JsonConvert.SerializeObject(Request);

        //    HttpRequestMessage request = _clientHelper.CreateHttpRequestMessage(HttpMethod.Post, URL, TokenDetails.Token, JsonConvert.SerializeObject(Request));
        //    HttpResponseMessage ResponseMessage = await _client.SendAsync(request);

        //    var result = await ResponseMessage.Content.ReadAsStringAsync();
        //    _logger.LogInformation("Data received from getAccountStatementReportVPS API: " + result);

        //    GetAccountStatementReportResponseDTO Report = JsonConvert.DeserializeObject<GetAccountStatementReportResponseDTO>(result);

        //    return Report;
        //}
    
    }
}
