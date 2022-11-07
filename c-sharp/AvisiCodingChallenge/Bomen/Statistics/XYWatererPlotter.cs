using Bomen.Waterers;

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
            Console.WriteLine($"Switching waterer to {waterer.Name}. Total time passed is {_seconds} seconds");

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

                if (treeI == 48)
                {
                    Console.WriteLine("");
                }

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

        public static Dictionary<int, float> CreateXYTableOfWatererWithDifferntLines(List<Waterer> first, List<Waterer> second, List<Waterer> third)
        {
            // 16 small bottles
            List<Waterer> watererList = first;

            //List<Waterer> watererList = new List<Waterer> { new SmallBottle(), new SmallBottle(), new SmallBottle(), new SmallBottle(), new SmallBottle(), new SmallBottle(), new SmallBottle(), new SmallBottle(), new SmallBottle(), new SmallBottle(), new SmallBottle(), new SmallBottle(), new SmallBottle(), new SmallBottle(), new SmallBottle(), new SmallBottle() };

            var watererWithWaterAvailable = true;
            var watererI = 0;
            var waterer = watererList[watererI];

            var triggerSecondTime = true;

            // Reset seconds
            _seconds = 0;

            Dictionary<int, float> XYDictionary = new Dictionary<int, float>(); // <nTrees, timeTaken>

            var trees = World.Get50TreesList();

            var treeI = 0;
            var currentTree = trees[treeI];

            GrabWaterer(waterer);
            WalkToNextTree(waterer);

            while (trees.Last().WaterGivenToTree != 3) // while last tree is not watered
            {
                if (treeI == 47)
                {
                    Console.WriteLine("");
                }

                if (watererWithWaterAvailable) { // UNUSED IF
                    if (waterer.IsEmpty()) // waterer is empty
                    {
                        if (waterer == watererList.Last()) // a new waterer is NOT available
                        {
                            if (triggerSecondTime) // second line is big bottles, 
                            {
                                watererList = second;
                                watererI = 0;
                                waterer = watererList[watererI];
                                GrabWaterer(waterer);
                                Console.WriteLine($"Switching waterer to {waterer.Name}. Total time passed is {_seconds} seconds");
                                watererWithWaterAvailable = true;
                                triggerSecondTime = false; // Dont trigger this if again, but trigger the else now
                            }
                            else // third line is fire hose
                            {
                                watererList = third;
                                watererI = 0;
                                watererWithWaterAvailable = true;
                                waterer = watererList[watererI];
                                GrabWaterer(waterer);
                                Console.WriteLine($"Switching waterer to {waterer.Name}. Total time passed is {_seconds} seconds");
                            }
                        }
                        else // a new waterer is available
                        {
                            // Switch to new waterer
                            watererI++;
                            waterer = watererList[watererI];
                            GrabWaterer(waterer);
                        }
                    }

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
                        var durationOfWatering  = waterer.WaterTreeByWaterToGive(waterToGiveInL).DurationOfGivingWater;
                        Console.WriteLine($"Watering tree {treeI + 1}/{trees.Count} with {waterer.Name} which took {durationOfWatering} seconds");
                        _seconds += durationOfWatering;

                        if (currentTree.TreeHasEnoughWater())
                        {
                            // Write the tree to the XY table
                            XYDictionary.Add(treeI + 1, _seconds);
                        }
                    }
                } else
                {
                    // not valid anymore
                }
            }

            return XYDictionary;
        }

        private static void GrabWaterer(Waterer waterer)
        {
            _seconds += waterer.TakeNewTimeInSeconds;
            Console.WriteLine($"Grab new waterer {waterer.Name}");
        }

        private static void WalkToNextTree(Waterer waterer)
        {
            _seconds += waterer.SwitchTreeTimeInSeconds;
            Console.WriteLine($"Walk to next tree with {waterer.Name} in hand. Total seconds is {_seconds} seconds");
        }

    }
}
