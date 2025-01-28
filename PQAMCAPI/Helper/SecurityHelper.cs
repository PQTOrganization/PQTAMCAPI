using API.Classes;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using PQAMCClasses;
using PQAMCClasses.DTOs;
using System;
using System.Security.Claims;
using System.Text;
using static PQAMCClasses.Globals;

namespace Helper
{
    public static class SecurityHelper
    {
        public static string[] UnAuthorizedControllers = new string[]
                            {
                              "user",
                              "adminlogin",
                              "accountcategory",
                              "annualincome",
                              "list"
                            };

        public static string[] FolioKeyMethods = new string[]
                            {
                              "/api/dashboard/summarycloud",
                              "/api/dashboard/investmentsummary",
                              "/api/dashboard/investmentsummarycloud",
                              "/api/dashboard/categorywisebreakup",
                              "/api/dashboard/categorywisebreakupcloud",
                              "/api/dashboard/riskwisebreakup",
                              "/api/dashboard/riskWisebreakupcloud",
                              "/api/dashboard/fundwisesummary",
                              "/api/dashboard/fundwisesummarycloud",
                              //"/api/fund/cloud",
                              //"/api/fund/banks/cloud",
                              "/api/investment/fundwiseposition",
                              "/api/investment/fundwiseposition/cloud",
                              "/api/investment/allfundwiseposition",
                              "/api/investment/allfundwiseposition/cloud",
                              "/api/investmentrequest/forfolio",
                              "/api/investmentrequest/testsubmitsubsale",
                              "/api/redemptionrequest/forfolio",
                              "/api/transactions/forfolio",
                              "/api/transactions/forfolio/cloud",
                              "/api/transactions/pending/forfolio",
                            };

        public static string[] UserAppKeyMethod = new string[]
                            {
                              "/api/userapplication",
                            };
        public static bool IsUnauthorizedController(string ControllerName)
        {
            return UnAuthorizedControllers.Contains(ControllerName.ToLower());
        }

        public static bool IsKeyMethod(string MethodName)
        {            
            return FolioKeyMethods.Contains(MethodName.ToLower());
        }

