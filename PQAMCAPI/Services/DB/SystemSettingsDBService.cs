using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;

namespace PQAMCAPI.Services.DB
{
    public class SystemSettingsDBService : ISystemSettingsDBService
    {
        private readonly IStoreProcedureService _spService;
        
        public SystemSettingsDBService(IStoreProcedureService spService)
        {
            _spService = spService;
        }

        public async Task<SystemSettings> GetSystemSettingsAsync(string SettingName)
        {
            var SysSettings = await _spService.GetSP<SystemSettings>("SP_AMC_GET_SETTINGS", SettingName);
            return SysSettings;
        }
    }
}
