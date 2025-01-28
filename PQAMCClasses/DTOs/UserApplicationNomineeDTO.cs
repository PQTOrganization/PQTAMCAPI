using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCClasses.DTOs
{
    public class UserApplicationNomineeDTO
    {
        public int UserApplicationNomineeId { get; set; }

        public int UserApplicationId { get; set; }

        public int SerialNumber { get; set; }

        public string? Name { get; set; }

        public string? Relationship { get; set; }

        public int? Share { get; set; }

        public string? ResidentialAddress { get; set; }

        public string? TelephoneNumber { get; set; }

        public string? BankAccountDetail { get; set; }
        public string? CNIC { get; set; }
    }
}
