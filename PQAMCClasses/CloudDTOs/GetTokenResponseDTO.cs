using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace PQAMCClasses.CloudDTOs
{
    public class CredentialsDTO
    {
        public string userId { get; set; }
        public string password { get; set; }
        public string channel { get; set; }
        public string requestTime { get; set; }
        public string brandName { get; set; }
    }
    public class GetTokenResponseDTO
    {
        public string ExpiryTime { get; set; }
        public string ResponseCode { get; set; } = string.Empty;
        public string ResponseMessage { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public int DBRequestID { get; set; }
    }

    public class GetRefreshTokenResponseDTO
    {
        public string ExpiryTime { get; set; }
        public string ResponseCode { get; set; } = string.Empty;
        public string ResponseMessage { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public int DBRequestID { get; set; }
    }
}
