using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAppTest
{
    public class UserApiService:IUserApiService
    {
        IUserApiRepository _userApiRepository;

        private ICacheService _cacheService;

        public UserApiService(IUserApiRepository userApiRepository, ICacheService cacheService)
        {
            _userApiRepository = userApiRepository;
            _cacheService = cacheService;
        }

        public async Task<List<UsersDto>> ReteriveUsers(List<string> users)
        {
            List<UsersDto> userList = new List<UsersDto>();
            foreach (var user in users)
            {
                UserModel userModel;
                try {
                    userModel = _cacheService.getFromCache<UserModel>($"UserModel_{user}");
                    if (userModel == null)
                    {
                        userModel = await _userApiRepository.ReteriveUsers(user);
                        _cacheService.updateCache($"UserModel_{user}", userModel);
                    }
                }
                catch (RedisConnectionException)
                {
                    userModel = await _userApiRepository.ReteriveUsers(user);
                }
                if (userModel.id != 0) { 
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
