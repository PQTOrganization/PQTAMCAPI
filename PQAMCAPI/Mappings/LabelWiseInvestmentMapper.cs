using AutoMapper;
using Oracle.ManagedDataAccess.Client;
using PQAMCClasses.DTOs;

namespace Mappings
{
    public class LabelWiseInvestmentMapper : Profile
    {
        public LabelWiseInvestmentMapper()
        {
            CreateMap<OracleDataReader, LabelWiseInvestmentDTO>()
              .ForMember(dest => dest.Label, src =>
              {
                  src.PreCondition(x => x["CATEGORY_NAME"] != DBNull.Value);
                  src.MapFrom(x => x["CATEGORY_NAME"]);
              })
              .ForMember(dest => dest.Value, src =>
              {
                  src.PreCondition(x => x["FUND_VALUE"] != DBNull.Value);
                  src.MapFrom(x => x["FUND_VALUE"]);
              });
        }
    }
}