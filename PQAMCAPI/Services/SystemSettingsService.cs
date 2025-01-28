using Classes;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NuGet.Configuration;
using PQAMCAPI.Interfaces;
using PQAMCAPI.Interfaces.Services;
using PQAMCAPI.Models;

namespace Services
{
    public class SystemSettingsService : ISystemSettingsService
    {
        public ISystemSettingsDBService _dbService;

        public SystemSettingsService(ISystemSettingsDBService dbService)
        {
            _dbService = dbService;
        }

        public async Task<SystemSettings> GetSystemSettingsAsync(string SettingName)
        {
            return await _dbService.GetSystemSettingsAsync(SettingName);
        }
    }
}
