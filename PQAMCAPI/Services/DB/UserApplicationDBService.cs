using System.Data;
using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;
using Oracle.ManagedDataAccess.Client;

using Helper;
using System.Collections.Generic;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Services
{
    public class UserApplicationDBService : IUserApplicationDBService
    {
        const string PACKAGE_NAME = "AMC_USER_APPLICATION_PKG";

        private readonly IStoreProcedureService _spService;
        private readonly OracleConnection conn;

        public UserApplicationDBService(IConfiguration configuration, IStoreProcedureService spService)
        {
            String connStr = DBSettingsHelper.GetConnectionString(configuration);
            conn = new OracleConnection(connStr);

            _spService = spService;            
        }
        
        public Task<UserApplication> FindAsync(int UserApplicationId)
        {
            var UserApplication = _spService.GetSP<UserApplication>(PACKAGE_NAME + ".get_user_application",
                                                                    UserApplicationId);
            return UserApplication;
        }

        public Task<List<UserApplication>> GetAllAsync()
        {
            var UserApplications = _spService.GetAllSP<UserApplication>(PACKAGE_NAME + ".get_user_applications", -1);
            return UserApplications;
        }

        public async Task<UserApplication> GetApplicationForUser(int UserId)
        {
            try
            {                
                return await _spService.GetSP<UserApplication>(PACKAGE_NAME + ".GET_USER_APPLICATION_FOR_USER",
                                                               UserId);
            }
            catch(Exception ex)
            {
                return null;
            }            
        }

        public async Task<UserApplication> InsertUserApplication(UserApplication Data)
        {
            await conn.OpenAsync();

            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".INSERT_USER_APPLICATION", conn);
            objCmd.CommandType = CommandType.StoredProcedure;

            PopulateParamsFromData(objCmd, Data);
            objCmd.Parameters.Add("P_NEW_USER_APPLICATION_ID", OracleDbType.Int64, ParameterDirection.Output);

            await objCmd.ExecuteNonQueryAsync();

            // Best way to convert Int64 to int
            Data.UserApplicationId = int.Parse(objCmd.Parameters["P_NEW_USER_APPLICATION_ID"].Value
                                        .ToString());

            await conn.CloseAsync();

            return Data;
        }

        public async Task<UserApplication> UpdateUserApplication(int UserApplicationId, 
                                                                 UserApplication Data)
        {           
                await conn.OpenAsync();

                OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".UPDATE_USER_APPLICATION", conn);
                objCmd.CommandType = CommandType.StoredProcedure;

                objCmd.Parameters.Add("P_USER_APPLICATION_ID", OracleDbType.Int64).Value = Data.UserApplicationId;
                PopulateParamsFromData(objCmd, Data);

                await objCmd.ExecuteNonQueryAsync();

                await conn.CloseAsync();

                return Data;                     
        }

        public async Task<UserApplication> UpdateUserApplicationStatus(int UserApplicationId, short Status)
        {
            var UserApplication = await FindAsync(UserApplicationId);

            await conn.OpenAsync();

            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".UPDATE_USER_APPLICATION_STATUS", conn);
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.Add("P_USER_APPLICATION_ID", OracleDbType.Int64).Value = UserApplicationId;
            objCmd.Parameters.Add("P_STATUS", OracleDbType.Int64).Value = Status;
            objCmd.Parameters.Add("P_RECORD_COUNT", OracleDbType.Int64, ParameterDirection.Output);

            await objCmd.ExecuteNonQueryAsync();

            int count = int.Parse(objCmd.Parameters["P_RECORD_COUNT"].Value.ToString());
            await conn.CloseAsync();

            UserApplication.ApplicationStatusId = Status;

            return UserApplication;
        }

        public async Task<Boolean> DoesCNICExistsForOtherApplication(string CellNo, string CNIC)
        {
            await conn.OpenAsync();

            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + 
                ".CHECK_USER_CNIC_EXISTS_IN_OTHER_APPLICATION", conn);
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.Add("P_CELL_NO", OracleDbType.Varchar2).Value = CellNo;
            objCmd.Parameters.Add("P_CNIC", OracleDbType.Varchar2).Value = CNIC;
            objCmd.Parameters.Add("P_RECORD_COUNT", OracleDbType.Int64, ParameterDirection.Output);

            await objCmd.ExecuteNonQueryAsync();

            int count = int.Parse(objCmd.Parameters["P_RECORD_COUNT"].Value.ToString());
            await conn.CloseAsync();

            return count > 0;
        }

        public Task<int> DeleteUserApplication(int UserApplicationId)
        {
            var rowCount = _spService.DeleteSP<UserApplication>(PACKAGE_NAME + ".del_residential_status",
                                                                UserApplicationId);
            return rowCount;
        }
        
        private void PopulateParamsFromData(OracleCommand objCmd, UserApplication Data)
        {
            objCmd.Parameters.Add("P_USER_ID", OracleDbType.Int64).Value = Data.UserId;
            objCmd.Parameters.Add("P_ACCOUNT_CATEGORY_ID", OracleDbType.Int64).Value = Data.AccountCategoryId;
            objCmd.Parameters.Add("P_FUND_PREFERENCE_ID", OracleDbType.Int64).Value = Data.FundPreferenceId;
            objCmd.Parameters.Add("P_VPS_FUND_ID", OracleDbType.Int64).Value = Data.VPSFundId;
            objCmd.Parameters.Add("P_NAME", OracleDbType.NVarchar2).Value = Data.Name;
            objCmd.Parameters.Add("P_FATHER_HUSBAND_NAME", OracleDbType.NVarchar2).Value = 
                Data.FatherHusbandName;
            objCmd.Parameters.Add("P_GENDER_ID", OracleDbType.Int64).Value = Data.GenderId;
            objCmd.Parameters.Add("P_DATE_OF_BIRTH", OracleDbType.Date).Value = Data.DateOfBirth;
            objCmd.Parameters.Add("P_MOTHER_MAIDEN_NAME", OracleDbType.NVarchar2).Value = Data.MotherMaidenName;
            objCmd.Parameters.Add("P_CNIC", OracleDbType.NVarchar2).Value = Data.CNIC;
            objCmd.Parameters.Add("P_CNIC_ISSUE_DATE", OracleDbType.Date).Value = Data.CNICIssueDate;
            objCmd.Parameters.Add("P_CNIC_EXPIRY_DATE", OracleDbType.Date).Value = Data.CNICExpiryDate;
            objCmd.Parameters.Add("P_IS_CNIC_LIFETIME", OracleDbType.Int16).Value = Data.IsCNICLifeTime;
            objCmd.Parameters.Add("P_CONTACT_OWNERSHIP_ID", OracleDbType.Int64).Value = Data.ContactOwnershipId;
            objCmd.Parameters.Add("P_EXPECTED_RETIREMENT_AGE", OracleDbType.Int64).Value =
                Data.ExpectedRetirementAge;
            objCmd.Parameters.Add("P_EXPECTED_RETIREMENT_DATE", OracleDbType.Date).Value =
                Data.ExpectedRetirementDate;
            objCmd.Parameters.Add("P_RESIDENTIAL_ADDRESS", OracleDbType.NVarchar2).Value =
                Data.ResidentialAddress;
            objCmd.Parameters.Add("P_IS_RESIDENTIAL_ADDRESS_SAME_AS_CNIC", OracleDbType.Int16).Value =
                Data.IsResidentAdressSameAsCNIC;
            objCmd.Parameters.Add("P_MAILING_ADDRESS", OracleDbType.NVarchar2).Value = Data.MailingAddress;
            objCmd.Parameters.Add("P_MAILING_SAME_AS_RESIDENTIAL", OracleDbType.Int16).Value =
                Data.MailingSameAsResidential;
            objCmd.Parameters.Add("P_RESIDENTIAL_STATUS_ID", OracleDbType.NVarchar2).Value =
                Data.ResidentialStatusId;
            objCmd.Parameters.Add("P_COUNTRY_OF_RESIDENCE_ID", OracleDbType.Int64).Value = 
                Data.CountryOfResidenceId;
            objCmd.Parameters.Add("P_CITY_OF_RESIDENCE_ID", OracleDbType.Int64).Value = Data.CityOfResidenceId;
            objCmd.Parameters.Add("P_AREA_ID", OracleDbType.Int64).Value = Data.AreaId;
            objCmd.Parameters.Add("P_NATIONALITY_ID", OracleDbType.Int64).Value = Data.NationalityId;
            objCmd.Parameters.Add("P_COUNTRY_OF_BIRTH_ID", OracleDbType.Int64).Value = Data.CountryOfBirthId;
            objCmd.Parameters.Add("P_CITY_OF_BIRTH_ID", OracleDbType.Int64).Value = Data.CityOfBirthId;
            objCmd.Parameters.Add("P_IS_POLITICALLY_EXPOSED_PERSON", OracleDbType.Int16).Value =
                Data.IsPoliticallyExposedPerson;
            objCmd.Parameters.Add("P_IS_US_RESIDENT", OracleDbType.Int16).Value = Data.IsUSResident;
            objCmd.Parameters.Add("P_IS_NON_PAK_TAX_RESIDENT", OracleDbType.Int16).Value = 
                Data.IsNonPakTaxResident;
            objCmd.Parameters.Add("P_IS_ZAKAT_DEDUCTION", OracleDbType.Int16).Value = Data.IsZakatDeduction;
            objCmd.Parameters.Add("P_IS_NON_MUSLIM", OracleDbType.Int16).Value = Data.IsNonMuslim;
            objCmd.Parameters.Add("P_USE_ONLINE_BANKING", OracleDbType.Int16).Value = Data.UseOnlineBanking;
            objCmd.Parameters.Add("P_EDUCATION_ID", OracleDbType.Int64).Value = Data.EducationId;
            objCmd.Parameters.Add("P_PROFESSION_ID", OracleDbType.Int64).Value = Data.ProfessionId;
            objCmd.Parameters.Add("P_OCCUPATION_ID", OracleDbType.Int64).Value = Data.OccupationId;
            objCmd.Parameters.Add("P_ANNUAL_INCOME_ID", OracleDbType.Int64).Value = Data.AnnualIncomeId;

            if (Data.SourceOfIncome.Count >= 1 && Data.SourceOfIncome[0] != 0)
                objCmd.Parameters.Add("P_INCOME_SOURCE", OracleDbType.NVarchar2).Value =
                    string.Join(",", Data.SourceOfIncome.Select(n => n.ToString()).ToArray());
            else
                objCmd.Parameters.Add("P_INCOME_SOURCE", OracleDbType.NVarchar2).Value = DBNull.Value;

            objCmd.Parameters.Add("P_NEXT_OF_KIN_NAME", OracleDbType.NVarchar2).Value = Data.NextOfKinName;
            objCmd.Parameters.Add("P_NEXT_OF_KIN_CNIC", OracleDbType.NVarchar2).Value = Data.NextOfKinCNIC;
            objCmd.Parameters.Add("P_NEXT_OF_KIN_RELATIONSHIP", OracleDbType.NVarchar2).Value =
                Data.NextOfKinRelationship;
            objCmd.Parameters.Add("P_NEXT_OF_KIN_MOBILE_NUMBER", OracleDbType.NVarchar2).Value =
                Data.NextOfKinMobileNumber;
            objCmd.Parameters.Add("P_NOMINEE_SHARE", OracleDbType.NVarchar2).Value = Data.NomineeShare;
            objCmd.Parameters.Add("P_IS_HEAD_OF_STATE", OracleDbType.Int16).Value = Data.IsHeadOfState;
            objCmd.Parameters.Add("P_IS_HEAD_OF_GOVT", OracleDbType.Int16).Value = Data.IsHeadOfGovt;
            objCmd.Parameters.Add("P_IS_SENIOR_POLITICIAN", OracleDbType.Int16).Value = 
                Data.IsSeniorPolitician;
            objCmd.Parameters.Add("P_IS_SENIOR_GOVT_OFFICIAL", OracleDbType.Int16).Value = 
                Data.IsSeniorGovtOfficial;
            objCmd.Parameters.Add("P_IS_SENIOR_JUDICIAL_OFFICIAL", OracleDbType.Int16).Value =
                Data.IsSeniorJudicialOfficial;
            objCmd.Parameters.Add("P_IS_SENIOR_MILITARY_OFFICIAL", OracleDbType.Int16).Value =
                Data.IsSeniorMilitaryOfficial;
            objCmd.Parameters.Add("P_IS_SENIOR_EXEC_SOC", OracleDbType.Int16).Value = Data.IsSeniorExecSOC;
            objCmd.Parameters.Add("P_IS_IMPORTANT_POLITICAL_PARTY_OFFICIAL", OracleDbType.Int16).Value =
                Data.IsImportantPoliticalPartyOfficial;
            objCmd.Parameters.Add("P_IS_SENIOR_EXEC_IO", OracleDbType.Int16).Value = Data.IsSeniorExecIO;
            objCmd.Parameters.Add("P_IS_MEMBER_OF_BOIO", OracleDbType.Int16).Value = Data.IsMemberOfBOIO;
            objCmd.Parameters.Add("P_NATURE_OF_PEP_ID", OracleDbType.NVarchar2).Value = Data.NatureOfPEPId;
            objCmd.Parameters.Add("P_PEP_NATURE_OF_DEPARTMENT", OracleDbType.NVarchar2).Value =
                Data.PEPNatureOfDepartment;
            objCmd.Parameters.Add("P_PEP_NAME_OF_FAMILY_MEMBER", OracleDbType.NVarchar2).Value =
                Data.PEPNameOfFamilyMember;
            objCmd.Parameters.Add("P_PEP_NAME_OF_CLOSE_ASSOCIATE", OracleDbType.NVarchar2).Value =
                Data.PEPNameOfCloseAssociate;
            objCmd.Parameters.Add("P_PEP_DESIGNATION", OracleDbType.NVarchar2).Value = Data.PEPDesignation;
            objCmd.Parameters.Add("P_PEP_GRADE", OracleDbType.NVarchar2).Value = Data.PEPGrade;
            objCmd.Parameters.Add("P_W9_NAME", OracleDbType.NVarchar2).Value = Data.W9Name;
            objCmd.Parameters.Add("P_W9_ADDRESS", OracleDbType.NVarchar2).Value = Data.W9Address;
            objCmd.Parameters.Add("P_W9_TIN", OracleDbType.NVarchar2).Value = Data.W9TIN;
            objCmd.Parameters.Add("P_W9_SSN", OracleDbType.NVarchar2).Value = Data.W9SSN;
            objCmd.Parameters.Add("P_W9_EIN", OracleDbType.NVarchar2).Value = Data.W9EIN;
            objCmd.Parameters.Add("P_IS_CERTIFY", OracleDbType.Int16).Value = Data.IsCertify;
            objCmd.Parameters.Add("P_COUNTRY_OF_TAX_ID", OracleDbType.Int64).Value = Data.CountryOfTaxId;
            objCmd.Parameters.Add("P_IS_TIN_AVAILABLE", OracleDbType.Int16).Value = Data.IsTINAvailable;
            objCmd.Parameters.Add("P_TIN_NUMBER", OracleDbType.NVarchar2).Value = Data.TINNumber;
            objCmd.Parameters.Add("P_TIN_REASON_ID", OracleDbType.Int64).Value = Data.TINReasonId;
            objCmd.Parameters.Add("P_TIN_REASON_DETAIL", OracleDbType.NVarchar2).Value = Data.TINReasonDetail;
            objCmd.Parameters.Add("P_IS_RECEIVE_STATEMENT", OracleDbType.Int16).Value = 
                Data.IsReceiveStatememt;
            objCmd.Parameters.Add("P_IS_RE_INVEST", OracleDbType.Int16).Value = Data.IsReinvest;
            objCmd.Parameters.Add("P_CELL_OWNER_CONSENT", OracleDbType.Int16).Value = Data.CellOwnerConsent;
            objCmd.Parameters.Add("P_IS_AGREE_TERMS", OracleDbType.Int16).Value = Data.IsAgreeTerms;
            objCmd.Parameters.Add("P_IS_FINAL_SUBMIT", OracleDbType.Int16).Value = Data.IsFinalSubmit;

            objCmd.Parameters.Add("P_APPLICATION_STATUS_ID", OracleDbType.Int64).Value =
                Data.IsFinalSubmit ? 1 : Data.ApplicationStatusId;
            objCmd.Parameters.Add("P_SALES_REPRESENTATIVE_NAME_CODE", OracleDbType.Varchar2).Value = Data.SalesRepresentativeNameCode;
            objCmd.Parameters.Add("P_SALES_REPRESENTATIVE_MOBILE", OracleDbType.Varchar2).Value = Data.SalesRepresentativeMobileNumber;
            objCmd.Parameters.Add("P_DEBT_SUB_FUND", OracleDbType.Int64).Value = Data.DebtSubFund;
            objCmd.Parameters.Add("P_EQUITY_SUB_FUND", OracleDbType.Int64).Value = Data.EquitySubFund;
            objCmd.Parameters.Add("P_MONEY_MARKET_SUB_FUND", OracleDbType.Int64).Value = Data.MoneyMarketSubFund;
            objCmd.Parameters.Add("P_MODE_OF_CONTRIBUTION", OracleDbType.Int64).Value = Data.ModeOfContribution;
            objCmd.Parameters.Add("P_INITIAL_CONTRIBUTION_AMOUNT", OracleDbType.Int64).Value = Data.InitialContributionAmount;
            objCmd.Parameters.Add("P_AMOUNT_IN_WORDS", OracleDbType.Varchar2).Value = Data.AmountInWords;
            objCmd.Parameters.Add("P_FRONTEND_LOAD", OracleDbType.Int64).Value = Data.FrontEndLoad;
            objCmd.Parameters.Add("P_CONTRIBUTION_PAYMENT_MODE", OracleDbType.Int64).Value = Data.ContributionPaymentMode;
            objCmd.Parameters.Add("P_CONTRIBUTION_REF_NO", OracleDbType.Varchar2).Value = Data.ContributionReferenceNumber;
            objCmd.Parameters.Add("P_DRAWN_ON", OracleDbType.Varchar2).Value = Data.DrawnOn;
            objCmd.Parameters.Add("P_CONTRIBUTION_FREQUENCY", OracleDbType.Int64).Value = Data.ContributionFrequency;
            objCmd.Parameters.Add("P_PERIODIC_CONTRIBUTION_AMOUNT", OracleDbType.Int64).Value = Data.PeriodicContributionAmount;
            objCmd.Parameters.Add("P_YEARLY_CONTRIBUTION_AMOUNT", OracleDbType.Int64).Value = Data.YearlyContributionAmount;
            objCmd.Parameters.Add("P_ON_BEHALF_OF_ANOTHER_PERSON", OracleDbType.Int16).Value = Data.OnBehalfOfAnotherPerson;
            objCmd.Parameters.Add("P_CONNECTION_TAX_HAVENS", OracleDbType.Int16).Value = Data.ConnectionTaxHavens;
            objCmd.Parameters.Add("P_DEALING_IN_HIGH_VALUE_ITEMS", OracleDbType.Int16).Value = Data.DealingInHighValueItems;
            objCmd.Parameters.Add("P_HAS_FINANCIAL_INSTITUTION_REFUSED_ACCOUNT", OracleDbType.Int16).Value = Data.HasFinancialInstRefusedToOpenAccount;
            objCmd.Parameters.Add("P_TOTAL_SCORE", OracleDbType.Int64).Value = Data.TotalScore;
            objCmd.Parameters.Add("P_RISK_PROFILE", OracleDbType.Clob).Value = Data.RiskProfile;
        }

        public async Task<UserApplication> UpdateInitialInvestment(int UserApplicationId, int InvestmentRequestID)
        {
            await conn.OpenAsync();

            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".UPDATE_USER_APPLICATION_INVESTMENT", conn);
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.Add("P_USER_APPLICATION_ID", OracleDbType.Int64).Value = UserApplicationId;
            objCmd.Parameters.Add("P_INVESTMENT_REQUEST_ID", OracleDbType.Int64).Value = InvestmentRequestID;
            objCmd.Parameters.Add("P_RECORD_COUNT", OracleDbType.Int64, ParameterDirection.Output);

            await objCmd.ExecuteNonQueryAsync();

            int count = int.Parse(objCmd.Parameters["P_RECORD_COUNT"].Value.ToString());
            await conn.CloseAsync();

            var UserApplication = await FindAsync(UserApplicationId);

            return UserApplication;
        }
    }
}
