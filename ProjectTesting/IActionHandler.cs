﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTesting
{
    public interface IActionHandler
    {
        string HandleDecision(string decision, Game purchase, double discount);
    }
}
