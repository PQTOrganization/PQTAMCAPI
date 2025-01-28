using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

using PQAMCClasses.DTOs;
using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;
using PQAMCClasses;

namespace PQAMCAPI.Services
{
    public interface ITokenService
    {
        Task<AccessToken> GenerateAccessToken(User user, string AccountCategoryID, string Keys);
        Task<AccessToken> RefreshAccessToken(RefreshAccessTokenDTO CurrentAccessToken);
        AccessToken GenerateOnlyAccessToken(User user, string Keys);
    }

    public class TokenService : ITokenService
    {
        private readonly ILogger<TokenService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IUserDBService _userDBService;
        private readonly IStoreProcedureService _spService;
        private readonly ITokenDBService _tokenDBService;

        public TokenService(ILogger<TokenService> logger, IConfiguration configuration, 
                            IUserDBService userDBService, IStoreProcedureService spService,
            ITokenDBService tokenDBService)
        {
            _logger = logger;
            _configuration = configuration;
            _userDBService = userDBService;
            _spService = spService;
            _tokenDBService = tokenDBService;
        }

        public async Task<AccessToken> GenerateAccessToken(User user, string AccountCategoryID, string Keys)
        {
            // get claims
            List<Claim> Claims = new List<Claim>();
            String AcsToken = GenerateJwtToken(user, AccountCategoryID, Claims, out DateTime ExpiryDateTime, Keys);
            String RefreshToken = GenerateRefreshToken();

            await UpdateRefreshToken(user.UserId, RefreshToken);

            return new AccessToken(AcsToken, RefreshToken, ExpiryDateTime, _configuration["JwtIssuer"]);
        }

        public AccessToken GenerateOnlyAccessToken(User user, string Keys)
        {
            List<Claim> Claims = new List<Claim>();
            string AcsToken = GenerateJwtToken(user, "", Claims, out DateTime ExpiryDateTime, Keys);
            return new AccessToken(AcsToken, "", ExpiryDateTime, _configuration["JwtIssuer"]);
        }

        private async Task InsertRefreshToken(int UserId, String NewRefreshToken)
        {
            UserRefreshToken UserToken = new UserRefreshToken
            {
                UserId = UserId,
                RefreshToken = NewRefreshToken,
                TokenDate = DateTime.UtcNow
            };

            await _tokenDBService.InsertToken(UserToken);
        }

        private async Task UpdateRefreshToken(int UserId, String NewRefreshToken)
        {
            try
            {
                UserRefreshToken UserToken = await _spService.GetSP<UserRefreshToken>("AMC_USER_REFRESH_TOKEN_PKG.GET_REFRESH_TOKEN", UserId);
                UserToken.RefreshToken = NewRefreshToken;
                UserToken.TokenDate = DateTime.UtcNow;
                await _tokenDBService.UpdateToken(UserToken);
            }
            catch (Exception ex)
            {
                await InsertRefreshToken(UserId, NewRefreshToken);
            }            
        }
       
        public async Task<AccessToken> RefreshAccessToken(RefreshAccessTokenDTO CurrentAccessToken)
        {
            ClaimsPrincipal Principal = GetPrincipalFromExpiredToken(CurrentAccessToken.Token);

            //Get User ID from claims
            Claim C = Principal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier);

            //Get Account Category ID from claims
            Claim CAccCat = Principal.Claims.First(c => c.Type == "AccountCategoryID");

            //Get Keys from claims
            Claim CKeys = Principal.Claims.First(c => c.Type == "SecurityKeys");

            String UserID = C.Value.ToString();
            String AccountCategoryID = CAccCat.Value.ToString();
            String Key = CKeys.Value.ToString();

            User user = await _userDBService.FindAsync(int.Parse(UserID));

            String SavedRefreshToken = await GetRefreshTokenFromDB(int.Parse(UserID)); //retrieve the refresh token from a data store

            if (SavedRefreshToken != CurrentAccessToken.RefreshToken)
                throw new SecurityTokenException("Invalid refresh token");

            return await GenerateAccessToken(user, AccountCategoryID, Key);
        }

        private async Task<string> GetRefreshTokenFromDB(int UserId)
        {
            UserRefreshToken UserToken = await _spService.GetSP<UserRefreshToken>("AMC_USER_REFRESH_TOKEN_PKG.GET_REFRESH_TOKEN", UserId);

            if (UserToken != null)
                return UserToken.RefreshToken;
            else
                return "";
        }

        private String GenerateJwtToken(User user, string AccountCategoryID, IList<Claim> Claims, out DateTime ExpiryDateTime, string SecurityKeys)
        {
            Console.WriteLine(user);
            Claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()));
            Claims.Add(new Claim(ClaimTypes.Sid, user.UserId.ToString()));
            Claims.Add(new Claim(JwtRegisteredClaimNames.GivenName, user.Email));
            Claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            Claims.Add(new Claim("AccountCategoryID", AccountCategoryID.ToString()));
            Claims.Add(new Claim(Globals.SessionKeys.SecurityKeys, SecurityKeys));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            ExpiryDateTime = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(_configuration["JwtIssuer"], _configuration["JwtIssuer"],
                                              Claims, expires: ExpiryDateTime, signingCredentials: creds);

            _logger.LogInformation("Token generated successfully for [" + user.Email + "]");

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken(int Size = 32)
        {
            var randomNumber = new byte[Size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["JwtIssuer"],
                ValidAudience = _configuration["JwtIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                                                _configuration["JwtKey"])),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                                                    StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
