// See https://aka.ms/new-console-template for more information


using Bomen;
using Bomen.Waterers;

var arsenal = new WaterArsenal();

arsenal.PrintArsenalValues();

// RULES
// There are 50 trees
// Each tree needs 3 water

// There are

// STRATEGY 1 <-----------------revision if this is the way to go
// Water plant with 0.5L 
// Check for fastest path
// Water plant with 0.5L 
// Check for fastest path

// Fill list of trees with all trees that needs to be watered
IList<Tree> trees = new List<Tree>();
for (var i=0; i<World.nTrees; i++)
{
    trees.Add(new Tree());
}

bool allTreesHaveWater = false;

float time = 0;

Waterer currentItem = arsenal.GardeningHose;

time += currentItem.GrabItem();
time += currentItem.WalkToTree();

var currentTreeIndex = 0;

//while (!allTreesHaveWater)
//{
//    var secondsOfWateringTree = 0.5f;
//    var waterGivenStats = currentItem.WaterTreeByTimeInSeconds(secondsOfWateringTree);

//    Console.WriteLine($"Water current tree with {waterGivenStats.WaterLeftInCurrentItem}L water which costs {waterGivenStats.DurationOfGivingWater} second");
//    time += waterGivenStats.DurationOfGivingWater;

//    // Current tree does not need any more water
//    if (trees[currentTreeIndex].WaterGivenToTree == 3)
//    {
//        // Go to next tree
//        Console.WriteLine($"Switching to new tree (index {currentTreeIndex})");
//        time += currentItem.SwitchTreeTimeInSeconds;
//        currentTreeIndex++;
//    }


//    if (trees.Last().TreeHasEnoughWater())
//    {
//        allTreesHaveWater = true;
//    }
//}

// One specific plt
//arsenal.PrintXYsPerWaterer();

arsenal.PrintMultipleStagesXY();

// TODO: Create for each of the 4 equipments a X,Y table where X is trees watered and y is duration in seconds