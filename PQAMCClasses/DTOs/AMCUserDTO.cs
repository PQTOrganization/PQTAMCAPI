namespace PQAMCClasses.DTOs
{
    public class AMCUserDTO
    {
        public string FolioNumber { get; set; } = "";
        public string AccountTitle { get; set; } = "";
        public short IsActive { get; set; }
        public string CNIC { get; set; } = "";
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string MobileNumber { get; set; } = "";
        public string EmailAddress { get; set; } = "";
    }
}
