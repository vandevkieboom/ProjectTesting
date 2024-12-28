using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTesting
{
    public class GameService : IGameService
    {
        private string gameUrl;
        public string GameUrl
        {
            get { return gameUrl; }
            set { gameUrl = value; }
        }

        public Game GetGame(int id)
        {
            //hier komt dan de code voor het ophalen van een game
            throw new NotImplementedException();
        }
    }
}
