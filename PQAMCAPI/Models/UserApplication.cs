using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("AMC_USER_APPLICATION")]
    public partial class UserApplication
    {
        public UserApplication()
        {
            // This done to handle front end
            SourceOfIncome = new List<int>() { 0 }; 
        }

        [Column("USER_APPLICATION_ID")]
        public int UserApplicationId { get; set; }

        [Column("USER_ID")]
        public int UserId { get; set; }

        public string? FolioNumber { get; set; }

        [Column("ACCOUNT_CATEGORY_ID")]
        public int? AccountCategoryId { get; set; }

        [Column("FUND_PREFERENCE_ID")]
        public int? FundPreferenceId { get; set; }

        [Column("VPS_FUND_ID")]
        public int? VPSFundId { get; set; }

        [Column("DEBT_SUB_FUND")]
        public int? DebtSubFund { get; set; }

        [Column("EQUITY_SUB_FUND")]
        public int? EquitySubFund { get; set; }

        [Column("MONEY_MARKET_SUB_FUND")]
        public int? MoneyMarketSubFund { get; set; }

        [Column("NAME")]
        public string? Name { get; set; }

        [Column("FATHER_HUSBAND_NAME")]
        public string? FatherHusbandName { get; set; }

        [Column("GENDER_ID")]
        public int? GenderId { get; set; }

        [Column("DATE_OF_BIRTH")]
        public DateTime? DateOfBirth { get; set; }

        [Column("MOTHER_MAIDEN_NAME")]
        public string? MotherMaidenName { get; set; }

        [Column("CNIC")]
        public string? CNIC { get; set; }

        [Column("CNIC_ISSUE_DATE")]
        public DateTime? CNICIssueDate { get; set; }

        [Column("CNIC_EXPIRY_DATE")]
        public DateTime? CNICExpiryDate { get; set; }

        [Column("IS_CNIC_LIFETIME")]
        public bool? IsCNICLifeTime { get; set; }

        [Column("CONTACT_OWNERSHIP_ID")]
        public int? ContactOwnershipId { get; set; }

        [Column("EXPECTED_RETIREMENT_AGE")]
        public int? ExpectedRetirementAge { get; set; }

        [Column("EXPECTED_RETIREMENT_DATE")]
        public DateTime? ExpectedRetirementDate { get; set; }

        [Column("RESIDENTIAL_ADDRESS")]
        public string? ResidentialAddress { get; set; }

        [Column("IS_RESIDENTIAL_ADDRESS_SAME_AS_CNIC")]
        public bool? IsResidentAdressSameAsCNIC { get; set; }

        [Column("MAILING_ADDRESS")]
        public string? MailingAddress { get; set; }

        [Column("MAILING_SAME_AS_RESIDENTIAL")]
        public bool? MailingSameAsResidential { get; set; }

        [Column("RESIDENTIAL_STATUS_ID")]
        public int? ResidentialStatusId { get; set; }

        [Column("COUNTRY_OF_RESIDENCE_ID")]
        public int? CountryOfResidenceId { get; set; }

        [Column("CITY_OF_RESIDENCE_ID")]
        public int? CityOfResidenceId { get; set; }

        [Column("AREA_ID")]
        public int? AreaId { get; set; }

        [Column("NATIONALITY_ID")]
        public int? NationalityId { get; set; }

        [Column("COUNTRY_OF_BIRTH_ID")]
        public int? CountryOfBirthId { get; set; }

        [Column("CITY_OF_BIRTH_ID")]
        public int? CityOfBirthId { get; set; }

        [Column("IS_POLITICALLY_EXPOSED_PERSON")]
        public bool? IsPoliticallyExposedPerson { get; set; }

        [Column("IS_US_RESIDENT")]
        public bool? IsUSResident { get; set; }

        [Column("IS_NON_PAK_TAX_RESIDENT")]
        public bool? IsNonPakTaxResident { get; set; }

        [Column("IS_ZAKAT_DEDUCTION")]
        public bool? IsZakatDeduction { get; set; }

        [Column("IS_NON_MUSLIM")]
        public bool? IsNonMuslim { get; set; }


        [Column("USE_ONLINE_BANKING")]
        public bool? UseOnlineBanking { get; set; }

        [Column("EDUCATION_ID")]
        public int? EducationId { get; set; }
        
        [Column("PROFESSION_ID")]
        public int? ProfessionId { get; set; }
        
        [Column("OCCUPATION_ID")]
        public int? OccupationId { get; set; }
        
        [Column("ANNUAL_INCOME_ID")]
        public int? AnnualIncomeId { get; set; }

        [Column("INCOME_SOURCE")]
        public List<int> SourceOfIncome { get; set; }

        [Column("NEXT_OF_KIN_NAME")]
        public string? NextOfKinName { get; set; }

        [Column("NEXT_OF_KIN_CNIC")]
        public string? NextOfKinCNIC { get; set; }

        [Column("NEXT_OF_KIN_RELATIONSHIP")]
        public string? NextOfKinRelationship { get; set; }

        [Column("NEXT_OF_KIN_MOBILE_NUMBER")]
        public string? NextOfKinMobileNumber { get; set; }

        [Column("NOMINEE_SHARE")]
        public string? NomineeShare { get; set; }

        [Column("IS_HEAD_OF_STATE")]
        public bool? IsHeadOfState { get; set; }

        [Column("IS_HEAD_OF_GOVT")]
        public bool? IsHeadOfGovt { get; set; }

        [Column("IS_SENIOR_POLITICIAN")]
        public bool? IsSeniorPolitician { get; set; }

        [Column("IS_SENIOR_GOVT_OFFICIAL")]
        public bool? IsSeniorGovtOfficial { get; set; }

        [Column("IS_SENIOR_JUDICIAL_OFFICIAL")]
        public bool? IsSeniorJudicialOfficial { get; set; }

        [Column("IS_SENIOR_MILITARY_OFFICIAL")]
        public bool? IsSeniorMilitaryOfficial { get; set; }

        [Column("IS_SENIOR_EXEC_SOC")]
        public bool? IsSeniorExecSOC { get; set; }

        [Column("IS_IMPORTANT_POLITICAL_PARTY_OFFICIAL")]
        public bool? IsImportantPoliticalPartyOfficial { get; set; }

        [Column("IS_SENIOR_EXEC_IO")]
        public bool? IsSeniorExecIO { get; set; }

        [Column("IS_MEMBER_OF_BOIO")]
        public bool? IsMemberOfBOIO { get; set; }

        [Column("NATURE_OF_PEP_ID")]
        public int? NatureOfPEPId { get; set; }

        [Column("PEP_NATURE_OF_DEPARTMENT")]
        public string? PEPNatureOfDepartment { get; set; }

        [Column("PEP_NAME_OF_FAMILY_MEMBER")]
        public string? PEPNameOfFamilyMember { get; set; }

        [Column("PEP_NAME_OF_CLOSE_ASSOCIATE")]
        public string? PEPNameOfCloseAssociate { get; set; }

        [Column("PEP_DESIGNATION")]
        public string? PEPDesignation { get; set; }

        [Column("PEP_GRADE")]
        public string? PEPGrade { get; set; }

        [Column("W9_NAME")]
        public string? W9Name { get; set; }

        [Column("W9_ADDRESS")]
        public string? W9Address { get; set; }

        [Column("W9_TIN")]
        public string? W9TIN { get; set; }

        [Column("W9_SSN")]
        public string? W9SSN { get; set; }

        [Column("W9_EIN")]
        public string? W9EIN { get; set; }

        [Column("IS_CERTIFY")]
        public bool? IsCertify { get; set; }

        [Column("COUNTRY_OF_TAX_ID")]
        public int? CountryOfTaxId { get; set; }

        [Column("IS_TIN_AVAILABLE")]
        public bool? IsTINAvailable { get; set; }

        [Column("TIN_NUMBER")]
        public string? TINNumber { get; set; }

        [Column("TIN_REASON_ID")]
        public int? TINReasonId { get; set; }

        [Column("TIN_REASON_DETAIL")]
        public string? TINReasonDetail { get; set; }

        [Column("IS_RECEIVE_STATEMENT")]
        public bool? IsReceiveStatememt { get; set; }
        
        [Column("IS_RE_INVEST")]        
        public bool? IsReinvest { get; set; }
        
        [Column("CELL_OWNER_CONSENT")]
        public bool? CellOwnerConsent { get; set; }
        
        [Column("IS_AGREE_TERMS")]
        public bool? IsAgreeTerms { get; set; }

        [Column("IS_FINAL_SUBMIT")]
        public bool IsFinalSubmit { get; set; }

        [Column("APPLICATION_STATUS_ID")]
        public int ApplicationStatusId { get; set; }

        [Column("CREATED_ON")]
        public DateTime? CreatedOn { get; set; }

        [Column("MODIFIED_ON")]
        public DateTime? ModifiedOn { get; set; }

        [Column("SALES_REPRESENTATIVE_NAME_CODE")]
        public string SalesRepresentativeNameCode { get; set; }

        [Column("SALES_REPRESENTATIVE_MOBILE")]
        public string SalesRepresentativeMobileNumber { get; set; }
        [Column("INITIAL_INVESTMENT_REQUEST_ID")]
        public int? InitialInvestmentRequestID { get; set; }

        [Column("MODE_OF_CONTRIBUTION")]
        public short? ModeOfContribution { get; set; }

        [Column("INITIAL_CONTRIBUTION_AMOUNT")]
        public decimal? InitialContributionAmount { get; set; }

        [Column("AMOUNT_IN_WORDS")]
        public string? AmountInWords{ get; set; }

        [Column("FRONTEND_LOAD")]
        public int? FrontEndLoad { get; set; }

        [Column("CONTRIBUTION_PAYMENT_MODE")]
        public short? ContributionPaymentMode { get; set; }

        [Column("CONTRIBUTION_REF_NO")]
        public string? ContributionReferenceNumber { get; set; }

        [Column("DRAWN_ON")]
        public string? DrawnOn { get; set; }

        [Column("CONTRIBUTION_FREQUENCY")]
        public int? ContributionFrequency { get; set; }

        [Column("PERIODIC_CONTRIBUTION_AMOUNT")]
        public decimal? PeriodicContributionAmount { get; set; }

        [Column("YEARLY_CONTRIBUTION_AMOUNT")]
        public decimal? YearlyContributionAmount { get; set; }

        [Column("ON_BEHALF_OF_ANOTHER_PERSON")]
        public bool? OnBehalfOfAnotherPerson { get; set; }

        [Column("CONNECTION_TAX_HAVENS")]
        public bool? ConnectionTaxHavens { get; set; }

        [Column("DEALING_IN_HIGH_VALUE_ITEMS")]
        public bool? DealingInHighValueItems { get; set; }

        [Column("HAS_FINANCIAL_INSTITUTION_REFUSED_ACCOUNT")]
        public bool? HasFinancialInstRefusedToOpenAccount { get; set; }

        [Column("TOTAL_SCORE")]
        public int? TotalScore { get; set; }

        [Column("RISK_PROFILE")]
        public string? RiskProfile { get; set; }
    }
}