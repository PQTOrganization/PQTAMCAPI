using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;
using Oracle.ManagedDataAccess.Client;
using Helper;
using System.Configuration;
using System.Data;
using AutoMapper;
using PQAMCClasses.DTOs;
using PQAMCClasses.CloudDTOs;
using Hangfire;
using Services;
using API.Classes;
using PQAMCClasses;

namespace PQAMCAPI.Services.Domain
{
    public class UserApplicationNomineeService : IUserApplicationNomineeService
    {
        private readonly IMapper _mapper;
        private readonly IUserApplicationNomineeDBService _userApplicationNomineeDBService;
        private readonly ILogger _logger;
        
        public UserApplicationNomineeService(IMapper mapper, IUserApplicationDBService userAppDBService, IUserApplicationNomineeDBService userApplicationNomineeDBService,
                                             ILogger<UserService> logger)
        {
            _mapper = mapper;
            _userApplicationNomineeDBService = userApplicationNomineeDBService;
            _logger = logger;
        }

        public async Task<UserApplicationNomineeDTO> GetUserApplicationNominee(int UserApplicationNomineeID)
        {
            var UserApplicationNominees = await _userApplicationNomineeDBService.FindAsync(UserApplicationNomineeID);
            return _mapper.Map<UserApplicationNomineeDTO>(UserApplicationNominees); ;
        }

        public async Task<List<UserApplicationNomineeDTO>> GetNomineesForUserApplication(int UserApplicationNomineeID)
        {
            var UserApplicationNominees = await _userApplicationNomineeDBService.GetNomineesForUserApplication(UserApplicationNomineeID);
            return _mapper.Map<List<UserApplicationNomineeDTO>>(UserApplicationNominees); ;
        }

        public async Task<List<UserApplicationNomineeDTO>> InsertUserApplicationNominee(List<UserApplicationNomineeDTO> Data)
        {
            List<UserApplicationNomineeDTO> NewNomineesList = new List<UserApplicationNomineeDTO>();

            foreach (UserApplicationNomineeDTO Nominee in Data)
            {
                if (Nominee.UserApplicationNomineeId == 0)
                {
                    var UserAppNominee = _mapper.Map<UserApplicationNominee>(Nominee);
                    var NewUserAppNominee = await _userApplicationNomineeDBService.InsertUserApplicationNominee(UserAppNominee);
                    NewNomineesList.Add(_mapper.Map<UserApplicationNomineeDTO>(NewUserAppNominee));
                } 
                else
                {
                    var UserAppNominee = _mapper.Map<UserApplicationNominee>(Nominee);
                    var NewUserAppNominee = await _userApplicationNomineeDBService.UpdateUserApplicationNominee(Nominee.UserApplicationNomineeId, UserAppNominee);
                    NewNomineesList.Add(_mapper.Map<UserApplicationNomineeDTO>(NewUserAppNominee));
                }
            }
            return NewNomineesList;
        }

        public async Task<UserApplicationNomineeDTO> InsertUserApplicationNominee(UserApplicationNomineeDTO Data)
        {
            var UserAppNominee = _mapper.Map<UserApplicationNominee>(Data);
            var NewUserAppNominee = await _userApplicationNomineeDBService.InsertUserApplicationNominee(UserAppNominee);

            return _mapper.Map<UserApplicationNomineeDTO>(NewUserAppNominee);
        }

        public async Task<UserApplicationNomineeDTO> UpdateUserApplicationNominee(int UserApplicationNomineeId, UserApplicationNomineeDTO Data)
        {
            var UserAppNominee = _mapper.Map<UserApplicationNominee>(Data);
            UserAppNominee = await _userApplicationNomineeDBService.UpdateUserApplicationNominee(UserApplicationNomineeId, UserAppNominee);
           
            return _mapper.Map<UserApplicationNomineeDTO>(UserAppNominee);
        }

        public async Task<int> DeleteUserApplicationNominee(int UserApplicationNomineeId)
        {
            return await _userApplicationNomineeDBService.DeleteUserApplicationNominee(UserApplicationNomineeId); ;
        }

        public async Task<int> DeleteNomineeByUserApplicationID(int UserApplicationId)
        {
            return await _userApplicationNomineeDBService.DeleteNomineeByUserApplicationID(UserApplicationId); ;
        }

    }
}
