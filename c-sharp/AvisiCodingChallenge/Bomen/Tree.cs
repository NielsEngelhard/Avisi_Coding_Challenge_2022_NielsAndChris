using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomen
{
    public class Tree
    {
        public float WaterGivenToTree;

        public Tree()
        {
            WaterGivenToTree = 0;
        }
        
        // Returns if tree has enough water
        public bool WaterTree(float litersGiven)
        {
            WaterGivenToTree += litersGiven;

            if (litersGiven > World.nWaterPerTree)
            {
                throw new Exception("The tree was given too much water!! This is probably not efficient because that costs extra time.");
            }

            return TreeHasEnoughWater();
        }

        public bool TreeHasEnoughWater()
        {
            return WaterGivenToTree == World.nWaterPerTree;
        }
    }
}
