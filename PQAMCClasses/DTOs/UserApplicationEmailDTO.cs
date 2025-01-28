namespace PQAMCClasses.DTOs
{
    public class UserApplicationEmailDTO
    {
        public int UserApplicationId { get; set; }
        public int UserId { get; set; }
        public string? FolioNumber { get; set; }
        public int? AccountCategoryId { get; set; }
        public int? FundPreferenceId { get; set; }
        public int? VPSFundId { get; set; }
        public string? Name { get; set; }
        public string? FatherHusbandName { get; set; }
        public int? GenderId { get; set; }
        public string GenderName { get; set; } = "";
        public DateTime? DateOfBirth { get; set; }
        public int? Age { get; set; }
        public string? MotherMaidenName { get; set; }
        public string? CNIC { get; set; }
        public DateTime? CNICIssueDate { get; set; }
        public DateTime? CNICExpiryDate { get; set; }
        public bool? IsCNICLifeTime { get; set; }
        public int? ContactOwnershipId { get; set; }
        public string ContactOwnershipName { get; set; } = ""; 
        public int? ExpectedRetirementAge { get; set; }
        public DateTime? ExpectedRetirementDate { get; set; }
        public string? ResidentialAddress { get; set; }
        public bool? IsResidentAdressSameAsCNIC { get; set; }
        public string? MailingAddress { get; set; }
        public bool? MailingSameAsResidential { get; set; }
        public int? ResidentialStatusId { get; set; }
        public string ResidentialStatusName { get; set; } = "";
        public int? CountryOfResidenceId { get; set; }
        public string CountryOfResidence { get; set; } = "";        
        public int? CityOfResidenceId { get; set; }
        public string CityOfResidence { get; set; } = "";
        public int? AreaId { get; set; }
        public int? NationalityId { get; set; }
        public int? CountryOfBirthId { get; set; }
        public string CountryOfBirth { get; set; } = "";
        public int? CityOfBirthId { get; set; }
        public string CityOfBirth { get; set; } = "";
        public bool? IsPoliticallyExposedPerson { get; set; }
        public bool? IsUSResident { get; set; }
        public string USResident
        {
            get
            {
                if ((bool)IsUSResident)
                    return "Yes";
                return "No";
            }
        }
        public bool? IsNonPakTaxResident { get; set; }
        public bool? IsZakatDeduction { get; set; }
        public string ZakatStatus
        {
            get
            {
                if ((bool)IsZakatDeduction)
                    return "Liable";
                else
                    return "No";               
            }
        }
        public bool? IsNonMuslim { get; set; }

        public int UserBankId { get; set; } = 0;
        public int? BankId { get; set; }
        public string BankName { get; set; } = "";
        public string? IBANNumber { get; set; }
        public bool? IsIBANVerified { get; set; }
        public string? OneLinkTitle { get; set; }
        public bool? UseOnlineBanking { get; set; }

        public int? EducationId { get; set; }
        public string EducationName { get; set; } = "";
        public int? ProfessionId { get; set; }
        public int? OccupationId { get; set; }
        public string OccupationName { get; set; } = "";
        public int? AnnualIncomeId { get; set; }
        public string AnnualIncomeName { get; set; } = "";
        public List<int> SourceOfIncome { get; set; } = new List<int>();
        public string SourceOfIncomeName{ get; set; } = "";
       
        public string? NextOfKinName { get; set; }
        public string? NextOfKinCNIC { get; set; }
        public string? NextOfKinRwc { get; set; }
        public string? NextOfKinMobileNumber { get; set; }
        public string? NomineeShare { get; set; }

        public bool? IsHeadOfState { get; set; }
        public bool? IsHeadOfGovt { get; set; }
        public bool? IsSeniorPolitician { get; set; }
        public bool? IsSeniorGovtOfficial { get; set; }
        public bool? IsSeniorJudicialOfficial { get; set; }
        public bool? IsSeniorMilitaryOfficial { get; set; }
        public bool? IsSeniorExecSOC { get; set; }
        public bool? IsImportantPoliticalPartyOfficial { get; set; }
        public bool? IsSeniorExecIO { get; set; }
        public bool? IsMemberOfBOIO{ get; set; }
        public int? NatureOfPEPId { get; set; }
        public string? PEPNatureOfDepartment { get; set; }
        public string? PEPNameOfFamilyMember { get; set; }
        public string? PEPNameOfCloseAssociate { get; set; }
        public string? PEPDesignation { get; set; }
        public string? PEPGrade { get; set; }

        public string? W9Name { get; set; }
        public string? W9Address { get; set; }
        public string? W9TIN { get; set; }
        public string? W9SSN { get; set; }
        public string? W9EIN { get; set; }
        public bool? IsCertify { get; set; }

        public int? CountryOfTaxId { get; set; }
        public string CountryOfTaxName { get; set; } = "";
        public bool? IsTINAvailable { get; set; }
        public string? TINNumber { get; set; }
        public int? TINReasonId { get; set; }
        public string? TINReasonDetail { get; set; }

        public bool? IsReceiveStatememt { get; set; }
        public bool? IsReinvest { get; set; }
        public bool? CellOwnerConsent { get; set; }
        public bool? IsAgreeTerms { get; set; }

        public bool IsFinalSubmit { get; set; }
        public int ApplicationStatusId { get; set; }
        public DateTime? CreatedOn { get; set; } = null;
        public DateTime? ModifiedOn { get; set; } = null;
        public string MobileNumber { get; set; } = ""; 
        public string EmailAddress { get; set; } = "";
    }
}
