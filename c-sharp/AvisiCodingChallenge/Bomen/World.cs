namespace Bomen;

public static class World
{
    public static int nTrees = 50;
    public static int nWaterPerTree = 3;

    public static void FindBestOption()
    {
        // Idk, look at the best options to do??
    }

    public static List<Tree> Get50TreesList()
    {
        var list = new List<Tree>();
        for (var i=0; i<50; i++)
        {
            list.Add(new Tree());
        }

        return list;
    }

}