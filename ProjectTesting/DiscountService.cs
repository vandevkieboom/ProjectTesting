using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTesting
{
    public class DiscountService : IDiscountService
    {
        public double isEligibleForDiscount(int userAge)
        {
            //hier komt dan de code voor het berekenen van de korting
            //hoeveel korting er wordt toegepast hangt af van de leeftijd van de gebruiker
            return 7.0; //stub
        }
    }
}
