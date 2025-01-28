using PQAMCAPI.Models;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface ISMSLogDBService
    {
        Task<SMSLog> InsertSMSLog(SMSLog smsLog);

        Task<int> GetSMSCount(string MobileNumber, DateTime StartDateTime, DateTime EndDateTime);
    }
}
