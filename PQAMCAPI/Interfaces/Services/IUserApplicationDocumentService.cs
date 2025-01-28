using PQAMCClasses.DTOs;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IUserApplicationDocumentService
    {
        Task<List<UserApplicationDocumentDTO>> GetAllUserApplicationDocuments(int UserApplicationId);
        Task<UserApplicationDocumentDTO> GetUserApplicationDocument(int UserApplicationDocumentId);
        Task<UserApplicationDocumentDTO> InsertDocument(UserApplicationDocumentDTO Document);
        Task<UserApplicationDocumentDTO> DeleteDocument(int UserApplicationDocumentId);
    }
}
