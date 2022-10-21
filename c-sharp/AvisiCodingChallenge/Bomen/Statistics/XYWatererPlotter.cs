﻿using Bomen.Waterers;

namespace Bomen.Statistics
{
    public static class XYWatererPlotter
    {
        private static float _seconds = 0;

        public static Dictionary<int, float> CreateXYTableOfWaterer(IList<Waterer> watererList)
        {
            var watererWithWaterAvailable = true;
            var watererI = 0;
            var waterer = watererList[watererI];

            // Reset seconds
            _seconds = 0;

            Dictionary<int, float> XYDictionary = new Dictionary<int, float>(); // <nTrees, timeTaken>

            var trees = World.Get50TreesList();

            var treeI = 0;
            var currentTree = trees[treeI];

            GrabWaterer(waterer);
            WalkToNextTree(waterer);

            while(trees.Last().WaterGivenToTree != 3 && watererWithWaterAvailable) // while last tree is not watered
            {
                if (currentTree.TreeHasEnoughWater())
                {
                    if (trees.Last().WaterGivenToTree != 3)
                    {
                        // Go to next tree
                        treeI++;
                        currentTree = trees[treeI];

                        // Add seconds for switching to new tree
                        WalkToNextTree(waterer);
                    }
                }

                if (waterer.IsEmpty()) // waterer is empty
                {
                    if (waterer == watererList.Last()) // a new waterer is NOT available
                    {
                        watererWithWaterAvailable = false;
                    } else // a new waterer is available
                    {
                        // Switch to new waterer
                        watererI++;
                        waterer = watererList[watererI];
                        GrabWaterer(waterer);
                    }
                }

                // Water tree untill tree is watered enough OR untill the current waterer is empty
                var currentTreeHasEnoughWater = false;
                var currentWatererStillHasWater = true;
                while (!currentTree.TreeHasEnoughWater() && currentWatererStillHasWater) // Water tree WHILE possible
                {
                    if (waterer.IsEmpty())
                    {
                        currentWatererStillHasWater = false;
                        break;
                    }

                    // Give 1L water to tree
                    var waterToGiveInL = 1;
                    currentTree.WaterTree(waterToGiveInL);
                    _seconds += waterer.WaterTreeByWaterToGive(waterToGiveInL).DurationOfGivingWater;

                    if (currentTree.TreeHasEnoughWater())
                    {
                        // Write the tree to the XY table
                        XYDictionary.Add(treeI + 1, _seconds);
                    }
                }
            }

            return XYDictionary;
        }

        private static void GrabWaterer(Waterer waterer)
        {
            _seconds += waterer.TakeNewTimeInSeconds;
        }

        private static void WalkToNextTree(Waterer waterer)
        {
            _seconds += waterer.SwitchTreeTimeInSeconds;
        }

    }
}
