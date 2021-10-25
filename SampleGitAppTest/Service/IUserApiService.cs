using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleAppTest
{
    public interface IUserApiService
    {
        Task<List<UsersDto>> ReteriveUsers(List<string> users);
    }
}
