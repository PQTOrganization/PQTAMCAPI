using Microsoft.AspNetCore.Mvc;
using PQAMCAPI.Interfaces.Services;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminLoginController : ControllerBase
    {
        private readonly IAdminLoginService _service;

        public AdminLoginController(IAdminLoginService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<AdminAuthData> Post([FromBody] AdminLoginData Data)
        {
            return await _service.Login(Data.Username, Data.Password);
        }
    }
}
