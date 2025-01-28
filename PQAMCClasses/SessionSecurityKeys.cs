using PQAMCClasses.DTOs;

namespace PQAMCClasses
{
    public class SessionSecurityKeys
    {
        public List<UserFolioDTO> FolioList { get; set; }
        public bool IsAdmin { get; set; } = false;
        public string UserApplicationID { get; set; }
        public string CNIC { get; set; }
        public int UserId { get; set; }
    }
}
