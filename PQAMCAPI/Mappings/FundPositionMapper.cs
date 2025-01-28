using AutoMapper;
using Oracle.ManagedDataAccess.Client;
using PQAMCClasses.DTOs;

namespace Mappings
{
    public class FundPositionMapper : Profile
    {
        public FundPositionMapper()
        {
            CreateMap<OracleDataReader, FundPositionDTO>()
              .ForMember(dest => dest.FundId, src =>
              {
                  src.PreCondition(x => x["FUND_ID"] != DBNull.Value);
                  src.MapFrom(x => x["FUND_ID"]);
              })
              .ForMember(dest => dest.FundName, src =>
              {
                  src.PreCondition(x => x["FUND_NAME"] != DBNull.Value);
                  src.MapFrom(x => x["FUND_NAME"]);
              })
              .ForMember(dest => dest.CurrentValue, src =>
              {
                  src.PreCondition(x => x["FUND_VALUE"] != DBNull.Value);
                  src.MapFrom(x => x["FUND_VALUE"]);
              })
              .ForMember(dest => dest.NumberOfUnits, src =>
              {
                  src.PreCondition(x => x["UNITS"] != DBNull.Value);
                  src.MapFrom(x => x["UNITS"]);
              })
              .ForMember(dest => dest.ProfitLoss, src =>
              {
                  src.PreCondition(x => x["GAIN_LOSS"] != DBNull.Value);
                  src.MapFrom(x => x["GAIN_LOSS"]);
              })
              .ForMember(dest => dest.FrontEndLoadPercentage, src =>
              {
                  src.PreCondition(x => x["FRONT_END_LOAD_PERCENT"] != DBNull.Value);
                  src.MapFrom(x => x["FRONT_END_LOAD_PERCENT"]);
              })
              .ForMember(dest => dest.BackEndLoadPercentage, src =>
              {
                  src.PreCondition(x => x["BACK_END_LOAD_PERCENT"] != DBNull.Value);
                  src.MapFrom(x => x["BACK_END_LOAD_PERCENT"]);
              })
              .ForMember(dest => dest.LastNav, src =>
              {
                  src.PreCondition(x => x["NAV"] != DBNull.Value);
                  src.MapFrom(x => x["NAV"]);
              })
              .ForMember(dest => dest.OfferNav, src =>
              {
                  src.PreCondition(x => x["OFFER_PRICE"] != DBNull.Value);
                  src.MapFrom(x => x["OFFER_PRICE"]);
              })
              .ForMember(dest => dest.PurchaseNav, src =>
              {
                  src.PreCondition(x => x["REPURCHASE_PRICE"] != DBNull.Value);
                  src.MapFrom(x => x["REPURCHASE_PRICE"]);
              });
        }
    }
}