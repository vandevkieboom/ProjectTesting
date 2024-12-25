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
            //ik heb dit zo gedaan omdat als ik wil testen of de user bestaat of niet word er een JsonSerializationException gegooid
            //dus de is null check in de DecisionModule wordt dan niet uitgevoerd maar nu wel
            using (var httpClient = new HttpClient())
            {
                var httpResponse = httpClient.GetAsync(userUrl).GetAwaiter().GetResult();
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                var response = httpResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                try
                {
                    return JsonConvert.DeserializeObject<User>(response);
                }
                catch (JsonSerializationException)
                {
                    return null;
                }
            }
        }
    }
}
