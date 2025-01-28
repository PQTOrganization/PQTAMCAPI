using AutoMapper;
using Oracle.ManagedDataAccess.Client;
using PQAMCAPI.Models;

namespace Mappings
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<OracleDataReader, User>()
              .ForMember(dest => dest.UserId, src => src.MapFrom(x => x["user_id"]))
              .ForMember(dest => dest.MobileNumber, src => src.MapFrom(x => x["mobile_number"]))
              .ForMember(dest => dest.Email, src => src.MapFrom(x => x["email"]))
              .ForMember(dest => dest.EmailConfirmed, src => src.MapFrom(x => x["email_confirmed"]))
              .ForMember(dest => dest.RegistrationDate, src => src.MapFrom(x => x["registration_date"]))

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
              .ForMember(dest => dest.OTP, src =>
              {
                  src.PreCondition(x => x["otp"] != DBNull.Value);
                  src.MapFrom(x => x["otp"]);
              })
              .ForMember(dest => dest.ProfileImage, src =>
              {
                  src.PreCondition(x => x["profile_image"] != DBNull.Value);
                  src.MapFrom(x => x["profile_image"]);
              })
              .ForMember(dest => dest.FolioNumber, src =>
              {
                  src.PreCondition(x => x["FOLIO_NUMBER"] != DBNull.Value);
                  src.MapFrom(x => x["FOLIO_NUMBER"]);
              })
              .ForMember(dest => dest.AccountStatus, src =>
              {
                  src.PreCondition(x => x["USER_ACCOUNT_STATUS"] != DBNull.Value);
                  src.MapFrom(x => x["USER_ACCOUNT_STATUS"]);
              })
              .ForMember(dest => dest.LastOTPGenerateDateTime, src =>
              {
                  src.PreCondition(x => x["LAST_OTP_GENERATE_DATETIME"] != DBNull.Value);
                  src.MapFrom(x => x["LAST_OTP_GENERATE_DATETIME"]);
              })
              .ForMember(dest => dest.IncorrectOTPAttempts, src =>
              {
                  src.PreCondition(x => x["INCORRECT_OTP_ATTEMPTS"] != DBNull.Value);
                  src.MapFrom(x => x["INCORRECT_OTP_ATTEMPTS"]);
              });
        }
    }
}