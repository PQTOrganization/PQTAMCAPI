using PQAMCAPI.Models;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IDocumentDBService
    {
        Task<List<Document>> GetAllAsync();
    }
}
