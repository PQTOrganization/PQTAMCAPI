using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;
using AutoMapper;
using PQAMCClasses.CloudDTOs;
using PQAMCClasses;

namespace PQAMCAPI.Services.Domain
{
    public class ITMindsService : IITMindsService
    {        
        private readonly IMapper _mapper;
        private readonly IAMCFolioNumberDBService _amcFolioNumberDBService;
        private readonly IInvestorTransactionDBService _investorTransactionDBService;
        private readonly ICloudService _cloudService;

        public ITMindsService(IMapper mapper, IAMCFolioNumberDBService amcFolioNumberDBService,
            IInvestorTransactionDBService investorTransactionDBService, ICloudService cloudService)
        {
            _mapper = mapper;
            _amcFolioNumberDBService = amcFolioNumberDBService;
            _investorTransactionDBService = investorTransactionDBService;
            _cloudService = cloudService;
        }

        private async Task<List<InvestorTransactionDTO>> FetchAccountStatement(string FolioNumber)
        {
            List<InvestorAccountTransactionDTO> AcctStmt = await _cloudService.GetAccountStatementCISFromCloud(FolioNumber);
            List<InvestorTransactionDTO> Txns = new List<InvestorTransactionDTO>();

            for(int i = 0; i < AcctStmt.Count; i++)
            {
                Txns.AddRange(AcctStmt[i].InvTranscation);
            }

            return Txns;
        }

        private async Task<List<InvestorTransaction>> InsertInvestorTransactionDB(List<InvestorTransactionDTO> Txns, AMCFolioNumber AMCFolioNo)
        {
            List<InvestorTransaction> InvestorTxns = new List<InvestorTransaction>();
            InvestorTransaction InvestorTxn = new InvestorTransaction();

            for(int i = 0; i < Txns.Count; i++)
            {
                InvestorTxn = _mapper.Map<InvestorTransaction>(Txns[i]);
                InvestorTxn.FolioNumber = AMCFolioNo.FolioNumber;
                InvestorTxn.DividendPayout = string.Join(",", Txns[i].DividendPayout);
                InvestorTxn = await _investorTransactionDBService.InsertInvestorTransaction(InvestorTxn);
                InvestorTxns.Add(InvestorTxn);
            }

            return InvestorTxns;
        }

        public async Task<string> GetAllAccountStatementsAsync()
        {
            List<AMCFolioNumber> folioNumbers = await _amcFolioNumberDBService.GetAllAsync();

            for(int i = 0; i < folioNumbers.Count; i++)
            {
                List<InvestorTransactionDTO> InvTxns = await FetchAccountStatement(folioNumbers[i].FolioNumber);
                List<InvestorTransaction> Txns = await InsertInvestorTransactionDB(InvTxns, folioNumbers[i]);
            }

            return Globals.SuccessMessages.FetchedAcctStmnt;
        }
    }
}
