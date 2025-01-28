using System.Data;
using PQAMCAPI.Interfaces.Services;
using PQAMCAPI.Models;

namespace PQAMCAPI.Services.Domain
{
    public class AreaService : IAreaService
    {
        const string PACKAGE_NAME = "AMC_AREA_PKG";

        private readonly IStoreProcedureService _spService;

        public AreaService(IStoreProcedureService spService, PQAMCAPIContext dbContext)
        {
            _spService = spService;
        }

        public Task<int> DeleteArea(int areaId)
        {
            var rowCount = _spService.DeleteSP<Area>(PACKAGE_NAME + ".del_area", areaId);
            return rowCount;
        }

        public Task<Area> FindAsync(int areaId)
        {
            var area = _spService.GetSP<Area>(PACKAGE_NAME + ".get_area", areaId);
            return area;
        }

        public Task<List<Area>> GetAllAsync()
        {
            var areas = _spService.GetAllSP<Area>(PACKAGE_NAME + ".get_areas", -1);
            return areas;
        }

        public async Task<List<Area>> GetAreasByCity(int cityId)
        {
            var areas = await _spService.GetAllSP<Area>(PACKAGE_NAME + ".get_areas", -1);
            return areas.Where(x => x.CityId == cityId).ToList();
        }

        public Task<Area> InsertArea(Area area)
        {
            throw new NotImplementedException();
        }

        public Task<Area> UpdateArea(int areaId, Area area)
        {
            throw new NotImplementedException();
        }
    }
}
