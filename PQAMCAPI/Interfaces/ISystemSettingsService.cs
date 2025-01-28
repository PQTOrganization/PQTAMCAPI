using PQAMCAPI.Models;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces
{
    public interface ISystemSettingsService
    {
        Task<SystemSettings> GetSystemSettingsAsync(string SettingName);
    }
}
