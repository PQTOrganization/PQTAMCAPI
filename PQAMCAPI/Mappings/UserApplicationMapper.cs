using AutoMapper;
using Oracle.ManagedDataAccess.Client;
using PQAMCAPI.Models;
using PQAMCClasses.CloudDTOs;
using PQAMCClasses.DTOs;

namespace Mappings
{
    public class UserApplicationMapper : Profile
    {
        public UserApplicationMapper()
        {
            CreateMap<UserApplicationDTO, UserApplication>();
            CreateMap<UserApplication, UserApplicationDTO>();

            CreateMap<OracleDataReader, UserApplication>()
               .ForMember(dest => dest.UserApplicationId, src => src.MapFrom(x => x["USER_APPLICATION_ID"]))
               .ForMember(dest => dest.UserId, src => src.MapFrom(x => x["USER_ID"]))
               .ForMember(dest => dest.IsFinalSubmit, src => src.MapFrom(x => x["IS_FINAL_SUBMIT"]))
               .ForMember(dest => dest.ApplicationStatusId, src => src.MapFrom(x => x["APPLICATION_STATUS_ID"]))

               .ForMember(dest => dest.FolioNumber, src =>
               {
                   src.PreCondition(x => x["FOLIO_NUMBER"] != DBNull.Value);
                   src.MapFrom(x => x["FOLIO_NUMBER"]);
               })
               .ForMember(dest => dest.AccountCategoryId, src =>
               {
                   src.PreCondition(x => x["ACCOUNT_CATEGORY_ID"] != DBNull.Value);
                   src.MapFrom(x => x["ACCOUNT_CATEGORY_ID"]);
               })
               .ForMember(dest => dest.FundPreferenceId, src =>
               {
                   src.PreCondition(x => x["FUND_PREFERENCE_ID"] != DBNull.Value);
                   src.MapFrom(x => x["FUND_PREFERENCE_ID"]);
               })
               .ForMember(dest => dest.VPSFundId, src =>
               {
                   src.PreCondition(x => x["VPS_FUND_ID"] != DBNull.Value);
                   src.MapFrom(x => x["VPS_FUND_ID"]);
               })
               .ForMember(dest => dest.Name, src =>
               {
                   src.PreCondition(x => x["NAME"] != DBNull.Value);
                   src.MapFrom(x => x["NAME"]);
               })
               .ForMember(dest => dest.FatherHusbandName, src =>
               {
                   src.PreCondition(x => x["FATHER_HUSBAND_NAME"] != DBNull.Value);
                   src.MapFrom(x => x["FATHER_HUSBAND_NAME"]);
               })
               .ForMember(dest => dest.GenderId, src =>
               {
                   src.PreCondition(x => x["GENDER_ID"] != DBNull.Value);
                   src.MapFrom(x => x["GENDER_ID"]);
               })
               .ForMember(dest => dest.DateOfBirth, src =>
               {
                   src.PreCondition(x => x["DATE_OF_BIRTH"] != DBNull.Value);
                   src.MapFrom(x => x["DATE_OF_BIRTH"]);
               })
               .ForMember(dest => dest.MotherMaidenName, src =>
               {
                   src.PreCondition(x => x["MOTHER_MAIDEN_NAME"] != DBNull.Value);
                   src.MapFrom(x => x["MOTHER_MAIDEN_NAME"]);
               })
               .ForMember(dest => dest.CNIC, src =>
               {
                   src.PreCondition(x => x["CNIC"] != DBNull.Value);
                   src.MapFrom(x => x["CNIC"]);
               })
               .ForMember(dest => dest.CNICIssueDate, src =>
               {
                   src.PreCondition(x => x["CNIC_ISSUE_DATE"] != DBNull.Value);
                   src.MapFrom(x => x["CNIC_ISSUE_DATE"]);
               })
               .ForMember(dest => dest.CNICExpiryDate, src =>
               {
                   src.PreCondition(x => x["CNIC_EXPIRY_DATE"] != DBNull.Value);
                   src.MapFrom(x => x["CNIC_EXPIRY_DATE"]);
               })
               .ForMember(dest => dest.IsCNICLifeTime, src =>
               {
                   src.PreCondition(x => x["IS_CNIC_LIFETIME"] != DBNull.Value);
                   src.MapFrom(x => x["IS_CNIC_LIFETIME"]);
               })
               .ForMember(dest => dest.ContactOwnershipId, src =>
               {
                   src.PreCondition(x => x["CONTACT_OWNERSHIP_ID"] != DBNull.Value);
                   src.MapFrom(x => x["CONTACT_OWNERSHIP_ID"]);
               })
               .ForMember(dest => dest.ExpectedRetirementAge, src =>
               {
                   src.PreCondition(x => x["EXPECTED_RETIREMENT_AGE"] != DBNull.Value);
                   src.MapFrom(x => x["EXPECTED_RETIREMENT_AGE"]);
               })
               .ForMember(dest => dest.ExpectedRetirementDate, src =>
               {
                   src.PreCondition(x => x["EXPECTED_RETIREMENT_DATE"] != DBNull.Value);
                   src.MapFrom(x => x["EXPECTED_RETIREMENT_DATE"]);
               })
               .ForMember(dest => dest.ResidentialAddress, src =>
               {
                   src.PreCondition(x => x["RESIDENTIAL_ADDRESS"] != DBNull.Value);
                   src.MapFrom(x => x["RESIDENTIAL_ADDRESS"]);
               })
               .ForMember(dest => dest.IsResidentAdressSameAsCNIC, src =>
               {
                   src.PreCondition(x => x["IS_RESIDENTIAL_ADDRESS_SAME_AS_CNIC"] != DBNull.Value);
                   src.MapFrom(x => x["IS_RESIDENTIAL_ADDRESS_SAME_AS_CNIC"]);
               })
               .ForMember(dest => dest.MailingAddress, src =>
               {
                   src.PreCondition(x => x["MAILING_ADDRESS"] != DBNull.Value);
                   src.MapFrom(x => x["MAILING_ADDRESS"]);
               })
               .ForMember(dest => dest.MailingSameAsResidential, src =>
               {
                   src.PreCondition(x => x["MAILING_SAME_AS_RESIDENTIAL"] != DBNull.Value);
                   src.MapFrom(x => x["MAILING_SAME_AS_RESIDENTIAL"]);
               })
               .ForMember(dest => dest.ResidentialStatusId, src =>
               {
                   src.PreCondition(x => x["RESIDENTIAL_STATUS_ID"] != DBNull.Value);
                   src.MapFrom(x => x["RESIDENTIAL_STATUS_ID"]);
               })
               .ForMember(dest => dest.CountryOfResidenceId, src =>
               {
                   src.PreCondition(x => x["COUNTRY_OF_RESIDENCE_ID"] != DBNull.Value);
                   src.MapFrom(x => x["COUNTRY_OF_RESIDENCE_ID"]);
               })
               .ForMember(dest => dest.CityOfResidenceId, src =>
               {
                   src.PreCondition(x => x["CITY_OF_RESIDENCE_ID"] != DBNull.Value);
                   src.MapFrom(x => x["CITY_OF_RESIDENCE_ID"]);
               })
               .ForMember(dest => dest.AreaId, src =>
               {
                   src.PreCondition(x => x["AREA_ID"] != DBNull.Value);
                   src.MapFrom(x => x["AREA_ID"]);
               })
               .ForMember(dest => dest.NationalityId, src =>
               {
                   src.PreCondition(x => x["NATIONALITY_ID"] != DBNull.Value);
                   src.MapFrom(x => x["NATIONALITY_ID"]);
               })
               .ForMember(dest => dest.CountryOfBirthId, src =>
               {
                   src.PreCondition(x => x["COUNTRY_OF_BIRTH_ID"] != DBNull.Value);
                   src.MapFrom(x => x["COUNTRY_OF_BIRTH_ID"]);
               })
               .ForMember(dest => dest.CityOfBirthId, src =>
               {
                   src.PreCondition(x => x["CITY_OF_BIRTH_ID"] != DBNull.Value);
                   src.MapFrom(x => x["CITY_OF_BIRTH_ID"]);
               })
               .ForMember(dest => dest.IsPoliticallyExposedPerson, src =>
               {
                   src.PreCondition(x => x["IS_POLITICALLY_EXPOSED_PERSON"] != DBNull.Value);
                   src.MapFrom(x => x["IS_POLITICALLY_EXPOSED_PERSON"]);
               })
               .ForMember(dest => dest.IsUSResident, src =>
               {
                   src.PreCondition(x => x["IS_US_RESIDENT"] != DBNull.Value);
                   src.MapFrom(x => x["IS_US_RESIDENT"]);
               })
               .ForMember(dest => dest.IsNonPakTaxResident, src =>
               {
                   src.PreCondition(x => x["IS_NON_PAK_TAX_RESIDENT"] != DBNull.Value);
                   src.MapFrom(x => x["IS_NON_PAK_TAX_RESIDENT"]);
               })
               .ForMember(dest => dest.IsZakatDeduction, src =>
               {
                   src.PreCondition(x => x["IS_ZAKAT_DEDUCTION"] != DBNull.Value);
                   src.MapFrom(x => x["IS_ZAKAT_DEDUCTION"]);
               })
               .ForMember(dest => dest.IsNonMuslim, src =>
               {
                   src.PreCondition(x => x["IS_NON_MUSLIM"] != DBNull.Value);
                   src.MapFrom(x => x["IS_NON_MUSLIM"]);
               })
               .ForMember(dest => dest.UseOnlineBanking, src =>
               {
                   src.PreCondition(x => x["USE_ONLINE_BANKING"] != DBNull.Value);
                   src.MapFrom(x => x["USE_ONLINE_BANKING"]);
               })
               .ForMember(dest => dest.EducationId, src =>
               {
                   src.PreCondition(x => x["EDUCATION_ID"] != DBNull.Value);
                   src.MapFrom(x => x["EDUCATION_ID"]);
               })
               .ForMember(dest => dest.ProfessionId, src =>
               {
                   src.PreCondition(x => x["PROFESSION_ID"] != DBNull.Value);
                   src.MapFrom(x => x["PROFESSION_ID"]);
               })
               .ForMember(dest => dest.OccupationId, src =>
               {
                   src.PreCondition(x => x["OCCUPATION_ID"] != DBNull.Value);
                   src.MapFrom(x => x["OCCUPATION_ID"]);
               })
               .ForMember(dest => dest.AnnualIncomeId, src =>
               {
                   src.PreCondition(x => x["ANNUAL_INCOME_ID"] != DBNull.Value);
                   src.MapFrom(x => x["ANNUAL_INCOME_ID"]);
               })
               .ForMember(dest => dest.SourceOfIncome, src =>
               {
                   src.PreCondition(x => x["INCOME_SOURCE"] != DBNull.Value);
                   src.MapFrom(x => x["INCOME_SOURCE"].ToString().Split(",",
                                    StringSplitOptions.RemoveEmptyEntries));
               })
               .ForMember(dest => dest.NextOfKinName, src =>
               {
                   src.PreCondition(x => x["NEXT_OF_KIN_NAME"] != DBNull.Value);
                   src.MapFrom(x => x["NEXT_OF_KIN_NAME"]);
               })
               .ForMember(dest => dest.NextOfKinCNIC, src =>
               {
                   src.PreCondition(x => x["NEXT_OF_KIN_CNIC"] != DBNull.Value);
                   src.MapFrom(x => x["NEXT_OF_KIN_CNIC"]);
               })
               .ForMember(dest => dest.NextOfKinRelationship, src =>
               {
                   src.PreCondition(x => x["NEXT_OF_KIN_RELATIONSHIP"] != DBNull.Value);
                   src.MapFrom(x => x["NEXT_OF_KIN_RELATIONSHIP"]);
               })
               .ForMember(dest => dest.NextOfKinMobileNumber, src =>
               {
                   src.PreCondition(x => x["NEXT_OF_KIN_MOBILE_NUMBER"] != DBNull.Value);
                   src.MapFrom(x => x["NEXT_OF_KIN_MOBILE_NUMBER"]);
               })
               .ForMember(dest => dest.NomineeShare, src =>
               {
                   src.PreCondition(x => x["NOMINEE_SHARE"] != DBNull.Value);
                   src.MapFrom(x => x["NOMINEE_SHARE"]);
               })
               .ForMember(dest => dest.IsHeadOfState, src =>
               {
                   src.PreCondition(x => x["IS_HEAD_OF_STATE"] != DBNull.Value);
                   src.MapFrom(x => x["IS_HEAD_OF_STATE"]);
               })
               .ForMember(dest => dest.IsHeadOfGovt, src =>
               {
                   src.PreCondition(x => x["IS_HEAD_OF_GOVT"] != DBNull.Value);
                   src.MapFrom(x => x["IS_HEAD_OF_GOVT"]);
               })
               .ForMember(dest => dest.IsSeniorPolitician, src =>
               {
                   src.PreCondition(x => x["IS_SENIOR_POLITICIAN"] != DBNull.Value);
                   src.MapFrom(x => x["IS_SENIOR_POLITICIAN"]);
               })
               .ForMember(dest => dest.IsSeniorGovtOfficial, src =>
               {
                   src.PreCondition(x => x["IS_SENIOR_GOVT_OFFICIAL"] != DBNull.Value);
                   src.MapFrom(x => x["IS_SENIOR_GOVT_OFFICIAL"]);
               })
               .ForMember(dest => dest.IsSeniorJudicialOfficial, src =>
               {
                   src.PreCondition(x => x["IS_SENIOR_JUDICIAL_OFFICIAL"] != DBNull.Value);
                   src.MapFrom(x => x["IS_SENIOR_JUDICIAL_OFFICIAL"]);
               })
               .ForMember(dest => dest.IsSeniorMilitaryOfficial, src =>
               {
                   src.PreCondition(x => x["IS_SENIOR_MILITARY_OFFICIAL"] != DBNull.Value);
                   src.MapFrom(x => x["IS_SENIOR_MILITARY_OFFICIAL"]);
               })
               .ForMember(dest => dest.IsSeniorExecSOC, src =>
               {
                   src.PreCondition(x => x["IS_SENIOR_EXEC_SOC"] != DBNull.Value);
                   src.MapFrom(x => x["IS_SENIOR_EXEC_SOC"]);
               })
               .ForMember(dest => dest.IsImportantPoliticalPartyOfficial, src =>
               {
                   src.PreCondition(x => x["IS_IMPORTANT_POLITICAL_PARTY_OFFICIAL"] != DBNull.Value);
                   src.MapFrom(x => x["IS_IMPORTANT_POLITICAL_PARTY_OFFICIAL"]);
               })
               .ForMember(dest => dest.IsSeniorExecIO, src =>
               {
                   src.PreCondition(x => x["IS_SENIOR_EXEC_IO"] != DBNull.Value);
                   src.MapFrom(x => x["IS_SENIOR_EXEC_IO"]);
               })
               .ForMember(dest => dest.IsMemberOfBOIO, src =>
               {
                   src.PreCondition(x => x["IS_MEMBER_OF_BOIO"] != DBNull.Value);
                   src.MapFrom(x => x["IS_MEMBER_OF_BOIO"]);
               })
               .ForMember(dest => dest.NatureOfPEPId, src =>
               {
                   src.PreCondition(x => x["NATURE_OF_PEP_ID"] != DBNull.Value);
                   src.MapFrom(x => x["NATURE_OF_PEP_ID"]);
               })
               .ForMember(dest => dest.PEPNatureOfDepartment, src =>
               {
                   src.PreCondition(x => x["PEP_NATURE_OF_DEPARTMENT"] != DBNull.Value);
                   src.MapFrom(x => x["PEP_NATURE_OF_DEPARTMENT"]);
               })
               .ForMember(dest => dest.PEPNameOfFamilyMember, src =>
               {
                   src.PreCondition(x => x["PEP_NAME_OF_FAMILY_MEMBER"] != DBNull.Value);
                   src.MapFrom(x => x["PEP_NAME_OF_FAMILY_MEMBER"]);
               })
               .ForMember(dest => dest.PEPNameOfCloseAssociate, src =>
               {
                   src.PreCondition(x => x["PEP_NAME_OF_CLOSE_ASSOCIATE"] != DBNull.Value);
                   src.MapFrom(x => x["PEP_NAME_OF_CLOSE_ASSOCIATE"]);
               })
               .ForMember(dest => dest.PEPDesignation, src =>
               {
                   src.PreCondition(x => x["PEP_DESIGNATION"] != DBNull.Value);
                   src.MapFrom(x => x["PEP_DESIGNATION"]);
               })
               .ForMember(dest => dest.PEPGrade, src =>
               {
                   src.PreCondition(x => x["PEP_GRADE"] != DBNull.Value);
                   src.MapFrom(x => x["PEP_GRADE"]);
               })
               .ForMember(dest => dest.W9Name, src =>
               {
                   src.PreCondition(x => x["W9_NAME"] != DBNull.Value);
                   src.MapFrom(x => x["W9_NAME"]);
               })
               .ForMember(dest => dest.W9Address, src =>
               {
                   src.PreCondition(x => x["W9_ADDRESS"] != DBNull.Value);
                   src.MapFrom(x => x["W9_ADDRESS"]);
               })
               .ForMember(dest => dest.W9TIN, src =>
               {
                   src.PreCondition(x => x["W9_TIN"] != DBNull.Value);
                   src.MapFrom(x => x["W9_TIN"]);
               })
               .ForMember(dest => dest.W9SSN, src =>
               {
                   src.PreCondition(x => x["W9_SSN"] != DBNull.Value);
                   src.MapFrom(x => x["W9_SSN"]);
               })
               .ForMember(dest => dest.W9EIN, src =>
               {
                   src.PreCondition(x => x["W9_EIN"] != DBNull.Value);
                   src.MapFrom(x => x["W9_EIN"]);
               })
               .ForMember(dest => dest.IsCertify, src =>
               {
                   src.PreCondition(x => x["IS_CERTIFY"] != DBNull.Value);
                   src.MapFrom(x => x["IS_CERTIFY"]);
               })
               .ForMember(dest => dest.CountryOfTaxId, src =>
               {
                   src.PreCondition(x => x["COUNTRY_OF_TAX_ID"] != DBNull.Value);
                   src.MapFrom(x => x["COUNTRY_OF_TAX_ID"]);
               })
               .ForMember(dest => dest.IsTINAvailable, src =>
               {
                   src.PreCondition(x => x["IS_TIN_AVAILABLE"] != DBNull.Value);
                   src.MapFrom(x => x["IS_TIN_AVAILABLE"]);
               })
               .ForMember(dest => dest.TINNumber, src =>
               {
                   src.PreCondition(x => x["TIN_NUMBER"] != DBNull.Value);
                   src.MapFrom(x => x["TIN_NUMBER"]);
               })
               .ForMember(dest => dest.TINReasonId, src =>
               {
                   src.PreCondition(x => x["TIN_REASON_ID"] != DBNull.Value);
                   src.MapFrom(x => x["TIN_REASON_ID"]);
               })
               .ForMember(dest => dest.TINReasonDetail, src =>
               {
                   src.PreCondition(x => x["TIN_REASON_DETAIL"] != DBNull.Value);
                   src.MapFrom(x => x["TIN_REASON_DETAIL"]);
               })
               .ForMember(dest => dest.IsReceiveStatememt, src =>
               {
                   src.PreCondition(x => x["IS_RECEIVE_STATEMENT"] != DBNull.Value);
                   src.MapFrom(x => x["IS_RECEIVE_STATEMENT"]);
               })
               .ForMember(dest => dest.IsReinvest, src =>
               {
                   src.PreCondition(x => x["IS_RE_INVEST"] != DBNull.Value);
                   src.MapFrom(x => x["IS_RE_INVEST"]);
               })
               .ForMember(dest => dest.CellOwnerConsent, src =>
               {
                   src.PreCondition(x => x["CELL_OWNER_CONSENT"] != DBNull.Value);
                   src.MapFrom(x => x["CELL_OWNER_CONSENT"]);
               })
               .ForMember(dest => dest.IsAgreeTerms, src =>
               {
                   src.PreCondition(x => x["IS_AGREE_TERMS"] != DBNull.Value);
                   src.MapFrom(x => x["IS_AGREE_TERMS"]);
               })
               .ForMember(dest => dest.IsFinalSubmit, src =>
               {
                   src.PreCondition(x => x["IS_FINAL_SUBMIT"] != DBNull.Value);
                   src.MapFrom(x => x["IS_FINAL_SUBMIT"]);
               })
               .ForMember(dest => dest.ApplicationStatusId, src =>
               {
                   src.PreCondition(x => x["APPLICATION_STATUS_ID"] != DBNull.Value);
                   src.MapFrom(x => x["APPLICATION_STATUS_ID"]);
               })
               .ForMember(dest => dest.CreatedOn, src =>
               {
                   src.PreCondition(x => x["CREATED_ON"] != DBNull.Value);
                   src.MapFrom(x => x["CREATED_ON"]);
               })
               .ForMember(dest => dest.ModifiedOn, src =>
               {
                   src.PreCondition(x => x["MODIFIED_ON"] != DBNull.Value);
                   src.MapFrom(x => x["MODIFIED_ON"]);
               })
               .ForMember(dest => dest.SalesRepresentativeNameCode, src =>
               {
                   src.PreCondition(x => x["SALES_REPRESENTATIVE_NAME_CODE"] != DBNull.Value);
                   src.MapFrom(x => x["SALES_REPRESENTATIVE_NAME_CODE"]);
               })
               .ForMember(dest => dest.SalesRepresentativeMobileNumber, src =>
               {
                   src.PreCondition(x => x["SALES_REPRESENTATIVE_MOBILE"] != DBNull.Value);
                   src.MapFrom(x => x["SALES_REPRESENTATIVE_MOBILE"]);
               })
               .ForMember(dest => dest.DebtSubFund, src =>
               {
                   src.PreCondition(x => x["DEBT_SUB_FUND"] != DBNull.Value);
                   src.MapFrom(x => x["DEBT_SUB_FUND"]);
               })
               .ForMember(dest => dest.EquitySubFund, src =>
               {
                   src.PreCondition(x => x["EQUITY_SUB_FUND"] != DBNull.Value);
                   src.MapFrom(x => x["EQUITY_SUB_FUND"]);
               })
               .ForMember(dest => dest.MoneyMarketSubFund, src =>
               {
                   src.PreCondition(x => x["MONEY_MARKET_SUB_FUND"] != DBNull.Value);
                   src.MapFrom(x => x["MONEY_MARKET_SUB_FUND"]);
               })
               .ForMember(dest => dest.InitialInvestmentRequestID, src =>
               {
                   src.PreCondition(x => x["INITIAL_INVESTMENT_REQUEST_ID"] != DBNull.Value);
                   src.MapFrom(x => x["INITIAL_INVESTMENT_REQUEST_ID"]);
               })
               .ForMember(dest => dest.ModeOfContribution, src =>
               {
                   src.PreCondition(x => x["MODE_OF_CONTRIBUTION"] != DBNull.Value);
                   src.MapFrom(x => x["MODE_OF_CONTRIBUTION"]);
               })
               .ForMember(dest => dest.InitialContributionAmount, src =>
               {
                   src.PreCondition(x => x["INITIAL_CONTRIBUTION_AMOUNT"] != DBNull.Value);
                   src.MapFrom(x => x["INITIAL_CONTRIBUTION_AMOUNT"]);
               })
               .ForMember(dest => dest.AmountInWords, src =>
               {
                   src.PreCondition(x => x["AMOUNT_IN_WORDS"] != DBNull.Value);
                   src.MapFrom(x => x["AMOUNT_IN_WORDS"]);
               })
               .ForMember(dest => dest.FrontEndLoad, src =>
               {
                   src.PreCondition(x => x["FRONTEND_LOAD"] != DBNull.Value);
                   src.MapFrom(x => x["FRONTEND_LOAD"]);
               })
               .ForMember(dest => dest.ContributionPaymentMode, src =>
               {
                   src.PreCondition(x => x["CONTRIBUTION_PAYMENT_MODE"] != DBNull.Value);
                   src.MapFrom(x => x["CONTRIBUTION_PAYMENT_MODE"]);
               })
               .ForMember(dest => dest.ContributionReferenceNumber, src =>
               {
                   src.PreCondition(x => x["CONTRIBUTION_REF_NO"] != DBNull.Value);
                   src.MapFrom(x => x["CONTRIBUTION_REF_NO"]);
               })
               .ForMember(dest => dest.DrawnOn, src =>
               {
                   src.PreCondition(x => x["DRAWN_ON"] != DBNull.Value);
                   src.MapFrom(x => x["DRAWN_ON"]);
               })
               .ForMember(dest => dest.ContributionFrequency, src =>
               {
                   src.PreCondition(x => x["CONTRIBUTION_FREQUENCY"] != DBNull.Value);
                   src.MapFrom(x => x["CONTRIBUTION_FREQUENCY"]);
               })
               .ForMember(dest => dest.PeriodicContributionAmount, src =>
               {
                   src.PreCondition(x => x["PERIODIC_CONTRIBUTION_AMOUNT"] != DBNull.Value);
                   src.MapFrom(x => x["PERIODIC_CONTRIBUTION_AMOUNT"]);
               })
               .ForMember(dest => dest.YearlyContributionAmount, src =>
               {
                   src.PreCondition(x => x["YEARLY_CONTRIBUTION_AMOUNT"] != DBNull.Value);
                   src.MapFrom(x => x["YEARLY_CONTRIBUTION_AMOUNT"]);
               })
               .ForMember(dest => dest.OnBehalfOfAnotherPerson, src =>
               {
                   src.PreCondition(x => x["ON_BEHALF_OF_ANOTHER_PERSON"] != DBNull.Value);
                   src.MapFrom(x => x["ON_BEHALF_OF_ANOTHER_PERSON"]);
               })
               .ForMember(dest => dest.ConnectionTaxHavens, src =>
               {
                   src.PreCondition(x => x["CONNECTION_TAX_HAVENS"] != DBNull.Value);
                   src.MapFrom(x => x["CONNECTION_TAX_HAVENS"]);
               })
               .ForMember(dest => dest.DealingInHighValueItems, src =>
               {
                   src.PreCondition(x => x["DEALING_IN_HIGH_VALUE_ITEMS"] != DBNull.Value);
                   src.MapFrom(x => x["DEALING_IN_HIGH_VALUE_ITEMS"]);
               })
               .ForMember(dest => dest.HasFinancialInstRefusedToOpenAccount, src =>
               {
                   src.PreCondition(x => x["HAS_FINANCIAL_INSTITUTION_REFUSED_ACCOUNT"] != DBNull.Value);
                   src.MapFrom(x => x["HAS_FINANCIAL_INSTITUTION_REFUSED_ACCOUNT"]);
               })
               .ForMember(dest => dest.TotalScore, src =>
               {
                   src.PreCondition(x => x["TOTAL_SCORE"] != DBNull.Value);
                   src.MapFrom(x => x["TOTAL_SCORE"]);
               })
               .ForMember(dest => dest.RiskProfile, src =>
               {
                   src.PreCondition(x => x["RISK_PROFILE"] != DBNull.Value);
                   src.MapFrom(x => x["RISK_PROFILE"]);
               });

            CreateMap<UserApplication, SubmitSaleRequestDTO>()
                .ForMember(dest => dest.additionalDisclosurePEP, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.additionalDisclosurePosition, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.address1, src => src.MapFrom(x => x.ResidentialAddress))
                .ForMember(dest => dest.address2, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.cnicIssueDate, src => src.MapFrom(x => x.CNICIssueDate.Value.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.dateOfBirth, src => src.MapFrom(x => x.DateOfBirth.Value.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.debtSubFund, src => src.MapFrom(x => x.DebtSubFund))
                .ForMember(dest => dest.dividendPayout, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.email, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.equitySubFund, src => src.MapFrom(x => x.EquitySubFund))
                .ForMember(dest => dest.fatherName, src => src.MapFrom(x => x.FatherHusbandName))
                .ForMember(dest => dest.folioType, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.grossSaleAmount, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.guardianCnic, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.guardianCnicAllowNull, src => src.MapFrom(x => "N"))
                .ForMember(dest => dest.guardianName, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.guardianCnicExpiryDate, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.guardianCnicIssueDate, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.instrumentNumber, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.instrumentType, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.investmentProofUpload, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.isRealized, src => src.MapFrom(x => "N"))
                .ForMember(dest => dest.lifeTime, src => src.MapFrom(x => x.IsCNICLifeTime == true ? "Y" : "N"))
                .ForMember(dest => dest.mailingAddress1, src => src.MapFrom(x => x.MailingAddress))
                .ForMember(dest => dest.mailingAddress2, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.minor, src => src.MapFrom(x => "N"))
                .ForMember(dest => dest.mmSubFund, src => src.MapFrom(x => x.MoneyMarketSubFund))
                .ForMember(dest => dest.mobCityCode, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.mobCountryCode, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.mobileNumber, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.motherName, src => src.MapFrom(x => x.MotherMaidenName))
                .ForMember(dest => dest.nameOfEmployer, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.nomineeName, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.nomineePhnNumber, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.occupation, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.phnCityCode, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.phnCountryCode, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.phnNumber, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.planFundId, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.principleCnic, src => src.MapFrom(x => x.CNIC))
                .ForMember(dest => dest.principleCnicExpiryDate, src => src.MapFrom(x => x.CNICExpiryDate.Value.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.relationWithMinor, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.reserved1, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.reserved2, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.reserved3, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.reserved4, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.reserved5, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.residentialStatus, src => src.MapFrom(x => x.ResidentialStatusId))
                .ForMember(dest => dest.retirementAge, src => src.MapFrom(x => x.ExpectedRetirementAge))
                .ForMember(dest => dest.salutation, src => src.MapFrom(x => "Mr."))
                .ForMember(dest => dest.seq, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.sourceOfIncome, src => src.MapFrom(x => x.SourceOfIncome))
                .ForMember(dest => dest.title, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.vpsFundId, src => src.MapFrom(x => x.VPSFundId))
                .ForMember(dest => dest.zakatStatus, src => src.MapFrom(x => "Liable"))
                .ForMember(dest => dest.formReceivingDateTime, src => src.MapFrom(x => x.CreatedOn.Value.ToString("dd/MM/yyyy HH:mm:ss")))
                .ForMember(dest => dest.additionalDisclosurePEP, src => src.MapFrom(x => "N"))
                .ForMember(dest => dest.additionalDisclosurePosition, src => src.MapFrom(x => "N"));

            CreateMap<UserApplication, SubmitDigitalAccountRequestDTO>()
                .ForMember(dest => dest.fatherName, src => src.MapFrom(x => x.FatherHusbandName))
                .ForMember(dest => dest.motherName, src => src.MapFrom(x => x.MotherMaidenName))
                .ForMember(dest => dest.dateOfBirth, src => src.MapFrom(x => x.DateOfBirth.Value.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.title, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.mailingAddress1, src => src.MapFrom(x => x.MailingAddress))
                .ForMember(dest => dest.principleCnic, src => src.MapFrom(x => x.CNIC))
                .ForMember(dest => dest.principleCnicExpiryDate, src => src.MapFrom(x => x.CNICExpiryDate.Value.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.cnicIssueDate, src => src.MapFrom(x => x.CNICIssueDate.Value.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.lifeTime, src => src.MapFrom(x => x.IsCNICLifeTime.Value ? "Y" : "N"))
                .ForMember(dest => dest.retirementAge, src => src.MapFrom(x => x.ExpectedRetirementAge))
                .ForMember(dest => dest.address1, src => src.MapFrom(x => x.ResidentialAddress))
                .ForMember(dest => dest.zakatStatus, src => src.MapFrom(x => x.IsZakatDeduction.Value ? "Y" : "NA"));
        }
    }
}