        public static bool IsUserAppKeyMethod(string MethodName)
        {
            return UserAppKeyMethod.Contains(MethodName.ToLower());
        }
        public static async Task<bool> VerifyContext(HttpContext CurrentContext)
        {
            Claim? CSessionKeys = CurrentContext.User.FindFirst(Globals.SessionKeys.SecurityKeys);

            if (CSessionKeys != null)
            {
                SessionSecurityKeys SecurityKeys = JsonConvert.DeserializeObject<SessionSecurityKeys>(CSessionKeys.Value.ToString());
                if (SecurityKeys.IsAdmin)
                    return true;
            }

            string[] Path = CurrentContext.Request.Path.ToString().Split("//".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            if (!IsUnauthorizedController(Path[1]))
            {
                string MethodName = CurrentContext.Request.Path.ToString().Substring(0, CurrentContext.Request.Path.ToString().LastIndexOf("/"));

                if (CurrentContext.Request.Method == "GET")
                {
                    if (IsKeyMethod(MethodName))
                    {                        
                        if (CSessionKeys != null)
                        {
                            SessionSecurityKeys SecurityKeys = JsonConvert.DeserializeObject<SessionSecurityKeys>(CSessionKeys.Value.ToString());
                            if (!SecurityKeys.IsAdmin)
                            {
                                bool Found = false;
                                for (int i = 0; i < SecurityKeys.FolioList.Count; i++)
                                {
                                    if (Path[Path.Length - 1] == SecurityKeys.FolioList[i].FolioNumber)
                                    {
                                        Found = true;
                                    }
                                }

                                return Found;
                            }
                        }
                    }
                    else if (IsUserAppKeyMethod(MethodName))
                    {                     
                        if (CSessionKeys != null)
                        {
                            SessionSecurityKeys SecurityKeys = JsonConvert.DeserializeObject<SessionSecurityKeys>(CSessionKeys.Value.ToString());
                            if (!SecurityKeys.IsAdmin)
                            {
                                bool Found = false;
                                if (Path[Path.Length - 1] == SecurityKeys.UserApplicationID)
                                {
                                    Found = true;
                                }


                                return Found;
                            }
                        }
                    }
                }
                //else if (CurrentContext.Request.Method == "POST")
                //{
                //    var request = CurrentContext.Request;
                //    if (Path[2].ToLower() == "redemptionrequest")
                //    {

                //        request.EnableBuffering();
                //        var buffer = new byte[Convert.ToInt32(request.ContentLength)];
                //        await request.Body.ReadAsync(buffer, 0, buffer.Length);
                //        var requestContent = Encoding.UTF8.GetString(buffer);

                //        request.Body.Position = 0;  //rewinding the stream to 0                        

                //        //JsonSerializerSettings
                //        RedemptionRequestDTO Data = JsonConvert.DeserializeObject<RedemptionRequestDTO>(requestContent,
                //            new JsonSerializerSettings
                //            {
                //                Error = static delegate (object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
                //                {
                //                    args.ErrorContext.Handled = true;
                //                }
                //            });

                //        Claim? CSessionKeys = CurrentContext.User.FindFirst(SessionKeys.SecurityKeys);
                //        if (CSessionKeys != null)
                //        {
                //            SessionSecurityKeys SecurityKeys = JsonConvert.DeserializeObject<SessionSecurityKeys>(CSessionKeys.Value.ToString());

                //            if (!SecurityKeys.IsAdmin)
                //            {
                //                bool Found = false;
                //                for (int i = 0; i < SecurityKeys.FolioList.Count; i++)
                //                {
                //                    if (Data.FolioNumber == SecurityKeys.FolioList[i].FolioNumber)
                //                    {
                //                        Found = true;
                //                    }
                //                }

                //                return Found;
                //            }
                //        }
                //    }
                //    else if (Path[2].ToLower() == "investmentrequest")
                //    {
                //        if (Path[3].ToLower() == "initial") 
                //        {
                //            request.EnableBuffering();
                //            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
                //            await request.Body.ReadAsync(buffer, 0, buffer.Length);
                //            var requestContent = Encoding.UTF8.GetString(buffer);

                //            request.Body.Position = 0;  //rewinding the stream to 0                        

                //            InitialInvestmentRequestDTO Data = JsonConvert.DeserializeObject<InitialInvestmentRequestDTO>(requestContent,
                //                new JsonSerializerSettings
                //                {
                //                    Error = static delegate (object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
                //                    {
                //                        args.ErrorContext.Handled = true;
                //                    }
                //                });

                //            Claim? CSessionKeys = CurrentContext.User.FindFirst(SessionKeys.SecurityKeys);
                //            if (CSessionKeys != null)
                //            {
                //                SessionSecurityKeys SecurityKeys = JsonConvert.DeserializeObject<SessionSecurityKeys>(CSessionKeys.Value.ToString());

                //                if (!SecurityKeys.IsAdmin)
                //                {
                //                    bool Found = false;

                //                    if (Data.UserId == SecurityKeys.UserId &&
                //                        Data.CNIC == SecurityKeys.CNIC)
                //                    {
                //                        Found = true;
                //                    }
                                    
                //                    return Found;
                //                }
                //            }
                //        }
                //        else
                //        {
                //            request.EnableBuffering();
                //            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
                //            await request.Body.ReadAsync(buffer, 0, buffer.Length);
                //            var requestContent = Encoding.UTF8.GetString(buffer);

                //            request.Body.Position = 0;  //rewinding the stream to 0                        

                //            InvestmentRequestDTO Data = JsonConvert.DeserializeObject<InvestmentRequestDTO>(requestContent,
                //                new JsonSerializerSettings
                //                {
                //                    Error = static delegate (object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
                //                    {
                //                        args.ErrorContext.Handled = true;
                //                    }
                //                });

                //            Claim? CSessionKeys = CurrentContext.User.FindFirst(SessionKeys.SecurityKeys);
                //            if (CSessionKeys != null)
                //            {
                //                SessionSecurityKeys SecurityKeys = JsonConvert.DeserializeObject<SessionSecurityKeys>(CSessionKeys.Value.ToString());

                //                if (!SecurityKeys.IsAdmin)
                //                {
                //                    bool Found = false;
                //                    for (int i = 0; i < SecurityKeys.FolioList.Count; i++)
                //                    {
                //                        if (Data.FolioNumber == SecurityKeys.FolioList[i].FolioNumber &&
                //                            Data.UserId == SecurityKeys.UserId &&
                //                            Data.CNIC == SecurityKeys.CNIC)
                //                        {
                //                            Found = true;
                //                        }
                //                    }

                //                    return Found;
                //                }
                //            }
                //        }
                //    }
                //    else if (Path[2].ToLower() == "fundtransferrequest")
                //    {

                //        request.EnableBuffering();
                //        var buffer = new byte[Convert.ToInt32(request.ContentLength)];
                //        await request.Body.ReadAsync(buffer, 0, buffer.Length);
                //        var requestContent = Encoding.UTF8.GetString(buffer);

                //        request.Body.Position = 0;  //rewinding the stream to 0                        

                //        //JsonSerializerSettings
                //        FundTransferRequestDTO Data = JsonConvert.DeserializeObject<FundTransferRequestDTO>(requestContent,
                //            new JsonSerializerSettings
                //            {
                //                Error = static delegate (object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
                //                {
                //                    args.ErrorContext.Handled = true;
                //                }
                //            });

                //        Claim? CSessionKeys = CurrentContext.User.FindFirst(SessionKeys.SecurityKeys);
                //        if (CSessionKeys != null)
                //        {
                //            SessionSecurityKeys SecurityKeys = JsonConvert.DeserializeObject<SessionSecurityKeys>(CSessionKeys.Value.ToString());

                //            if (!SecurityKeys.IsAdmin)
                //            {
                //                bool Found = false;
                //                for (int i = 0; i < SecurityKeys.FolioList.Count; i++)
                //                {
                //                    if (Data.FolioNumber == SecurityKeys.FolioList[i].FolioNumber)
                //                    {
                //                        Found = true;
                //                    }
                //                }

                //                return Found;
                //            }
                //        }
                //    }
                //}
            } 

            return true;
        }

    }
}