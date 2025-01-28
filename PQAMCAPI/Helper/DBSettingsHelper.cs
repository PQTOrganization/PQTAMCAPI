using Microsoft.Extensions.Configuration;
using System;

namespace Helper
{
    public static class DBSettingsHelper
    {
        public static string GetConnectionString(IConfiguration Config, string EntryName="DBSettings")
        {
            string ConnStr = "";

            IConfigurationSection Section = Config.GetSection(EntryName);

            if (Section != null)
            {
                ConnStr = "Data Source=" + Section.GetValue<String>("Server", "");
                ConnStr += ":" + Section.GetValue<Int16>("Port", 0);
                ConnStr += "/" + Section.GetValue<String>("Database", "");
                ConnStr += ";User ID=" + Section.GetValue<String>("UserID", "");
                ConnStr += ";Password=" + Section.GetValue<String>("Password", "");
            }
            return ConnStr;
        }

        public static string GetOracleSchemaName(IConfiguration Config, string EntryName = "DBSettings")
        {
            IConfigurationSection Section = Config.GetSection(EntryName);

            if (Section != null)
                return Section.GetValue<String>("UserID", "");
            else
                return "";
        }
    }
}