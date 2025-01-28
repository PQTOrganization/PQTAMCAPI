using AutoMapper;
using Oracle.ManagedDataAccess.Client;
using PQAMCClasses.DTOs;

namespace Mappings
{
    public class FundMapper : Profile
    {
        public FundMapper()
        {
            CreateMap<OracleDataReader, FundDTO>()
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
              .ForMember(dest => dest.RiskCategoryId, src =>
              {
                  src.PreCondition(x => x["RISK_CATEGORY_ID"] != DBNull.Value);
                  src.MapFrom(x => x["RISK_CATEGORY_ID"]);
              })
              .ForMember(dest => dest.CategoryId, src =>
              {
                  src.PreCondition(x => x["CATEGORY_ID"] != DBNull.Value);
                  src.MapFrom(x => x["CATEGORY_ID"]);
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
              })
              .ForMember(dest => dest.ITMindsFundID, src =>
              {
                  src.PreCondition(x => x["ITMINDS_FUND_ID"] != DBNull.Value);
                  src.MapFrom(x => x["ITMINDS_FUND_ID"]);
              })
              .ForMember(dest => dest.ITMindsFundShortName, src =>
              {
                  src.PreCondition(x => x["ITMINDS_FUND_SHORT_NAME"] != DBNull.Value);
                  src.MapFrom(x => x["ITMINDS_FUND_SHORT_NAME"]);
              })
              .ForMember(dest => dest.CategoryName, src =>
              {
                  src.PreCondition(x => x["CATEGORY_SHORT_NAME"] != DBNull.Value);
                  src.MapFrom(x => x["CATEGORY_SHORT_NAME"]);
              })
              .ForMember(dest => dest.RiskCategoryName, src =>
              {
                  src.PreCondition(x => x["RISK_CATEGORY_NAME"] != DBNull.Value);
                  src.MapFrom(x => x["RISK_CATEGORY_NAME"]);
              })
             .ForMember(dest => dest.MinInvestmentLimit, src =>
             {
                 src.PreCondition(x => x["MIN_INVESTMENT_LIMIT"] != DBNull.Value);
                 src.MapFrom(x => x["MIN_INVESTMENT_LIMIT"]);
             });
        }
    }
}