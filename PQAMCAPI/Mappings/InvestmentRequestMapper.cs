using AutoMapper;
using Oracle.ManagedDataAccess.Client;
using PQAMCClasses.CloudDTOs;
using PQAMCClasses.DTOs;

namespace Mappings
{
    public class InvestmentRequestMapper : Profile
    {
        public InvestmentRequestMapper()
        {
            CreateMap<OracleDataReader, InvestmentRequestDTO>()
              .ForMember(dest => dest.InvestmentRequestId, src =>
              {
                  src.PreCondition(x => x["INVESTMENT_REQUEST_ID"] != DBNull.Value);
                  src.MapFrom(x => x["INVESTMENT_REQUEST_ID"]);
              })
              .ForMember(dest => dest.RequestDate, src =>
              {
                    src.PreCondition(x => x["REQUEST_DATE"] != DBNull.Value);
                    src.MapFrom(x => x["REQUEST_DATE"]);
              })
              .ForMember(dest => dest.FolioNumber, src =>
              {
                  src.PreCondition(x => x["FOLIO_NUMBER"] != DBNull.Value);
                  src.MapFrom(x => x["FOLIO_NUMBER"]);
              })
              .ForMember(dest => dest.UserId, src =>
              {
                  src.PreCondition(x => x["USER_ID"] != DBNull.Value);
                  src.MapFrom(x => x["USER_ID"]);
              })
              .ForMember(dest => dest.UserName, src =>
              {
                  src.PreCondition(x => x["USER_NAME"] != DBNull.Value);
                  src.MapFrom(x => x["USER_NAME"]);
              })
              .ForMember(dest => dest.CNIC, src =>
              {
                  src.PreCondition(x => x["CNIC"] != DBNull.Value);
                  src.MapFrom(x => x["CNIC"]);
              })
              .ForMember(dest => dest.FundId, src =>
              {
                  src.PreCondition(x => x["FUND_ID"] != DBNull.Value);
                  src.MapFrom(x => x["FUND_ID"]);
              })
              .ForMember(dest => dest.FundName, src =>
              {
                  src.PreCondition(x => x["FUND_NAME"] != DBNull.Value);
                  src.MapFrom(x => x["FUND_NAME"]);
              })
              .ForMember(dest => dest.InvestmentAmount, src =>
              {
                  src.PreCondition(x => x["INVESTMENT_AMOUNT"] != DBNull.Value);
                  src.MapFrom(x => x["INVESTMENT_AMOUNT"]);
              })
              .ForMember(dest => dest.PaymentMode, src =>
              {
                  src.PreCondition(x => x["PAYMENT_MODE"] != DBNull.Value);
                  src.MapFrom(x => x["PAYMENT_MODE"]);
              })
              .ForMember(dest => dest.FrontEndLoad, src =>
              {
                  src.PreCondition(x => x["FRONT_END_LOAD"] != DBNull.Value);
                  src.MapFrom(x => x["FRONT_END_LOAD"]);
              })
              .ForMember(dest => dest.NavApplied, src =>
              {
                  src.PreCondition(x => x["NAV_APPLIED"] != DBNull.Value);
                  src.MapFrom(x => x["NAV_APPLIED"]);
              })
              .ForMember(dest => dest.ProofOfPayment, src =>
              {
                  src.PreCondition(x => x["PROOF_OF_PAYMENT"] != DBNull.Value);
                  src.MapFrom(x => x["PROOF_OF_PAYMENT"]);
              })
              .ForMember(dest => dest.BankId, src =>
              {
                  src.PreCondition(x => x["BANK_ID"] != DBNull.Value);
                  src.MapFrom(x => x["BANK_ID"]);
              })
              .ForMember(dest => dest.OnlinePaymentReference, src =>
              {
                  src.PreCondition(x => x["ONLINE_PAYMENT_REFERENCE"] != DBNull.Value);
                  src.MapFrom(x => x["ONLINE_PAYMENT_REFERENCE"]);
              })
              .ForMember(dest => dest.RequestStatus, src =>
              {
                  src.PreCondition(x => x["REQUEST_STATUS"] != DBNull.Value);
                  src.MapFrom(x => x["REQUEST_STATUS"]);
              })
              .ForMember(dest => dest.AccountNumber, src =>
              {
                  src.PreCondition(x => x["ACCOUNT_NO"] != DBNull.Value);
                  src.MapFrom(x => x["ACCOUNT_NO"]);
              })
              .ForMember(dest => dest.BankName, src =>
              {
                  src.PreCondition(x => x["BANK_NAME"] != DBNull.Value);
                  src.MapFrom(x => x["BANK_NAME"]);
              })
              .ForMember(dest => dest.BranchId, src =>
              {
                  src.PreCondition(x => x["BRANCH_ID"] != DBNull.Value);
                  src.MapFrom(x => x["BRANCH_ID"]);
              })
              .ForMember(dest => dest.ITMindsBankID, src =>
              {
                  src.PreCondition(x => x["ITMINDS_BANK_ID"] != DBNull.Value);
                  src.MapFrom(x => x["ITMINDS_BANK_ID"]);
              });

            CreateMap<InvestmentRequestDTO, SubmitSubSaleRequestDTO>()
               .ForMember(dest => dest.folioType, src => src.MapFrom(x => ""))
               //.ForMember(dest => dest.folioId, src => 
               //             src.MapFrom(x => x.FolioNumber.ToString().PadLeft(7, '0')))
               .ForMember(dest => dest.folioId, src =>
                            src.MapFrom(x => x.FolioNumber))
               .ForMember(dest => dest.investmentAmount, src => src.MapFrom(x => x.InvestmentAmount))
               .ForMember(dest => dest.investmentType, src => src.MapFrom(x => Enum.GetName(typeof(PQAMCClasses.Globals.PaymentModes), x.PaymentMode)))
               .ForMember(dest => dest.isRealized, src => src.MapFrom(x => "Y"))
               .ForMember(dest => dest.formReceivingDateTime, src => 
                                                  src.MapFrom(x => x.RequestDate.ToString("dd/MM/yyyy HH:mm:ss")))
               .ForMember(dest => dest.investmentProofUpload, src => src.MapFrom(x => string.IsNullOrEmpty(x.ProofOfPayment) ? "-" : x.ProofOfPayment.Substring(x.ProofOfPayment.IndexOf(",") + 1)))
               .ForMember(dest => dest.bankId, src => src.MapFrom(x => x.BankId))
               .ForMember(dest => dest.branchId, src => src.MapFrom(x => x.BranchId))
               .ForMember(dest => dest.accountNumber, src => src.MapFrom(x => x.AccountNumber))
               .ForMember(dest => dest.bankId, src => src.MapFrom(x => x.ITMindsBankID));

            CreateMap<InitialInvestmentRequestDTO, InvestmentRequestDTO>();
        }
    }
}