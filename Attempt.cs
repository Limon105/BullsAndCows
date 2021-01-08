using System;
using System.Collections.Generic;
using System.Text;

namespace BullsAndCows
{
    class Attempt
    {
        public string Number { get; set; }
        public int CountBulls { get; set; }
        public int CountCows { get; set; }
        public int SummCowsAndBulls { get; }
        
        public Attempt(string number, int countBulls, int countCows)
        {
            Number = number;
            CountBulls = countBulls;
            CountCows = countCows;
            SummCowsAndBulls = countBulls + countCows;
        }
    }
}
