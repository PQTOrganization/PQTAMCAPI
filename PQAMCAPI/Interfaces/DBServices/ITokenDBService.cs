using PQAMCAPI.Models;

namespace PQAMCAPI.Interfaces.Services
{
    public interface ITokenDBService
    {
        Task<UserRefreshToken> InsertToken(UserRefreshToken userToken);
        Task<UserRefreshToken> UpdateToken(UserRefreshToken userToken);
        Task<bool> InsertExpireToken(UserToken userToken);
        Task<bool> IsExpireToken(string userToken);
    }
}
