using PQAMCAPI.Models;

namespace PQAMCAPI.Interfaces.Services
{
    public interface IProfessionService
    {
        Task<List<Profession>> GetAllAsync();
        Task<Profession> FindAsync(int professionId);
        Task<Profession> InsertProfession(Profession profession);
        Task<Profession> UpdateProfession(int professionId, Profession profession);
        Task<int> DeleteProfession(int professionId);



    }
}
