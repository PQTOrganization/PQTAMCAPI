using PQAMCAPI.Models;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IResidentialStatusService
    {
        Task<List<ResidentialStatus>> GetAllAsync();
        Task<ResidentialStatus> FindAsync(int residentialStatusId);
        Task<ResidentialStatus> InsertResidentialStatus(ResidentialStatus residentialStatus);
        Task<ResidentialStatus> UpdateResidentialStatus(int residentialStatusId, ResidentialStatus residentialStatus);
        Task<int> DeleteResidentialStatus(int residentialStatusId);



    }
}
