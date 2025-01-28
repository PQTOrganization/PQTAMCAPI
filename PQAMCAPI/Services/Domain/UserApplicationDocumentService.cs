using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;
using Oracle.ManagedDataAccess.Client;
using Helper;
using System.Configuration;
using System.Data;
using AutoMapper;
using PQAMCClasses.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace PQAMCAPI.Services.Domain
{
    public class UserApplicationDocumentService : IUserApplicationDocumentService
    {
        private readonly IMapper _mapper;
        private readonly IUserApplicationDocumentDBService _documentDBService;

        public UserApplicationDocumentService(IUserApplicationDocumentDBService documentDBService,
                                              IMapper mapper)
        {
            _mapper = mapper;
            _documentDBService = documentDBService;
        }

        public async Task<List<UserApplicationDocumentDTO>> GetAllUserApplicationDocuments(
                                                                            int UserApplicationId)
        {
            return await _documentDBService.GetAllUserApplicationDocuments(UserApplicationId);
        }

        public async Task<UserApplicationDocumentDTO> GetUserApplicationDocument(int UserApplicationDocumentId)
        {
            var Document = await _documentDBService.GetApplicationDocument(UserApplicationDocumentId);

            return _mapper.Map<UserApplicationDocumentDTO>(Document);
        }

        public async Task<UserApplicationDocumentDTO> InsertDocument(UserApplicationDocumentDTO Document)
        {
            var Doc = _mapper.Map<UserApplicationDocument>(Document);
            var newDoc = await _documentDBService.InsertDocument(Doc);

            return _mapper.Map<UserApplicationDocumentDTO>(newDoc);
        }

        public async Task<UserApplicationDocumentDTO> DeleteDocument(int UserApplicationDocumentId)
        {
            var DeletedDoc = await _documentDBService.DeleteDocument(UserApplicationDocumentId);

            return _mapper.Map<UserApplicationDocumentDTO>(DeletedDoc);
        }
    }
}
