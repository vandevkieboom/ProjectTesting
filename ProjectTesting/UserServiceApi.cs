using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTesting
{
    public class UserServiceApi : IUserService
    {
        private string userUrl;
        public string UserUrl
        {
            get { return userUrl; }
            set { userUrl = value; }
        }

        public User GetUser(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var httpRespone = httpClient.GetAsync(userUrl).GetAwaiter().GetResult();
                var response = httpRespone.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return JsonConvert.DeserializeObject<User>(response);
            }
        }
    }
}
