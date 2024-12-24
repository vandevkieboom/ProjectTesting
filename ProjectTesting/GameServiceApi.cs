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
            using (var httpClient = new HttpClient())
            {
                var httpRespone = httpClient.GetAsync(gameUrl).GetAwaiter().GetResult();
                var response = httpRespone.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return JsonConvert.DeserializeObject<Game>(response);
            }
        }
    }
}
