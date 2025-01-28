using PQAMCAPI.Interfaces.Services;
using PQAMCClasses.CloudDTOs;
using PQAMCClasses.DTOs;
using System;
using static PQAMCClasses.Globals;

namespace PQAMCAPI.Services.Domain
{
    public class InvestmentService : IInvestmentService
    {
        const string PACKAGE_NAME = "AMC_INVESTMENT_PKG";

        private readonly IStoreProcedureService _spService;
        private readonly ICloudService _cloudService;
        private readonly IFundService _fundService;

        public InvestmentService(IStoreProcedureService spService, ICloudService cloudService, IFundService fundService)
        {
            _spService = spService;
            _cloudService = cloudService;
            _fundService = fundService;
        }

        public async Task<List<FundPositionDTO>> GetFundWisePositionForFolioAsync(
            string FolioNumber)
        {
            var Positions = await _spService.GetAllSP<FundPositionDTO>(PACKAGE_NAME +
                                            ".GET_FUND_WISE_POSITION_FOR_FOLIO", FolioNumber);
            // Remove all funds in the list that now have been sold off.
            return Positions.Where(x => x.NumberOfUnits > 0).ToList();
        }

        public async Task<List<FundPositionDTO>> GetAllFundWisePositionForFolioAsync(
            string FolioNumber)
        {
            var Positions = await _spService.GetAllSP<FundPositionDTO>(PACKAGE_NAME +
                                            ".GET_ALL_FUND_WISE_POSITION_FOR_FOLIO", FolioNumber);
            return Positions;
        }

        public async Task<List<LabelWiseInvestmentDTO>> GetFundRiskWiseSummaryForFolioAsync(
            string FolioNumber)
        {
            var Summary = await _spService.GetAllSP<LabelWiseInvestmentDTO>(PACKAGE_NAME +
                                          ".GET_FUND_RISK_WISE_SUMMARY_FOR_FOLIO", FolioNumber);
            // Remove all funds in the list that now have been sold off.
            return Summary.Where(x => x.Value > 0).ToList();
        }

        public async Task<List<LabelWiseInvestmentDTO>> GetFundCategoryWiseSummaryForFolioAsync(
            string FolioNumber)
        {
            var Summary = await _spService.GetAllSP<LabelWiseInvestmentDTO>(PACKAGE_NAME +
                                      ".GET_FUND_CATEGORY_WISE_SUMMARY_FOR_FOLIO", FolioNumber);
            // Remove all funds in the list that now have been sold off.
            return Summary.Where(x => x.Value > 0).ToList();
        }

        public async Task<List<LabelWiseInvestmentDTO>> GetFundSummaryForFolioAsync(
            string FolioNumber)
        {
            var Summary = await _spService.GetAllSP<LabelWiseInvestmentDTO>(PACKAGE_NAME +
                                      ".GET_FUND_SUMMARY_FOR_FOLIO", FolioNumber);
            // Remove all funds in the list that now have been sold off.
            return Summary.Where(x => x.Value > 0).ToList();
        }

        public async Task<List<LabelWiseInvestmentDTO>> GetFundSummaryForFolioAsyncFromCloudAsync(string FolioNumber)
        {
            List<LabelWiseInvestmentDTO> Summary = new List<LabelWiseInvestmentDTO>();

            List<AMCFundNAVDTO> APIFundList = await _cloudService.GetAMCFundNAVList();

            List<InvestorAccountTransactionDTO> accountStatement = await GetAccountStatementCISFromCloud(FolioNumber);

            List <FundDTO> funds = await _fundService.GetFundsAsync();

            IEnumerable<IGrouping<string, FundDTO>> groupedByCategory = funds.GroupBy(x => x.ITMindsFundID);

            foreach (var fundsGroup in groupedByCategory)
            {
                decimal fundValue = 0;
                List<IEnumerable<InvestorTransactionDTO>> Selected = accountStatement.Select(x => x.InvTranscation.Where(y => y.PlanId == fundsGroup.Key)).ToList();
                FundNAVDTO FundNAV = await _fundService.GetFundNAVAsync(fundsGroup.ElementAt(0).FundId);

                for (int i = 0; i < Selected.Count; i++)
                {
                    if (Selected[i].ToList().Count > 0)
                    {
                        //FundNAVDTO FundNAV = await _cloudService.GetFundNAV(Selected[i].ToList()[0].FundShortName);
                       
                        fundValue += ((decimal)Selected[i].ToList().Sum(x => x.NoOfUnitsDouble)) * decimal.Parse(FundNAV.navPerUnit);
                    }
                }

                Summary.Add(new LabelWiseInvestmentDTO { Label = fundsGroup.First().FundName, Value = fundValue });
            }

            // Remove all funds in the list that now have been sold off.
            return Summary.Where(x => x.Value > 0).ToList();
        }


