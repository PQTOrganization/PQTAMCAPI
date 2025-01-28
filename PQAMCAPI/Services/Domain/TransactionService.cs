using PQAMCAPI.Interfaces.Services;
using PQAMCClasses.CloudDTOs;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Services.Domain
{
    public class TransactionService: ITransactionService
    {
        const string PACKAGE_NAME = "AMC_INVESTMENT_PKG";

        private readonly IStoreProcedureService _spService;
        private readonly ICloudService _cloudService;

        public TransactionService(IStoreProcedureService spService, ICloudService cloudService)
        {
            _spService = spService;
            _cloudService = cloudService;
        }

        public async Task<List<TransactionDTO>> GetTransactionsForFolioAsync(string FolioNumber)
        {
            var Transactions = await _spService.GetAllSP<TransactionDTO>(
                                                PACKAGE_NAME + ".GET_TRANSACTIONS_OF_FOLIO",
                                                FolioNumber);
            return Transactions;
        }

        public async Task<List<PendingTransactionDTO>> GetPendingTransactionsForFolioAsync(
            string FolioNumber)
        {
            var Transactions = await _spService.GetAllSP<PendingTransactionDTO>(
                                    PACKAGE_NAME + ".GET_PENDING_TRANSACTION_FOR_FOLIO",
                                    FolioNumber);
            return Transactions;
        }

        public async Task<List<TransactionDTO>> GetTransactionsForFolioFromCloudAsync(string FolioNumber)
        {
            List<TransactionDTO> Transactions = new List<TransactionDTO>();
            TransactionDTO Txn = new TransactionDTO();
            int index = 1;

            //GetAccountStatementCISRequestDTO request = new GetAccountStatementCISRequestDTO();
            //request.folioNo = PQAMCClasses.Globals.GetFolioNumber(FolioNumber);
            //request.statementType = "T";
            //request.fundPlanId = "*ALL";
            //request.fromDate = "01/01/" + DateTime.Now.Year.ToString();
            //request.toDate = DateTime.Now.Date.ToString("dd/MM/yyyy");

       
            List<InvestorAccountTransactionDTO> accountStatement = await _cloudService.GetAccountStatementCISFromCloud(FolioNumber);

            foreach (InvestorAccountTransactionDTO trans in accountStatement)
            {
                foreach (InvestorTransactionDTO InvTxn in trans.InvTranscation)
                {
                    Txn = new TransactionDTO();
                    Txn.TransactionID = index++.ToString();
                    Txn.Fundame = InvTxn.FundName;
                    if (InvTxn.NavDate != null)
                    {
                        Txn.ProcessDate = (DateTime)InvTxn.NavDate;
                    }
                    Txn.GrossAmount = (decimal)InvTxn.GrossAmountDouble;
                    Txn.NetAmount = (decimal)InvTxn.NetAmountDouble;
                    Txn.Transaction = InvTxn.Transaction;
                    Transactions.Add(Txn);
                }
            }

            return Transactions.OrderByDescending(x => x.ProcessDate).ToList();
        }
    }
}
