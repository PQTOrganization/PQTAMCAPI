using Oracle.ManagedDataAccess.Client;
using AutoMapper;

using PQAMCAPI.Models;
using PQAMCClasses.DTOs;
using PQAMCClasses.CloudDTOs;

namespace KhaabachiAPI.Mappings
{
    public class GeneralDataMapper : Profile
    {
        public GeneralDataMapper()
        {
            CreateMap<OracleDataReader, AccountCategory>()
              .ForMember(dest => dest.AccountCategoryId,
                         src => src.MapFrom(x => x["account_category_id"]))
              .ForMember(dest => dest.Name, src => src.MapFrom(x => x["name"]))
              .ForMember(dest => dest.Title, src => src.MapFrom(x => x["title"]))
              .ForMember(dest => dest.SubTitle, src => src.MapFrom(x => x["subtitle"]))
              .ForMember(dest => dest.Description, src => src.MapFrom(x => x["description"]))
              .ForMember(dest => dest.DisplayOrder, src => src.MapFrom(x => x["display_order"]))
              .ForMember(dest => dest.FolioType, src => src.MapFrom(x => x["folio_type"]));

            CreateMap<OracleDataReader, Area>()
              .ForMember(dest => dest.AreaId, src => src.MapFrom(x => x["area_id"]))
              .ForMember(dest => dest.CityId, src => src.MapFrom(x => x["city_id"]))
              .ForMember(dest => dest.Name, src => src.MapFrom(x => x["name"]));

            CreateMap<OracleDataReader, Bank>()
              .ForMember(dest => dest.BankId, src => src.MapFrom(x => x["bank_id"]))
              .ForMember(dest => dest.Name, src => src.MapFrom(x => x["name"]));

            CreateMap<OracleDataReader, ContactOwnerShip>()
              .ForMember(dest => dest.ContactOwnerShipId,
                         src => src.MapFrom(x => x["contact_ownership_id"]))
              .ForMember(dest => dest.Name, src => src.MapFrom(x => x["name"]));

            CreateMap<OracleDataReader, Education>()
              .ForMember(dest => dest.EducationId, src => src.MapFrom(x => x["education_id"]))
              .ForMember(dest => dest.Name, src => src.MapFrom(x => x["name"]));

            CreateMap<OracleDataReader, Gender>()
              .ForMember(dest => dest.GenderId, src => src.MapFrom(x => x["gender_id"]))
              .ForMember(dest => dest.Name, src => src.MapFrom(x => x["name"]));

            CreateMap<OracleDataReader, Occupation>()
              .ForMember(dest => dest.OccupationId, src => src.MapFrom(x => x["occupation_id"]))
              .ForMember(dest => dest.Name, src => src.MapFrom(x => x["name"]))
              .ForMember(dest => dest.ITMindsName, src => src.MapFrom(x => x["ITMINDS_NAME"]));

            CreateMap<OracleDataReader, Profession>()
              .ForMember(dest => dest.ProfessionId, src => src.MapFrom(x => x["profession_id"]))
              .ForMember(dest => dest.Name, src => src.MapFrom(x => x["name"]));

            CreateMap<OracleDataReader, IncomeSource>()
              .ForMember(dest => dest.IncomeSourceId, src => src.MapFrom(x => x["income_source_id"]))
              .ForMember(dest => dest.Name, src => src.MapFrom(x => x["name"]))
              .ForMember(dest => dest.ITMindsName, src => src.MapFrom(x => x["ITMINDS_NAME"]));

            CreateMap<OracleDataReader, AnnualIncome>()
              .ForMember(dest => dest.AnnualIncomeId, src => src.MapFrom(x => x["annual_income_id"]))
              .ForMember(dest => dest.Name, src => src.MapFrom(x => x["name"]));

            CreateMap<OracleDataReader, ResidentialStatus>()
             .ForMember(dest => dest.ResidentialStatusId,
                        src => src.MapFrom(x => x["residential_status_id"]))
             .ForMember(dest => dest.Name, src => src.MapFrom(x => x["name"]));

            CreateMap<OracleDataReader, TINReason>()
             .ForMember(dest => dest.TINReasonId, src => src.MapFrom(x => x["TIN_REASON_ID"]))
             .ForMember(dest => dest.Name, src => src.MapFrom(x => x["NAME"]));

            CreateMap<OracleDataReader, UserRefreshToken>()
             .ForMember(dest => dest.UserId, src => src.MapFrom(x => x["user_id"]))
             .ForMember(dest => dest.UserRefreshTokenId, src =>
                        src.MapFrom(x => x["user_refresh_token_id"]))
             .ForMember(dest => dest.RefreshToken, src => src.MapFrom(x => x["refresh_token"]))
             .ForMember(dest => dest.TokenDate, src => src.MapFrom(x => x["token_date"]));

            CreateMap<OracleDataReader, SystemSettings>()
             .ForMember(dest => dest.NotificationToEmail, src => src.MapFrom(x => x["NOTIFICATION_TO_EMAIL"]))
             .ForMember(dest => dest.NotificationCCEmail, src =>
                        src.MapFrom(x => x["NOTIFICATION_CC_EMAIL"]));

            CreateMap<OracleDataReader, VPSPlan>()
              .ForMember(dest => dest.VPSPlanId, src => src.MapFrom(x => x["VPS_PLAN_ID"]))
              .ForMember(dest => dest.AllocationScheme, src => src.MapFrom(x => x["ALLOCATION_SCHEME"]))
              .ForMember(dest => dest.EquitySubFund, src => {
                  src.PreCondition(x => x["EQUITY_SUB_FUND"] != DBNull.Value);
                  src.MapFrom(x => x["EQUITY_SUB_FUND"]);
              })
              .ForMember(dest => dest.DebtSubFund, src => {
                  src.PreCondition(x => x["DEBT_SUB_FUND"] != DBNull.Value);
                  src.MapFrom(x => x["DEBT_SUB_FUND"]);
              })
              .ForMember(dest => dest.MoneyMarketSubFund, src => {
                  src.PreCondition(x => x["MONEY_MARKET_SUB_FUND"] != DBNull.Value);
                  src.MapFrom(x => x["MONEY_MARKET_SUB_FUND"]);
                  })
              .ForMember(dest => dest.AccountCategoryID, src => src.MapFrom(x => x["ACCOUNT_CATEGORY_ID"]));

            CreateMap<OracleDataReader, UserApplicationNominee>()
              .ForMember(dest => dest.UserApplicationNomineeId, src => src.MapFrom(x => x["USER_APPLICATION_NOMINEE_ID"]))
              .ForMember(dest => dest.UserApplicationId, src => src.MapFrom(x => x["USER_APPLICATION_ID"]))
              .ForMember(dest => dest.SerialNumber, src => {
                  src.PreCondition(x => x["SERIAL_NUMBER"] != DBNull.Value);
                  src.MapFrom(x => x["SERIAL_NUMBER"]);
              })
              .ForMember(dest => dest.Name, src => src.MapFrom(x => x["NAME"]))
              .ForMember(dest => dest.Relationship, src => src.MapFrom(x => x["RELATIONSHIP"]))
              .ForMember(dest => dest.Share, src => {
                  src.PreCondition(x => x["SHARE"] != DBNull.Value);
                  src.MapFrom(x => x["SHARE"]);
              })
              .ForMember(dest => dest.ResidentialAddress, src => src.MapFrom(x => x["RESIDENTIAL_ADDRESS"]))
              .ForMember(dest => dest.TelephoneNumber, src => src.MapFrom(x => x["TELEPHONE_NUMBER"]))
              .ForMember(dest => dest.BankAccountDetail, src => src.MapFrom(x => x["BANK_ACCOUNT_DETAIL"]))
              .ForMember(dest => dest.CNIC, src => src.MapFrom(x => x["CNIC"]));

            CreateMap<User, UserAuthData>();
            CreateMap<AccountCategory, AccountCategoryDTO>();
            CreateMap<AccountCategoryDTO, AccountCategory>();
            CreateMap<City, CityDTO>();
            CreateMap<CityDTO, City>();
            CreateMap<Country, CountryDTO>();
            CreateMap<CountryDTO, Country>();
            CreateMap<Area, AreaDTO>();
            CreateMap<AreaDTO, Area>();
            CreateMap<Bank, BankDTO>();
            CreateMap<BankDTO, Bank>();
            CreateMap<OracleDataReader, ContactOwnerShipDTO>();
            CreateMap<ContactOwnerShipDTO, ContactOwnerShip>();
            CreateMap<Education, EducationDTO>();
            CreateMap<EducationDTO, Education>();
            CreateMap<Gender, GenderDTO>();
            CreateMap<GenderDTO, Gender>();
            CreateMap<Occupation, OccupationDTO>();
            CreateMap<OccupationDTO, Occupation>();
            CreateMap<Profession, ProfessionDTO>();
            CreateMap<ProfessionDTO, Profession>();
            CreateMap<IncomeSource, SourceIncomeDTO>();
            CreateMap<SourceIncomeDTO, IncomeSource>();
            CreateMap<TINReason, TINReasonDTO>();
            CreateMap<TINReasonDTO, TINReason>();
            CreateMap<GetAccountStatementReportResponseDTO, AccountStatementReportDTO>();
            CreateMap<UserApplicationDTO, UserApplicationEmailDTO>();
            CreateMap<VPSPlan, VPSPlanDTO>();
            CreateMap<UserApplicationNomineeDTO, UserApplicationNominee>();
            CreateMap<UserApplicationNominee, UserApplicationNomineeDTO>();

            CreateMap<OracleDataReader, AMCFolioNumber>()
              .ForMember(dest => dest.FolioNumber, src => src.MapFrom(x => x["FOLIO_NUMBER"]));

            CreateMap<InvestorTransactionDTO, InvestorTransaction>();
        }
    }
}
