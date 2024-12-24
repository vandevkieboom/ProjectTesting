﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTesting
{
    public class Game
    {
        public int Id { get; set; }
        public string GameTitle { get; set; } = string.Empty;
        public int AgeRating { get; set; }
        public int IsEligibleForDiscount { get; set; }
    }
}
