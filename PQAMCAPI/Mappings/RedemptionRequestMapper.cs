using AutoMapper;
using Oracle.ManagedDataAccess.Client;
using PQAMCClasses.CloudDTOs;
using PQAMCClasses.DTOs;

namespace Mappings
{
    public class RedemptionRequestMapper : Profile
    {
        public RedemptionRequestMapper()
        {
            CreateMap<OracleDataReader, RedemptionRequestDTO>()
              .ForMember(dest => dest.RedemptionRequestId, src =>
              {
                  src.PreCondition(x => x["REDEMPTION_REQUEST_ID"] != DBNull.Value);
                  src.MapFrom(x => x["REDEMPTION_REQUEST_ID"]);
              })
              .ForMember(dest => dest.RequestDate, src =>
              {
                    src.PreCondition(x => x["REQUEST_DATE"] != DBNull.Value);
                    src.MapFrom(x => x["REQUEST_DATE"]);
              })
              .ForMember(dest => dest.FolioNumber, src =>
              {
                  src.PreCondition(x => x["FOLIO_NUMBER"] != DBNull.Value);
                  src.MapFrom(x => x["FOLIO_NUMBER"]);
              })
              .ForMember(dest => dest.UserId, src =>
              {
                  src.PreCondition(x => x["USER_ID"] != DBNull.Value);
                  src.MapFrom(x => x["USER_ID"]);
              })
              .ForMember(dest => dest.UserName, src =>
              {
                  src.PreCondition(x => x["USER_NAME"] != DBNull.Value);
                  src.MapFrom(x => x["USER_NAME"]);
              })
              .ForMember(dest => dest.CNIC, src =>
              {
                  src.PreCondition(x => x["CNIC"] != DBNull.Value);
                  src.MapFrom(x => x["CNIC"]);
              })
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
              .ForMember(dest => dest.RedemptionAmount, src =>
              {
                  src.PreCondition(x => x["REDEMPTION_AMOUNT"] != DBNull.Value);
                  src.MapFrom(x => x["REDEMPTION_AMOUNT"]);
              })
              .ForMember(dest => dest.BackEndLoad, src =>
              {
                  src.PreCondition(x => x["BACK_END_LOAD"] != DBNull.Value);
                  src.MapFrom(x => x["BACK_END_LOAD"]);
              })
              .ForMember(dest => dest.NavApplied, src =>
              {
                  src.PreCondition(x => x["NAV_APPLIED"] != DBNull.Value);
                  src.MapFrom(x => x["NAV_APPLIED"]);
              })
              //.ForMember(dest => dest.BankID, src =>
              //{
              //   src.PreCondition(x => x["BANK_ID"] != DBNull.Value);
              //   src.MapFrom(x => x["BANK_ID"]);
              //})
              //.ForMember(dest => dest.BankAccountNo, src =>
              //{
              //   src.PreCondition(x => x["BANK_ACCOUNT_NO"] != DBNull.Value);
              //   src.MapFrom(x => x["BANK_ACCOUNT_NO"]);
              //})
              .ForMember(dest => dest.RequestStatus, src =>
              {
                  src.PreCondition(x => x["REQUEST_STATUS"] != DBNull.Value);
                  src.MapFrom(x => x["REQUEST_STATUS"]);
              });

            CreateMap<RedemptionRequestDTO, SubmitRedemptionRequestDTO>()
                .ForMember(dest => dest.folioNo, src =>
                            src.MapFrom(x => x.FolioNumber.ToString().PadLeft(7, '0')));
        }
    }
}