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
    public class UserApplicationDocumentController : ControllerBase
    {
        private readonly IUserApplicationDocumentService _service;
        private readonly IUserApplicationService _userAppService;
        private readonly IMapper _mapper;

        public UserApplicationDocumentController(IUserApplicationDocumentService service,
                                                 IUserApplicationService userAppService,
                                                 IMapper mapper)
        {
            _service = service;
            _userAppService = userAppService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<UserApplicationDocumentDTO> Get(int id)
        {
            return await _service.GetUserApplicationDocument(id);
        }

        [HttpGet("AllForUser/{id}")]
        public async Task<List<UserApplicationDocumentDTO>> GetApplicationDocuments(int id)
        {
            var UserApp = await _userAppService.GetUserApplication(id);
            var AllDocs = await _service.GetAllUserApplicationDocuments(id);

            if (UserApp.IsResidentAdressSameAsCNIC.GetValueOrDefault(false))
                AllDocs.RemoveAt(AllDocs.FindIndex(x => x.DocumentCode == "05"));

            if (UserApp.IsZakatDeduction.GetValueOrDefault(false) ||
                UserApp.IsNonMuslim.GetValueOrDefault(false))
                AllDocs.RemoveAt(AllDocs.FindIndex(x => x.DocumentCode == "04"));

            return AllDocs;
        }

        //[HttpPost("cloud/{Id}")]
        //public async Task<UserApplicationDocumentDTO> Post(int Id)
        //{
        //    var NewDocument = await _service.InsertDocument(data);

        //    return NewDocument;
        //}

        [HttpPost]
        public async Task<UserApplicationDocumentDTO> Post([FromBody] UserApplicationDocumentDTO data)
        {
            var NewDocument = await _service.InsertDocument(data);

            return NewDocument;
        }

        [HttpDelete("{id}")]
        public async Task<UserApplicationDocumentDTO> Delete(int id)
        {
            return await _service.DeleteDocument(id);
        }
    }
}
