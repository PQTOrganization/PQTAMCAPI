
using API.Classes;
using Microsoft.AspNetCore.Http;
using PQAMCAPI.Interfaces;
using System.Security.Claims;

namespace PQAMCAPI.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public HttpContext CurrentContext()
        {
            return _httpContextAccessor.HttpContext;
        }

        public string GetCurrentUserId()
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                Claim? UserClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid);

                if (UserClaim == null)
                    throw new MyAPIException("Bad request. Invalid Session");
                else
                    return UserClaim.Value;
            }
            else
                throw new MyAPIException("Bad request. Invalid Context");
        }

        public bool IsCurrentUserAdmin()
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                Claim? UserClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role);

                if (UserClaim == null)
                    throw new MyAPIException("Bad request. Invalid Session");
                else
                    return UserClaim.Value == "admin";
            }
            else
                throw new MyAPIException("Bad request. Invalid Context");
        }
        public string GetRequesterURL()
        {
            var Request = _httpContextAccessor.HttpContext.Request;
            return $"{Request.Scheme}://{Request.Host}/";
        }
    }
}