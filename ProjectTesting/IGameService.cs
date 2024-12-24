using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTesting
{
    public interface IGameService
    {
        string GameUrl { get; set; }
        Game GetGame(int id);
    }
}