        public async Task<InvestmentSummaryDTO> GetInvestmentSummaryForFolioAsync(
            string FolioNumber)
        {
            var Summary = await _spService.GetSP<InvestmentSummaryDTO>(PACKAGE_NAME +
                                          ".GET_INVESTMENT_SUMMARY_FOR_FOLIO", FolioNumber);
            return Summary;
        }

        public async Task<InvestmentSummaryDTO> GetInvestmentSummaryForFolioFromCloudAsync(string FolioNumber)
        {
            InvestmentSummaryDTO Summary = new InvestmentSummaryDTO();

            List<InvestorAccountTransactionDTO> accountStatement = await GetAccountStatementCISFromCloud(FolioNumber);

            foreach (InvestorAccountTransactionDTO AcctTranDTO in accountStatement)
            {
                Summary.CurrentValue += (decimal)AcctTranDTO.TotalNavAmount;
                
                foreach (InvestorTransactionDTO SingleTxn in AcctTranDTO.InvTranscation)
                {
                    if (SingleTxn.NoOfUnitsDouble < 0)
                    {
                        Summary.InvestedAmount -= (decimal)SingleTxn.GrossAmountDouble;
                    }
                    else
                    {
                        Summary.InvestedAmount += (decimal)SingleTxn.GrossAmountDouble;
                    }
                }
            }

            Summary.GainLoss = Summary.CurrentValue - Summary.InvestedAmount;

            return Summary;
        }
        public async Task<List<LabelWiseInvestmentDTO>> GetFundCategoryWiseSummaryForFolioFromCloudAsync(string FolioNumber)
        {
            List<LabelWiseInvestmentDTO> Summary = new List<LabelWiseInvestmentDTO>();

            List<AMCFundNAVDTO> APIFundList = await _cloudService.GetAMCFundNAVList();

            List<InvestorAccountTransactionDTO> accountStatement = await GetAccountStatementCISFromCloud(FolioNumber);

            List<FundDTO> funds = await _fundService.GetFundsAsync();

            IEnumerable<IGrouping<int, FundDTO>> groupedByCategory = funds.GroupBy(x => x.CategoryId);

            foreach (var fundsGroup in groupedByCategory)
            {
                decimal fundValue = 0;
                List<string> fundIds = new List<string>();

                foreach (FundDTO fund in fundsGroup)
                {
                    fundIds.Add(fund.ITMindsFundID);
                }

                List<IEnumerable<InvestorTransactionDTO>> Selected = accountStatement.Select(x => x.InvTranscation.Where(y => fundIds.Contains(y.PlanId))).ToList();

                for (int i = 0; i < Selected.Count; i++)
                {
                    if (Selected[i].ToList().Count > 0)
                    {
                        //FundNAVDTO FundNAV = await _cloudService.GetFundNAV(Selected[i].ToList()[0].FundShortName);
                        FundDTO foundFund = funds.Where(x => x.ITMindsFundID == Selected[i].ToList().ElementAt(0).PlanId).FirstOrDefault();
                        if (foundFund != null)
                        {
                            FundNAVDTO FundNAV = await _fundService.GetFundNAVAsync(foundFund.FundId);

                            fundValue += ((decimal)Selected[i].ToList().Sum(x => x.NoOfUnitsDouble)) * decimal.Parse(FundNAV.navPerUnit);
                        }
                    }
                }
                Summary.Add(new LabelWiseInvestmentDTO { Label = fundsGroup.First().CategoryName, Value = fundValue });
            }

            // Remove all funds in the list that now have been sold off.
            return Summary.Where(x => x.Value > 0).ToList();
        }

