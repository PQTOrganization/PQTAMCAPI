using AutoMapper;
using Oracle.ManagedDataAccess.Client;
using PQAMCAPI.Models;
using PQAMCClasses.DTOs;

namespace Mappings
{
    public class UserBankMapper : Profile
    {
        public UserBankMapper()
        {
            CreateMap<UserBankDTO, UserBank>();
            CreateMap<UserBank, UserBankDTO>();

            CreateMap<OracleDataReader, UserBank>()
              .ForMember(dest => dest.UserBankId, src => src.MapFrom(x => x["USER_BANK_ID"]))
              .ForMember(dest => dest.BankId, src => src.MapFrom(x => x["BANK_ID"]))
              .ForMember(dest => dest.UserId, src => src.MapFrom(x => x["USER_ID"]))
              .ForMember(dest => dest.IsOBAccount, src => src.MapFrom(x => x["IS_OB_ACCOUNT"]))

              .ForMember(dest => dest.IBANNumber, src =>
              {
                  src.PreCondition(x => x["IBAN_NUMBER"] != DBNull.Value);
                  src.MapFrom(x => x["IBAN_NUMBER"]);
              })
              .ForMember(dest => dest.IsIBANVerified, src =>
              {
                  src.PreCondition(x => x["IS_IBAN_VERIFIED"] != DBNull.Value);
                  src.MapFrom(x => x["IS_IBAN_VERIFIED"]);
              })
              .ForMember(dest => dest.OneLinkTitle, src =>
              {
                  src.PreCondition(x => x["ONE_LINK_TITLE"] != DBNull.Value);
                  src.MapFrom(x => x["ONE_LINK_TITLE"]);
              });
              
        }
    }
}