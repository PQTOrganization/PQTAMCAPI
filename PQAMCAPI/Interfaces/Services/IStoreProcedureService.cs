using PQAMCAPI.Models;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IStoreProcedureService
    {
        Task<T> InsertSP<T>(string spName, T movie);
        Task<T> GetSP<T>(string spName, int Id);
        Task<T> GetSP<T>(string spName, string Key);
        Task<List<T>> GetAllSP<T>(string spName, int Id = -1);
        Task<List<T>> GetAllSP<T>(string spName, string Id = "");
        Task<int> DeleteSP<T>(string spName, int Id);
    }
        
}
