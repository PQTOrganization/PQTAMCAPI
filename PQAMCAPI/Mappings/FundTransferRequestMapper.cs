using AutoMapper;
using Oracle.ManagedDataAccess.Client;
using PQAMCClasses.CloudDTOs;
using PQAMCClasses.DTOs;

namespace Mappings
{
    public class FundTransferRequestMapper : Profile
    {
        public FundTransferRequestMapper()
        {
            CreateMap<OracleDataReader, FundTransferRequestDTO>()
              .ForMember(dest => dest.FundTransferRequestId, src =>
              {
                  src.PreCondition(x => x["FUND_TRANSFER_REQUEST_ID"] != DBNull.Value);
                  src.MapFrom(x => x["FUND_TRANSFER_REQUEST_ID"]);
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
              .ForMember(dest => dest.FromFundId, src =>
              {
                  src.PreCondition(x => x["FROM_FUND_ID"] != DBNull.Value);
                  src.MapFrom(x => x["FROM_FUND_ID"]);
              })
              .ForMember(dest => dest.FromFundName, src =>
              {
                  src.PreCondition(x => x["FROM_FUND_NAME"] != DBNull.Value);
                  src.MapFrom(x => x["FROM_FUND_NAME"]);
              })
              .ForMember(dest => dest.TransferAmount, src =>
              {
                  src.PreCondition(x => x["TRANSFER_AMOUNT"] != DBNull.Value);
                  src.MapFrom(x => x["TRANSFER_AMOUNT"]);
              })
              .ForMember(dest => dest.FromNavApplied, src =>
              {
                  src.PreCondition(x => x["FROM_NAV_APPLIED"] != DBNull.Value);
                  src.MapFrom(x => x["FROM_NAV_APPLIED"]);
              })
              .ForMember(dest => dest.FromNumOfUnits, src =>
              {
                  src.PreCondition(x => x["FROM_NUM_OF_UNITS"] != DBNull.Value);
                  src.MapFrom(x => x["FROM_NUM_OF_UNITS"]);
              })
              .ForMember(dest => dest.ToFundId, src =>
              {
                  src.PreCondition(x => x["TO_FUND_ID"] != DBNull.Value);
                  src.MapFrom(x => x["TO_FUND_ID"]);
              })
              .ForMember(dest => dest.ToFundName, src =>
              {
                  src.PreCondition(x => x["TO_FUND_NAME"] != DBNull.Value);
                  src.MapFrom(x => x["TO_FUND_NAME"]);
              })
              .ForMember(dest => dest.ToNavApplied, src =>
              {
                  src.PreCondition(x => x["TO_NAV_APPLIED"] != DBNull.Value);
                  src.MapFrom(x => x["TO_NAV_APPLIED"]);
              })
              .ForMember(dest => dest.ToNumOfUnits, src =>
              {
                  src.PreCondition(x => x["TO_NUM_OF_UNITS"] != DBNull.Value);
                  src.MapFrom(x => x["TO_NUM_OF_UNITS"]);
              }).ForMember(dest => dest.RequestStatus, src =>
              {
                  src.PreCondition(x => x["REQUEST_STATUS"] != DBNull.Value);
                  src.MapFrom(x => x["REQUEST_STATUS"]);
              });


            CreateMap<FundTransferRequestDTO, SubmitConversionRequestDTO>()
                .ForMember(dest => dest.folioNo, src =>
                            src.MapFrom(x => x.FolioNumber.ToString().PadLeft(7, '0')))
                .ForMember(dest => dest.fromPlanFundId, src => src.MapFrom(x => x.FromFundId))
                .ForMember(dest => dest.toPlanFundId, src => src.MapFrom(x => x.ToFundId))
                //.ForMember(dest => dest.fromUnitTypeClass, src => src.MapFrom(x => x.FromNumOfUnits))
                .ForMember(dest => dest.allUnits, src => src.MapFrom(x => "N"))
                .ForMember(dest => dest.formReceivingDateTime, src => src.MapFrom(x => x.RequestDate.ToString("dd/MM/yyyy HH:mm:ss")))
                .ForMember(dest => dest.conversionTermValue, src => src.MapFrom(x => x.TransferAmount));
        }
    }
}