using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PQAMCAPI.Interfaces.Services;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Controller.Onboarding
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "PQAMCAuthScheme")]
    [ApiController]
    public class UserApplicationNomineeController : ControllerBase
    {
        private readonly IUserApplicationNomineeService _userApplicationNomineeService;
        private readonly IMapper _mapper;

        public UserApplicationNomineeController(IUserApplicationNomineeService userApplicationNomineeService, IMapper mapper)
        {
            _userApplicationNomineeService = userApplicationNomineeService;           
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<UserApplicationNomineeDTO> Get(int id)
        {
            return await _userApplicationNomineeService.GetUserApplicationNominee(id);
        }

        [HttpGet("application/{id}")]
        public async Task<List<UserApplicationNomineeDTO>> GetAllByApplication(int id)
        {
            return await _userApplicationNomineeService.GetNomineesForUserApplication(id);
        }

        [HttpPost]
        public async Task<List<UserApplicationNomineeDTO>> Post([FromBody] List<UserApplicationNomineeDTO> data)
        {
            var newUserApp = await _userApplicationNomineeService.InsertUserApplicationNominee(data);

            return newUserApp;
        }
        
        [HttpPut("{id}")]
        public async Task<UserApplicationNomineeDTO> Put(int id, [FromBody] UserApplicationNomineeDTO data)
        {

            try
            {
                var newUserApp = await _userApplicationNomineeService.UpdateUserApplicationNominee(id, data);                

                return newUserApp;
            }
            catch(Exception ex)
            { }
            return null;
        }

        [HttpDelete("{id}")]
        public async Task<int> Delete(int id)
        {
            return await _userApplicationNomineeService.DeleteUserApplicationNominee(id);

        }

        [HttpDelete("byapplication/{id}")]
        public async Task<int> DeleteByApplication(int id)
        {
            return await _userApplicationNomineeService.DeleteNomineeByUserApplicationID(id);

        }
    }
}
