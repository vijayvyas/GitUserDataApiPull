using Microsoft.Extensions.Caching.Memory;
using Moq;
using SampleAppTest.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SampleAppTest.UnitTest

{
    public class UserServiceTest
    {
        //  public Mock<IUserApiService> userServiceMock = new Mock<IUserApiService>();

        public Mock<IUserApiRepository> repoMoc = new Mock<IUserApiRepository>();

        public Mock<ICacheService<UserModel>> cacheMoc = new Mock<ICacheService<UserModel>>();

        IUserApiService userApiServiceImpl;
        

        [Fact]
        public async void RetrieveUserFromGit()
        {
            var userDto = new UsersDto()
                {
                    Id= 32073204,
                    Name= "Waqas Anwar",
                    Login= "waqasdotnet"
                 };

            var userModel = new UserModel()
            {
                id = 32073204,
                name = "Waqas Anwar",
                login = "waqasdotnet"
            };
            List<UsersDto> usersDtoList = new List<UsersDto>();
            usersDtoList.Add(userDto);

            List<string> users = new List<string> {
            "waqasdotnet"

        };
            userApiServiceImpl = new UserApiServiceImpl(repoMoc.Object, cacheMoc.Object);
            repoMoc.Setup(p => p.ReteriveUsers(users[0])).ReturnsAsync(userModel);
            var result = await userApiServiceImpl.ReteriveUsers(users);
            Assert.True(userDto.Id.Equals(result[0].Id));
            Assert.True(userDto.Name.Equals(result[0].Name));
            Assert.True(userDto.Login.Equals(result[0].Login));
        }

        [Fact]
        public async void RetrieveUserFromSort()
        {
            var userDto = new UsersDto()
            {
                Id = 1234,
                Name = "abc",
                Login = "abc"
            };

            var userModel = new UserModel()
            {
                id = 32073204,
                name = "Waqas Anwar",
                login = "waqasdotnet"
            };
            var userModel1 = new UserModel()
            {
                id = 1234,
                name = "abc",
                login = "abc"
            };
            List<UsersDto> usersDtoList = new List<UsersDto>();
            usersDtoList.Add(userDto);

            List<string> users = new List<string> {
            "waqasdotnet",
            "abc"

        };
            userApiServiceImpl = new UserApiServiceImpl(repoMoc.Object, cacheMoc.Object);
            repoMoc.Setup(p => p.ReteriveUsers(users[0])).ReturnsAsync(userModel);
            repoMoc.Setup(p => p.ReteriveUsers(users[1])).ReturnsAsync(userModel1);
            var result = await userApiServiceImpl.ReteriveUsers(users);
            Assert.True(userDto.Id.Equals(result[0].Id));
            Assert.True(userDto.Name.Equals(result[0].Name));
            Assert.True(userDto.Login.Equals(result[0].Login));
        }
    }
}
