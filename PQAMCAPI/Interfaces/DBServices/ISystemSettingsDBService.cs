using PQAMCAPI.Models;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface ISystemSettingsDBService
    {
        Task<SystemSettings> GetSystemSettingsAsync(string SettingName);
    }
}
