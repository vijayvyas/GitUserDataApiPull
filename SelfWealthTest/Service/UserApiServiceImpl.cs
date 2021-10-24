using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace SelfWealthTest
{
    public class UserApiServiceImpl:IUserApiService
    {
        IUserApiRepository _userApiRepository;

        private ICacheService<UserModel> _cacheService;

        public UserApiServiceImpl(IUserApiRepository userApiRepository, ICacheService<UserModel> cacheService)
        {
            _userApiRepository = userApiRepository;
            _cacheService = cacheService;
        }

        public async Task<List<UsersDto>> ReteriveUsers(List<string> users)
        {
            List<UsersDto> userList = new List<UsersDto>();
            foreach (var user in users)
            {
                UserModel userModel   = _cacheService.getFromCache("UserModel" + user);
                if (userModel == null)
                {
                    userModel = await _userApiRepository.ReteriveUsers(user);
                    _cacheService.updateCache("UserModel" + user, userModel);
                }
                if(userModel.id != 0) { 
                    UsersDto usersDto = transFormModelToDto(userModel);
                    userList.Add(usersDto);
                }

            }
            userList = userList.OrderBy(o=> o.Name).ToList();
            return userList;
        }

        private UsersDto transFormModelToDto(UserModel userModel)
        {
            UsersDto usersDto = new UsersDto();
            usersDto.Id = userModel.id;
            usersDto.Name = userModel.name;
            usersDto.Login = userModel.login;
            usersDto.Company = userModel.company;
            usersDto.NoOfFollowers = userModel.followers;
            usersDto.NoOfPublicRepsitories = userModel.public_repos;

            return usersDto;
        }
    }
}
