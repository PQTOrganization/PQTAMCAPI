namespace PQAMCClasses.DTOs
{
    public class UserAuthData
    {
        public int UserId { get; set; }
        public string MobileNumber { get; set; } = "";
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Email { get; set; } = "";
        public string ProfileImage { get; set; } = "";
        public DateTime RegisterationDate { get; set; }
        public AccessToken TokenInfo { get; set; }

        public int UserApplicationId { get; set; }
        public int ApplicationStatusId { get; set; }
        public string FolioNumber { get; set; }
        public List<UserFolioDTO> FolioList { get; set; } = new List<UserFolioDTO>();
        public int? AccountCategoryID { get; set; }
    }
}
