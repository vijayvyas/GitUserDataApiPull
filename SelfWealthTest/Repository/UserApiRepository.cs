using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace SelfWealthTest
{
    public class UserApiRepository: IUserApiRepository
    {
        private readonly HttpClient client;

        public UserApiRepository(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient("PublicGitApi");
        }
        //https://localhost:44399/user?users=vijayvyas
        public async Task<UserModel> ReteriveUsers(string user)
        //public UserModel ReteriveUsers(string users)
        {
            var url = string.Format("users/{0}", user);
            var result = new UserModel();


            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("GitApi", "1"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                result = JsonSerializer.Deserialize<UserModel>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }

            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
            }   
           
            //var result =
            //   JsonSerializer.Deserialize<UserModel>(jsonString);
            return result;
        }
    }
}
