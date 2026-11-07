using System;
using System.Collections.Generic;
using System.Linq;

namespace SalinoMvc5.SalinoUtilities
{
    public static class RandomNumber
    {
        public static int RandomMake()
        {
            Random random = new Random();
            int mycode = random.Next(10000, 900000);
            return mycode;
        }
    }
}