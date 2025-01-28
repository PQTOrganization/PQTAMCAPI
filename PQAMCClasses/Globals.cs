using System.ComponentModel.DataAnnotations;

namespace PQAMCClasses
{
    public static class Globals
    {
        public enum PaymentModes : short
        {
            Cheque = 2,
            //IBFT = 3,
            [Display(Name = "Online Transfer/IBFT")]
            OnlineTransfer = 4,
            RTGS = 5,
            Blinq = 6,
        }

        public enum RequestStatus : short
        {
            Pending = 1,
            Approved,
            Rejected,
            Posted,
            Paid,
            Cancelled
        }

        public enum ApplicationStatus : short
        {
            Pending = 0,
            Submitted,
            Discrepancy,
            Approved,
            Rejected,
            Posted,
            InvestmentSubmitted,
            InvestmentApproved,
            InvestmentRejected,
            InvestmentCancelled //?
        }

        public enum SMSOperations: short
        { 
            OTP = 1
        }

        public static string GetFolioNumber(string FolioNumber)
        {
            return FolioNumber;
            //return FolioNumber.ToString().PadLeft(7, '0');
        }

        public static class Constants
        {
            public static string ITMindsDateTimeFormat = "dd/MM/yyyy hh:mm:ss";
            public static string DateFormat = "dd/MM/yyyy";
            public static int SMSCount = 3;
            public static string[] InvestmentAmountAddTxnList = { "Sale", "Sale of Unit Instruction", "Online Sale (A)", "Conversion In" };
            public static string[] InvestmentAmountSubtractTxnList = { "Redemption", "Redemption of Unit Instruction", "Online Redemption (B)", "Conversion Out" };
            public static int OTPRetryLimit = 5;
        }

        public static int? GetAgeFromDOB(DateTime? DateOfBirth)
        {
            
            int? age = 0;
            if (DateOfBirth != null)
            {
                // Save today's date.
                var today = DateTime.Today;

                // Calculate the age.
                age = today.Year - DateOfBirth?.Year;

                // Go back to the year in which the person was born in case of a leap year
                if (DateOfBirth?.Date > today.AddYears((int)-age)) age--;
            }

            return age;
        }

        public static string GetFormattedCNIC(string CNIC)
        {
            if (!String.IsNullOrEmpty(CNIC))
            {
                string FormattedCNIC = CNIC.Substring(0, 5) + "-" + CNIC.Substring(5, CNIC.Length - 6) + "-" + CNIC.Substring(CNIC.Length - 1);
                return FormattedCNIC;
            }
            return CNIC;
        }

        public static int[] VPSAccountCategories = new int[2] { 2, 4 };

        public static class ErrorMessages
        {
            public static string TooManyOTPRequests = "Please wait before sending another request for OTP";
            public static string NAVNotAvailable = "Error in fetching NAV";
            public static string NoBanksFound = "No banks found";
            public static string NotFound = "Not Found";
            public static string AccountLockedDueToIncorrectOTPAttempts = "Your account is locked due to incorrect attempts.";
        }

        public static class SuccessMessages
        {
            public static string FetchedAcctStmnt = "Account Statements fetched scucessfully.";
        }

        public static class ITMindsKey
        {
            public static string CISKey = "2";
            public static string VPSKey = "3";
        }

        public static class VPSFundListAPITypes
        {
            public static string SubSale = "SubSale";
        }

        public static class SessionKeys
        {
            public static string AccountCategoryID = "AccountCategoryID";
            public static string FolioList = "FolioList";
            public static string SecurityKeys = "SecurityKeys";
        }

        public static class AccountCategories
        {
            public static int CIS = 1;
        }

        public static string[] LoggedOutPages = new string[1] { "login" };

        public static class AccountStatus
        {
            public static string Active = "ACTIVE";
            public static string Locked = "LOCKED";
        }
    }
}
