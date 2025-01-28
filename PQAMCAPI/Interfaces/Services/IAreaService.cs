using PQAMCAPI.Models;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IAreaService
    {
        Task<List<Area>> GetAllAsync();
        Task<List<Area>> GetAreasByCity(int cityId);
        Task<Area> FindAsync(int areaId);
        Task<Area> InsertArea(Area area);
        Task<Area> UpdateArea(int areaId, Area area);
        Task<int> DeleteArea(int areaId);



    }
}