        public async Task<List<LabelWiseInvestmentDTO>> GetFundRiskWiseSummaryForFolioFromCloudAsync(string FolioNumber)
        {
            List<LabelWiseInvestmentDTO> Summary = new List<LabelWiseInvestmentDTO>();

            List<AMCFundNAVDTO> APIFundList = await _cloudService.GetAMCFundNAVList();

            List<InvestorAccountTransactionDTO> accountStatement = await GetAccountStatementCISFromCloud(FolioNumber);

            List<FundDTO> funds = await _fundService.GetFundsAsync();

            IEnumerable<IGrouping<int, FundDTO>> groupedByCategory = funds.GroupBy(x => x.RiskCategoryId);

            foreach (var fundsGroup in groupedByCategory)
            {
                decimal fundValue = 0;
                List<string> fundIds = new List<string>();

                foreach (FundDTO fund in fundsGroup)
                {
                    fundIds.Add(fund.ITMindsFundID);
                }

                List<IEnumerable<InvestorTransactionDTO>> Selected = accountStatement.Select(x => x.InvTranscation.Where(y => fundIds.Contains(y.PlanId))).ToList();
                for (int i = 0; i < Selected.Count; i++)
                {
                    if (Selected[i].ToList().Count > 0)
                    {
                        //FundNAVDTO FundNAV = await _cloudService.GetFundNAV(Selected[i].ToList()[0].FundShortName);
                        FundDTO foundFund = funds.Where(x => x.ITMindsFundID == Selected[i].ToList().ElementAt(0).PlanId).FirstOrDefault();

                        if (foundFund != null)
                        {
                            FundNAVDTO FundNAV = await _fundService.GetFundNAVAsync(foundFund.FundId);
                            fundValue += ((decimal)Selected[i].ToList().Sum(x => x.NoOfUnitsDouble)) * decimal.Parse(FundNAV.navPerUnit);
                        }
                    }
                }
                Summary.Add(new LabelWiseInvestmentDTO { Label = fundsGroup.First().RiskCategoryName, Value = fundValue });
            }

            // Remove all funds in the list that now have been sold off.
            return Summary.Where(x => x.Value > 0).ToList();
        }

        public async Task<List<FundPositionDTO>> GetFundWisePositionForFolioFromCloudAsync(string FolioNumber)
        {
            List<FundPositionDTO> Positions = new List<FundPositionDTO>();

            List<AMCFundNAVDTO> AMCFundList = await _cloudService.GetAMCFundNAVList();
            List<FundDTO> FundsDB = await _fundService.GetFundsAsync();

            List<InvestorAccountTransactionDTO> accountStatement = await GetAccountStatementCISFromCloud(FolioNumber);

            foreach (AMCFundNAVDTO Fund in AMCFundList)
            {
                FundPositionDTO FundPosition = new FundPositionDTO();
                FundPosition.FundName = Fund.FundName;
                decimal GrossAmount = 0;

                List<InvestorAccountTransactionDTO> FundSelected = accountStatement.Where(x => x.FundName == Fund.FundName).ToList();

                FundDTO FundFound = FundsDB.Where(x => x.ITMindsFundShortName == Fund.FundShortName).FirstOrDefault();

                if (FundFound != null)
                {
                    FundNAVDTO FundNAV = await _fundService.GetFundNAVAsync(FundFound.FundId);
                    FundPosition.FundId = FundFound.FundId;
                    decimal InvestedAmount = 0;

                    foreach (InvestorAccountTransactionDTO AcctTranDTO in FundSelected)
                    {
                        FundPosition.CurrentValue += (decimal)AcctTranDTO.TotalNavAmount;   
                        FundPosition.NumberOfUnits += (decimal)AcctTranDTO.InvTranscation.Sum(y => y.NoOfUnitsDouble);
                        //GrossAmount += (decimal)AcctTranDTO.InvTranscation.Sum(y => y.GrossAmountDouble);

                        foreach (InvestorTransactionDTO SingleTxn in AcctTranDTO.InvTranscation)
                        {
                            if (Constants.InvestmentAmountAddTxnList.Contains(SingleTxn.Transaction))
                            {
                                InvestedAmount += (decimal)SingleTxn.GrossAmountDouble;
                            }
                            else if (Constants.InvestmentAmountSubtractTxnList.Contains(SingleTxn.Transaction))
                            {
                                InvestedAmount -= (decimal)SingleTxn.GrossAmountDouble;
                            }
                        }
                    }

                    //FundPosition.ProfitLoss = (FundPosition.NumberOfUnits * decimal.Parse(FundNAV.navPerUnit)) - GrossAmount;
                    //FundPosition.CurrentValue = FundPosition.NumberOfUnits * decimal.Parse(FundNAV.navPerUnit);

                    FundPosition.ProfitLoss = FundPosition.CurrentValue - InvestedAmount;

                    FundPosition.LastNav = decimal.Parse(FundNAV.navPerUnit);
                    FundPosition.OfferNav = decimal.Parse(FundNAV.navPerUnit);

                    Positions.Add(FundPosition);
                }
            }

            // Remove all funds in the list that now have been sold off.
            return Positions.Where(x => x.NumberOfUnits > 0).ToList();
        }

