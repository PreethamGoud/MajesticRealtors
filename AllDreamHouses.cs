﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MajesticRealtors
{
    public class AllDreamHouses
    {
        static AllDreamHouses()
        {
            AllHouses = new List<DreamHouses>();
        }
        public static IList<DreamHouses> AllHouses { get; set; }
    }
}
