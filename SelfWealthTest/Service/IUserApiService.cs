using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfWealthTest
{
    public interface IUserApiService
    {
        Task<List<UsersDto>> ReteriveUsers(List<string> users);
    }
}
