using AutoMapper;
using Oracle.ManagedDataAccess.Client;
using PQAMCAPI.Models;
using PQAMCClasses.DTOs;

namespace Mappings
{
    public class CityMapper : Profile
    {
        public CityMapper()
        {
            CreateMap<City, CityDTO>();
            
            CreateMap<CityDTO, City>();

            CreateMap<OracleDataReader, City>()
               .ForMember(dest => dest.CityId, src => src.MapFrom(x => x["city_id"]))
               .ForMember(dest => dest.Name, src => src.MapFrom(x => x["name"]))
               .ForMember(dest => dest.CountryId, src => src.MapFrom(x => x["country_id"]));
        }
    }
}