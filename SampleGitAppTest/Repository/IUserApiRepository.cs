using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAppTest
{
    public interface IUserApiRepository
    {
        //UserModel ReteriveUsers(string users);
        Task<UserModel> ReteriveUsers(string users);
    }
}
