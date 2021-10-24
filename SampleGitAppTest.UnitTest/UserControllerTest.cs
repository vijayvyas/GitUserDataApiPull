using Moq;
using SampleAppTest.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.Text.Json;

namespace SampleAppTest.UnitTest

{
    public class UserControllerTest
    {

        public Mock<IUserApiService> userServiceMock = new Mock<IUserApiService>();

        public Mock<IUserApiRepository> mock = new Mock<IUserApiRepository>();

        public Mock<ICacheService<UserModel>> ICacheServiceObj = new Mock<ICacheService<UserModel>>();

        UserController userController;


        [Fact]
        public async void RetrieveUser()
        {
            var userDto = new UsersDto()
            {
                Id = 32073204,
                Name = "Waqas Anwar",
                Login = "waqasdotnet"
            };
            List<UsersDto> usersDtoList = new List<UsersDto>();
            usersDtoList.Add(userDto);

            List<string> users = new List<string> {
            "waqasdotnet",
            "gkalpakq121212121212"

        };
            userServiceMock.Setup(p => p.ReteriveUsers(users)).ReturnsAsync(usersDtoList);
            userController = new UserController(userServiceMock.Object);
            var result = await userController.RetrieveUsers(users);
            Assert.True(usersDtoList.Equals(result));
        }
    }
}