        public async Task<List<FundPositionDTO>> GetAllFundWisePositionForFolioFromCloudAsync(string FolioNumber)
        {
            List<FundPositionDTO> Positions = new List<FundPositionDTO>();

            List<AMCFundNAVDTO> AMCFundList = await _cloudService.GetAMCFundNAVList();
            List<FundDTO> FundsDB = await _fundService.GetFundsAsync();

            List<InvestorAccountTransactionDTO> accountStatement = await GetAccountStatementCISFromCloud(FolioNumber);

            foreach (AMCFundNAVDTO Fund in AMCFundList)
            {
                FundPositionDTO FundPosition = new FundPositionDTO();
                FundPosition.FundName = Fund.FundName;
                decimal GrossAmount = 0;

                //FundNAVDTO FundNAV = await _cloudService.GetFundNAV(Fund.FundShortName);

                List<InvestorAccountTransactionDTO> FundSelected = accountStatement.Where(x => x.FundName == Fund.FundName).ToList();

                FundDTO FundFound = FundsDB.Where(x => x.ITMindsFundShortName == Fund.FundShortName).FirstOrDefault();

                if (FundFound != null)
                {
                    FundNAVDTO FundNAV = await _fundService.GetFundNAVAsync(FundFound.FundId);

                    FundPosition.FundId = FundFound.FundId;
                    foreach (InvestorAccountTransactionDTO AcctTranDTO in FundSelected)
                    {
                        FundPosition.NumberOfUnits += (decimal)AcctTranDTO.InvTranscation.Sum(y => y.NoOfUnitsDouble);
                        GrossAmount += (decimal)AcctTranDTO.InvTranscation.Sum(y => y.GrossAmountDouble);
                    }

                    FundPosition.ProfitLoss = (FundPosition.NumberOfUnits * decimal.Parse(FundNAV.navPerUnit)) - GrossAmount;
                    FundPosition.CurrentValue = FundPosition.NumberOfUnits * decimal.Parse(FundNAV.navPerUnit);
                    FundPosition.LastNav = decimal.Parse(FundNAV.navPerUnit);
                    FundPosition.OfferNav = decimal.Parse(FundNAV.navPerUnit);

                    Positions.Add(FundPosition);
                }
            }

            return Positions;
        }

        private async Task<List<InvestorAccountTransactionDTO>> GetAccountStatementCISFromCloud(string FolioNumber)
        {
            GetAccountStatementCISRequestDTO request = new GetAccountStatementCISRequestDTO();
            request.folioNo = PQAMCClasses.Globals.GetFolioNumber(FolioNumber);
            request.statementType = "T";
            request.fundPlanId = "*ALL";
            request.fromDate = "01/01/2022"; // + DateTime.Now.Year.ToString();
            request.toDate = DateTime.Now.Date.ToString("dd/MM/yyyy");

            return await _cloudService.GetAccountStatementCIS(request);

        }

