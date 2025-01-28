using AutoMapper;
using Oracle.ManagedDataAccess.Client;
using PQAMCClasses.DTOs;

namespace Mappings
{
    public class InvestmentSummaryMapper : Profile
    {
        public InvestmentSummaryMapper()
        {
            CreateMap<OracleDataReader, InvestmentSummaryDTO>()
              .ForMember(dest => dest.InvestedAmount, src =>
              {
                  src.PreCondition(x => x["INVESTED_AMOUNT"] != DBNull.Value);
                  src.MapFrom(x => x["INVESTED_AMOUNT"]);
              })
              .ForMember(dest => dest.CurrentValue, src =>
              {
                  src.PreCondition(x => x["CURRENT_VALUE"] != DBNull.Value);
                  src.MapFrom(x => x["CURRENT_VALUE"]);
              })
              .ForMember(dest => dest.GainLoss, src =>
              {
                  src.PreCondition(x => x["GAIN_LOSS"] != DBNull.Value);
                  src.MapFrom(x => x["GAIN_LOSS"]);
              });
        }
    }
}