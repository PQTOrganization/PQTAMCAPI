using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using PQAMCClasses.DTOs;
using API.Classes;
using Newtonsoft.Json;
using PQAMCClasses;

namespace PQAMCAPI.Services.Domain
{
    public class AdminLoginService : IAdminLoginService
    {
        private readonly IAdminLoginDBService _dbService;
        private readonly ITokenService _tokenService;

        public AdminLoginService(IAdminLoginDBService dbService, ITokenService tokenService)
        {
            _dbService = dbService;
            _tokenService = tokenService;
        }

        public async Task<AdminAuthData> Login(string Username, string Password)
        {
            if (await _dbService.Login(Username, Password))
            {
                var User = new User
                {
                    UserId = 99999,
                    Email = Username
                };

                string key = JsonConvert.SerializeObject(new SessionSecurityKeys { FolioList = null, IsAdmin = true });

                var AuthData = new AdminAuthData
                {
                    TokenInfo = _tokenService.GenerateOnlyAccessToken(User, key)
                };   

                return AuthData;
            }
            else
                throw new MyAPIException("Invalid Username or Password");
        }
    }
}
