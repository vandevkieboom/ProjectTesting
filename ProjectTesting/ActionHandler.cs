using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTesting
{
    public class ActionHandler : IActionHandler
    {
        public string HandleDecision(string decision, Game purchase, double? discount = null)
        {
            //hier komt dan de code voor het afhandelen van de beslissing, bv het kopen van het spel indien "Approved"
            //of het weigeren van de aankoop indien "Rejected"
            //ook het toepassen van een korting indien "Approved" en de gebruiker in aanmerking komt voor een korting
            //de korting wordt berekend door de DiscountService
            return decision;
        }
    }
}
