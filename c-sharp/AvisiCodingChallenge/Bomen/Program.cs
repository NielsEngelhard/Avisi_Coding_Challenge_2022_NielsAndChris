// See https://aka.ms/new-console-template for more information


using Bomen;

var waterArsenal = new WaterArsenal();

waterArsenal.PrintArsenalValues();

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
while (!allTreesHaveWater)
{
    // Grab big bottles

    // Grab hose and then spray everything else down

    if (trees.Last().TreeHasEnoughWater())
    {
        allTreesHaveWater = true;
    }
}