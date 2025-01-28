using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PQAMCAPI.Interfaces.Services;
using PQAMCAPI.Models;
using PQAMCClasses.DTOs;
using Services;

namespace PQAMCAPI.Controller.Onboarding
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "PQAMCAuthScheme")]
    [ApiController]
    public class UserApplicationDiscrepancyController : ControllerBase
    {
        private readonly IUserApplicationDiscrepancyService _service;

        public UserApplicationDiscrepancyController(IUserApplicationDiscrepancyService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<UserApplicationDiscrepancyDTO> Get(int id)
        {
            return await _service.GetUserApplicationDiscrepancy(id);
        }

        [HttpGet("ForApplication/{id}")]
        public async Task<UserApplicationDiscrepancyDTO> GetDiscrepanyForApplication(int id)
        {
            return await _service.GetDiscrepancyForUserApplication(id);
        }

        [HttpPost]
        public async Task<UserApplicationDiscrepancyDTO> Post([FromBody] InsertDiscrepancyDTO data)
        {
            return await _service.InsertDiscrepancy(data);
        }
    }
}
