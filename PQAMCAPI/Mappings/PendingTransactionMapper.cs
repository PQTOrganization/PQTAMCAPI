using AutoMapper;
using Oracle.ManagedDataAccess.Client;
using PQAMCClasses.DTOs;

namespace Mappings
{
    public class PendingTransactionMapper : Profile
    {
        public PendingTransactionMapper()
        {
            CreateMap<OracleDataReader, PendingTransactionDTO>()
              .ForMember(dest => dest.RequestType, src =>
              {
                  src.PreCondition(x => x["REQUEST_TYPE"] != DBNull.Value);
                  src.MapFrom(x => x["REQUEST_TYPE"]);
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
              .ForMember(dest => dest.ToFundName, src =>
              {
                  src.PreCondition(x => x["TO_FUND_NAME"] != DBNull.Value);
                  src.MapFrom(x => x["TO_FUND_NAME"]);
              })
              .ForMember(dest => dest.RequestDate, src =>
              {
                  src.PreCondition(x => x["REQUEST_DATE"] != DBNull.Value);
                  src.MapFrom(x => x["REQUEST_DATE"]);
              })
              .ForMember(dest => dest.Amount, src =>
              {
                  src.PreCondition(x => x["AMOUNT"] != DBNull.Value);
                  src.MapFrom(x => x["AMOUNT"]);
              })
              .ForMember(dest => dest.PaymentMode, src =>
              {
                  src.PreCondition(x => x["PAYMENT_MODE"] != DBNull.Value);
                  src.MapFrom(x => x["PAYMENT_MODE"]);
              })
              .ForMember(dest => dest.PaymentReference, src =>
              {
                  src.PreCondition(x => x["ONLINE_PAYMENT_REFERENCE"] != DBNull.Value);
                  src.MapFrom(x => x["ONLINE_PAYMENT_REFERENCE"]);
              });
              //.ForMember(dest => dest.BankID, src =>
              //{
              //    src.PreCondition(x => x["BANKID"] != DBNull.Value);
              //    src.MapFrom(x => x["BANKID"]);
              //})
              //.ForMember(dest => dest.BankAccountNo, src =>
              //{
              //    src.PreCondition(x => x["BANKACCOUNTNO"] != DBNull.Value);
              //    src.MapFrom(x => x["BANKACCOUNTNO"]);
              //});
        }
    }
}