        private async Task<List<LabelWiseInvestmentDTO>> GetCategoryWiseBreakupCloud(List<InvestorAccountTransactionDTO> AccountStatement, List<FundDTO> funds)
        {
            List<LabelWiseInvestmentDTO> Summary = new List<LabelWiseInvestmentDTO>();
            
            IEnumerable<IGrouping<int, FundDTO>> groupedByCategory = funds.GroupBy(x => x.CategoryId);

            foreach (var fundsGroup in groupedByCategory)
            {
                decimal fundValue = 0;
                List<string> fundIds = new List<string>();

                foreach (FundDTO fund in fundsGroup)
                {
                    fundIds.Add(fund.ITMindsFundID);
                }

                List<IEnumerable<InvestorTransactionDTO>> Selected = AccountStatement.Select(x => x.InvTranscation.Where(y => fundIds.Contains(y.PlanId))).ToList();

                for (int i = 0; i < Selected.Count; i++)
                {
                    if (Selected[i].ToList().Count > 0)
                    {
                        FundDTO foundFund = funds.Where(x => x.ITMindsFundID == Selected[i].ToList().ElementAt(0).PlanId).FirstOrDefault();
                        if (foundFund != null)
                        {
                            FundNAVDTO FundNAV = await _fundService.GetFundNAVAsync(foundFund.FundId);

                            fundValue += ((decimal)Selected[i].ToList().Sum(x => x.NoOfUnitsDouble)) * decimal.Parse(FundNAV.navPerUnit);
                        }
                    }
                }
                Summary.Add(new LabelWiseInvestmentDTO { Label = fundsGroup.First().CategoryName, Value = fundValue });
            }

            // Remove all funds in the list that now have been sold off.
            return Summary.Where(x => x.Value > 0).ToList();
        }

        //private async Task<InvestmentSummaryDTO> GetInvestmentSummary(List<InvestorAccountTransactionDTO> AccountStatement)
        //{
        //    InvestmentSummaryDTO Summary = new InvestmentSummaryDTO();

        //    foreach (InvestorAccountTransactionDTO AcctTranDTO in AccountStatement)
        //    {
        //        Summary.CurrentValue += (decimal)AcctTranDTO.TotalNavAmount;

        //        foreach (InvestorTransactionDTO SingleTxn in AcctTranDTO.InvTranscation)
        //        {
        //            if (SingleTxn.NoOfUnitsDouble < 0)
        //            {
        //                Summary.InvestedAmount -= (decimal)SingleTxn.GrossAmountDouble;
        //            }
        //            else
        //            {
        //                Summary.InvestedAmount += (decimal)SingleTxn.GrossAmountDouble;
        //            }
        //        }
        //    }

        //    Summary.GainLoss = Summary.CurrentValue - Summary.InvestedAmount;

        //    return Summary;
        //}

        private async Task<List<LabelWiseInvestmentDTO>> GetFundWiseSummary(List<InvestorAccountTransactionDTO> AccountStatement, List<FundDTO> funds)
        {
            List<LabelWiseInvestmentDTO> Summary = new List<LabelWiseInvestmentDTO>();
           
            IEnumerable<IGrouping<string, FundDTO>> groupedByCategory = funds.GroupBy(x => x.ITMindsFundID);

            foreach (var fundsGroup in groupedByCategory)
            {
                decimal fundValue = 0;
                List<IEnumerable<InvestorTransactionDTO>> Selected = AccountStatement.Select(x => x.InvTranscation.Where(y => y.PlanId == fundsGroup.Key)).ToList();
                FundNAVDTO FundNAV = await _fundService.GetFundNAVAsync(fundsGroup.ElementAt(0).FundId);

                for (int i = 0; i < Selected.Count; i++)
                {
                    if (Selected[i].ToList().Count > 0)
                    {           
                        fundValue += ((decimal)Selected[i].ToList().Sum(x => x.NoOfUnitsDouble)) * decimal.Parse(FundNAV.navPerUnit);
                    }
                }

                Summary.Add(new LabelWiseInvestmentDTO { Label = fundsGroup.First().FundName, Value = fundValue });
            }

            // Remove all funds in the list that now have been sold off.
            return Summary.Where(x => x.Value > 0).ToList();

        }

