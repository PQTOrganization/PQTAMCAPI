using AutoMapper;
using Oracle.ManagedDataAccess.Client;
using PQAMCClasses.DTOs;

namespace Mappings
{
    public class UserFolioMapper : Profile
    {
        public UserFolioMapper()
        {
            CreateMap<OracleDataReader, UserFolioDTO>()
              .ForMember(dest => dest.UserId, src => src.MapFrom(x => x["user_id"]))
              .ForMember(dest => dest.FirstName, src =>
              {
                  src.PreCondition(x => x["first_name"] != DBNull.Value);
                  src.MapFrom(x => x["first_name"]);
              })
              .ForMember(dest => dest.LastName, src =>
              {
                  src.PreCondition(x => x["last_name"] != DBNull.Value);
                  src.MapFrom(x => x["last_name"]);
              })
              .ForMember(dest => dest.FolioNumber, src =>
              {
                  src.PreCondition(x => x["FOLIO_NUMBER"] != DBNull.Value);
                  src.MapFrom(x => x["FOLIO_NUMBER"]);
              });
        }
    }
}