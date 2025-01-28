using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("amc_sms_log")]
    public class SMSLog
    {
        public SMSLog()
        {
        }

        [Column("MOBILE_NUMBER")]
        public string? MobileNumber { get; set; }

        [Column("OPERATION")]
        public int Operation { get; set; }

        [Column("SMS_DATE")]
        public DateTime SMSDate { get; set; }

        [Column("USER_ID")]
        public int? UserID { get; set; }

        [Column("SMS_LOG_ID")]
        public int SMSLogID { get; set; }

    }
}