        private async Task<List<LabelWiseInvestmentDTO>> GetFundRiskWiseBreakup(List<InvestorAccountTransactionDTO> AccountStatement, List<FundDTO> funds)
        {
            List<LabelWiseInvestmentDTO> Summary = new List<LabelWiseInvestmentDTO>();
            
            IEnumerable<IGrouping<int, FundDTO>> groupedByCategory = funds.GroupBy(x => x.RiskCategoryId);

            foreach (var fundsGroup in groupedByCategory)
            {
                decimal fundValue = 0;
                List<string> fundIds = new List<string>();

                foreach (FundDTO fund in fundsGroup)
                {
                    fundIds.Add(fund.ITMindsFundID);
                }

                List<IEnumerable<InvestorTransactionDTO>> Selected = AccountStatement.Select(x => x.InvTranscation.Where(y => fundIds.Contains(y.PlanId))).ToList();
                for (int i = 0; i < Selected.Count; i++)
                {
                    if (Selected[i].ToList().Count > 0)
                    {
                        //FundNAVDTO FundNAV = await _cloudService.GetFundNAV(Selected[i].ToList()[0].FundShortName);
                        FundDTO foundFund = funds.Where(x => x.ITMindsFundID == Selected[i].ToList().ElementAt(0).PlanId).FirstOrDefault();

                        if (foundFund != null)
                        {
                            FundNAVDTO FundNAV = await _fundService.GetFundNAVAsync(foundFund.FundId);
                            fundValue += ((decimal)Selected[i].ToList().Sum(x => x.NoOfUnitsDouble)) * decimal.Parse(FundNAV.navPerUnit);
                        }
                    }
                }
                Summary.Add(new LabelWiseInvestmentDTO { Label = fundsGroup.First().RiskCategoryName, Value = fundValue });
            }

            // Remove all funds in the list that now have been sold off.
            return Summary.Where(x => x.Value > 0).ToList();
        }

        private async Task<InvestmentSummaryDTO> GetInvestmentSummary(List<InvestorAccountTransactionDTO> AccountStatement)
        {
            InvestmentSummaryDTO Summary = new InvestmentSummaryDTO();
            string[] InvestmentAmountAddTxnList = { "Sale", "Sale of Unit Instruction", "Online Sale (A)", "Conversion In" };
            string[] InvestmentAmountSubtractTxnList = { "Redemption", "Redemption of Unit Instruction", "Online Redemption (B)", "Conversion Out" };

            foreach (InvestorAccountTransactionDTO AcctTranDTO in AccountStatement)
            {
                Summary.CurrentValue += (decimal)AcctTranDTO.TotalNavAmount;

                foreach (InvestorTransactionDTO SingleTxn in AcctTranDTO.InvTranscation)
                {
                    if(InvestmentAmountAddTxnList.Contains(SingleTxn.Transaction))
                    {
                        Summary.InvestedAmount += (decimal)SingleTxn.GrossAmountDouble;
                    }
                    else if (InvestmentAmountSubtractTxnList.Contains(SingleTxn.Transaction))
                    {
                        Summary.InvestedAmount -= (decimal)SingleTxn.GrossAmountDouble;
                    }                   
                }
            }

            Summary.GainLoss = Summary.CurrentValue - Summary.InvestedAmount;

            return Summary;
        }


        public async Task<DashboardSummaryDTO> GetDashboardSummaryFromCloud(string FolioNumber)
        {
            DashboardSummaryDTO DashboardSummary = new DashboardSummaryDTO();

            //Fetch needed data
            List<InvestorAccountTransactionDTO> AccountStatement = await GetAccountStatementCISFromCloud(FolioNumber);
            List<FundDTO> funds = await _fundService.GetFundsAsync();

            //Investment Summary
            DashboardSummary.InvestmentSummary = await GetInvestmentSummary(AccountStatement);            

            //CategoryWise Breakup
            DashboardSummary.CategoryWiseBreakup = await GetCategoryWiseBreakupCloud(AccountStatement, funds);

            //FundWise Summary
            DashboardSummary.FundWiseSummary = await GetFundWiseSummary(AccountStatement, funds);

            //RiskWise Summary
            DashboardSummary.RiskWiseBreakup = await GetFundRiskWiseBreakup(AccountStatement, funds);

            return DashboardSummary;
        }
    }
}
