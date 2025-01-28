using AutoMapper;
using Oracle.ManagedDataAccess.Client;
using PQAMCAPI.Models;
using PQAMCClasses.CloudDTOs;
using PQAMCClasses.DTOs;

namespace Mappings
{
    public class FundBankMapper : Profile
    {
        public FundBankMapper()
        {
            CreateMap<OracleDataReader, FundBankDTO>()
              .ForMember(dest => dest.FundBankId, src =>
              {
                  src.PreCondition(x => x["FUND_BANK_ID"] != DBNull.Value);
                  src.MapFrom(x => x["FUND_BANK_ID"]);
              })
              .ForMember(dest => dest.FundId, src =>
              {
                  src.PreCondition(x => x["FUND_ID"] != DBNull.Value);
                  src.MapFrom(x => x["FUND_ID"]);
              })
              .ForMember(dest => dest.BankName, src =>
              {
                  src.PreCondition(x => x["BANK_NAME"] != DBNull.Value);
                  src.MapFrom(x => x["BANK_NAME"]);
              })
              .ForMember(dest => dest.BranchName, src =>
              {
                  src.PreCondition(x => x["BRANCH_NAME"] != DBNull.Value);
                  src.MapFrom(x => x["BRANCH_NAME"]);
              })
              .ForMember(dest => dest.Location, src =>
              {
                  src.PreCondition(x => x["LOCATION"] != DBNull.Value);
                  src.MapFrom(x => x["LOCATION"]);
              })
              .ForMember(dest => dest.AccountTitle, src =>
              {
                  src.PreCondition(x => x["ACCOUNT_TITLE"] != DBNull.Value);
                  src.MapFrom(x => x["ACCOUNT_TITLE"]);
              }).ForMember(dest => dest.AccountNo, src =>
              {
                  src.PreCondition(x => x["ACCOUNT_NO"] != DBNull.Value);
                  src.MapFrom(x => x["ACCOUNT_NO"]);
              })
              .ForMember(dest => dest.IBANNumber, src =>
              {
                  src.PreCondition(x => x["IBAN_NUMBER"] != DBNull.Value);
                  src.MapFrom(x => x["IBAN_NUMBER"]);
              });


            CreateMap<FundBankAccountDTO, FundBankDTO>()
               .ForMember(dest => dest.BankName, src => src.MapFrom(x => x.BankName))
               .ForMember(dest => dest.AccountNo, src => src.MapFrom(x => x.AccountNumber))
               .ForMember(dest => dest.AccountTitle, src => src.MapFrom(x => x.FundName))
               .ForMember(dest => dest.BranchId, src => src.MapFrom(x => x.BranchId))
               //.ForMember(dest => dest.IBANNumber, src => src.MapFrom(x => x.Name))
               ;

        }
    }
}