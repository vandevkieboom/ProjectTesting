using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTesting
{
    public interface IDiscountService
    {
        public double isEligibleForDiscount(int userAge);
    }
}
