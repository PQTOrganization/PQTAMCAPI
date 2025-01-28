using AutoMapper;
using Oracle.ManagedDataAccess.Client;
using PQAMCClasses.DTOs;

namespace Mappings
{
    public class AMCUserMapper : Profile
    {
        public AMCUserMapper()
        {
            CreateMap<OracleDataReader, AMCUserDTO>()
              .ForMember(dest => dest.FolioNumber, src =>
              {
                  src.PreCondition(x => x["FOLIO_NUMBER"] != DBNull.Value);
                  src.MapFrom(x => x["FOLIO_NUMBER"]);
              })
              .ForMember(dest => dest.AccountTitle, src =>
              {
                  src.PreCondition(x => x["ACCOUNT_TITLE"] != DBNull.Value);
                  src.MapFrom(x => x["ACCOUNT_TITLE"]);
              })
              .ForMember(dest => dest.IsActive, src =>
              {
                  src.PreCondition(x => x["IS_ACTIVE"] != DBNull.Value);
                  src.MapFrom(x => x["IS_ACTIVE"]);
              })
              .ForMember(dest => dest.CNIC, src =>
              {
                  src.PreCondition(x => x["CNIC"] != DBNull.Value);
                  src.MapFrom(x => x["CNIC"]);
              })
              .ForMember(dest => dest.CountryId, src =>
              {
                  src.PreCondition(x => x["COUNTRY_ID"] != DBNull.Value);
                  src.MapFrom(x => x["COUNTRY_ID"]);
              })
              .ForMember(dest => dest.CityId, src =>
              {
                  src.PreCondition(x => x["CITY_ID"] != DBNull.Value);
                  src.MapFrom(x => x["CITY_ID"]);
              })
              .ForMember(dest => dest.MobileNumber, src =>
              {
                  src.PreCondition(x => x["MOBILE_NUMBER"] != DBNull.Value);
                  src.MapFrom(x => x["MOBILE_NUMBER"]);
              })
              .ForMember(dest => dest.EmailAddress, src =>
              {
                  src.PreCondition(x => x["EMAIL_ADDRESS"] != DBNull.Value);
                  src.MapFrom(x => x["EMAIL_ADDRESS"]);
              });
        }
    }
}