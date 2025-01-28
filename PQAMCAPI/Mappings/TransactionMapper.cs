using AutoMapper;
using Oracle.ManagedDataAccess.Client;
using PQAMCAPI.Models;
using PQAMCClasses.DTOs;

namespace Mappings
{
    public class TransactionMapper : Profile
    {
        public TransactionMapper()
        {
            CreateMap<OracleDataReader, TransactionDTO>()
              .ForMember(dest => dest.TransactionID, src =>
              {
                  src.PreCondition(x => x["TRANSACTION_ID"] != DBNull.Value);
                  src.MapFrom(x => x["TRANSACTION_ID"]);
              })
              .ForMember(dest => dest.Fundame, src =>
              {
                  src.PreCondition(x => x["FUND_NAME"] != DBNull.Value);
                  src.MapFrom(x => x["FUND_NAME"]);
              })
              .ForMember(dest => dest.ProcessDate, src =>
              {
                  src.PreCondition(x => x["PROCESS_DATE"] != DBNull.Value);
                  src.MapFrom(x => x["PROCESS_DATE"]);
              })
              .ForMember(dest => dest.GrossAmount, src =>
              {
                  src.PreCondition(x => x["GROSS_AMOUNT"] != DBNull.Value);
                  src.MapFrom(x => x["GROSS_AMOUNT"]);
              })
              .ForMember(dest => dest.NetAmount, src =>
              {
                  src.PreCondition(x => x["NET_AMOUNT"] != DBNull.Value);
                  src.MapFrom(x => x["NET_AMOUNT"]);
              });
        }
    }
}