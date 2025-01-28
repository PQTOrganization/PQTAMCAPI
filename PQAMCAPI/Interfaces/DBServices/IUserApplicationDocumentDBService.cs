using PQAMCAPI.Models;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IUserApplicationDocumentDBService
    {
        Task<List<UserApplicationDocumentDTO>> GetAllUserApplicationDocuments(int UserApplicationId);
        Task<UserApplicationDocument> GetApplicationDocument(int UserApplicationDocumentId);
        Task<UserApplicationDocument> InsertDocument(UserApplicationDocument Document);
        Task<UserApplicationDocument> DeleteDocument(int ApplicationDocumentId);
        Task<UserApplicationDocument> DeleteDocument(int UserApplicationId, int DocumentId);
    }
}
