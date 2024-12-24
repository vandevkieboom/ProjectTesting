using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTesting
{
    public interface IActionHandler
    {
        public string HandleDecision(string decision, Game purchase);
    }
}
