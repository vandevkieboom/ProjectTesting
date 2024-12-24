using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTesting
{
    public class GameService : IGameService
    {
        public string GameUrl { get; set; }

        public Game GetGame(int id)
        {
            //hier komt dan de code voor het ophalen van een game
            throw new NotImplementedException();
        }
    }
}
