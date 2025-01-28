using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PQAMCClasses.DTOs
{
    public class AccessToken
    {
        public String Token { get; set; }
        public String RefreshToken { get; set; }
        public DateTime ExpiryDate { get; set; }
        public String Issuer { get; set; }

        public AccessToken(String _accessToken, String _refreshToken, DateTime _expiryDate, String _issuer)
        {
            Token = _accessToken;
            RefreshToken = _refreshToken;
            ExpiryDate = _expiryDate;
            Issuer = _issuer;
        }
    }
}
