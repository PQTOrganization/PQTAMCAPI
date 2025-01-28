using AutoMapper;
using Oracle.ManagedDataAccess.Client;
using PQAMCAPI.Models;
using PQAMCClasses.DTOs;

namespace Mappings
{
    public class CountryMapper : Profile
    {
        public CountryMapper()
        {
            CreateMap<Country, CountryDTO>()
                .ForPath(dest => dest.Cities, src => src.MapFrom(x => x.City));
            
            CreateMap<CountryDTO, Country>()
               .ForPath(dest => dest.City, src => src.MapFrom(x => x.Cities));


            CreateMap<OracleDataReader, Country>()
               .ForMember(dest => dest.CountryId, src => src.MapFrom(x => x["country_id"]))
               .ForMember(dest => dest.Name, src => src.MapFrom(x => x["name"]))
               .ForMember(dest => dest.CountryCode, src => src.MapFrom(x => x["country_code"]))
               .ForMember(dest => dest.CurrencyName, src => src.MapFrom(x => x["currency_name"]))
               .ForMember(dest => dest.CurrencySymbol, src => src.MapFrom(x => x["currency_symbol"]));
        }
    }
}