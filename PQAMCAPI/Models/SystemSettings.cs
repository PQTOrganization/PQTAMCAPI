using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PQAMCAPI.Models
{
    [Table("amc_settings")]
    public partial class SystemSettings
    {
        public SystemSettings()
        {
            NotificationCCEmail = "";
            NotificationToEmail = "";
        }

        [Column("NOTIFICATION_TO_EMAIL")]
        public string NotificationToEmail { get; set; }

        [Column("NOTIFICATION_CC_EMAIL")]
        public string NotificationCCEmail { get; set; }        
    }
}
