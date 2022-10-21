using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomen.Waterers
{
    public class SmallBottle : Waterer
    {
        public SmallBottle()
        {
            WaterCapacity = 5;
            WaterRatePerSecond = 1;
            SwitchTreeTimeInSeconds = 1;
            TakeNewTimeInSeconds = 2;
            Name = "Small Bottle";
            Amount = 16;
        }
    }
}
