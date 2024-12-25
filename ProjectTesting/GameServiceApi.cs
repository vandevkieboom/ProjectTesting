using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTesting
{
    public class GameServiceApi : IGameService
    {
        private string gameUrl;
        public string GameUrl
        {
            get { return gameUrl; }
            set { gameUrl = value; }
        }

        public Game GetGame(int id)
        {
            //ik heb dit zo gedaan omdat als ik wil testen of de game bestaat of niet word er een JsonSerializationException gegooid
            //dus de is null check in de DecisionModule wordt dan niet uitgevoerd maar nu wel
            using (var httpClient = new HttpClient())
            {
                var httpResponse = httpClient.GetAsync(gameUrl).GetAwaiter().GetResult();
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                var response = httpResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                try
                {
                    return JsonConvert.DeserializeObject<Game>(response);
                }
                catch (JsonSerializationException)
                {
                    return null;
                }
            }
        }
    }
}
