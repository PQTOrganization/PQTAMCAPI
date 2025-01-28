using AutoMapper;
using Oracle.ManagedDataAccess.Client;
using PQAMCAPI.Models;
using PQAMCClasses.DTOs;

namespace Mappings
{
    public class UserApplicationDiscrepancyMapper : Profile
    {
        public UserApplicationDiscrepancyMapper()
        {
            CreateMap<UserApplicationDiscrepancyDTO, UserApplicationDiscrepancy>();
            CreateMap<UserApplicationDiscrepancy, UserApplicationDiscrepancyDTO>();

            CreateMap<OracleDataReader, UserApplicationDiscrepancy>()
              .ForMember(dest => dest.UserApplicationDiscrepancyId,
                         src => src.MapFrom(x => x["USER_APPLICATION_DISCREPANCY_ID"]))
              .ForMember(dest => dest.DiscrepancyDate, src => src.MapFrom(x => x["DISCREPANCY_DATE"]))
              .ForMember(dest => dest.UserApplicationId, src => src.MapFrom(x => x["USER_APPLICATION_ID"]))
              .ForMember(dest => dest.ApplicationData, src => src.MapFrom(x => x["APPLICATION_DATA"]))
              .ForMember(dest => dest.DiscrepantFields, src => src.MapFrom(x => x["DISCREPANT_FIELDS"]));
        }
    }
}