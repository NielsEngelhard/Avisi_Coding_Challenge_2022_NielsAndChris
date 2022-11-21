// See https://aka.ms/new-console-template for more information

using LocalGraaf;

var edges = DataMapper.GetAllPointsAsIntArray();

Graph g = new Graph(edges.Count, false);
foreach (var edge in edges)
{
    // Add weight
    g.AddEdge(edge[0], edge[1], edge[2]);
}

// if Validate if this really is right (handmatig bekijken)
// Kijken of we de weight ook kunnen printen
g.printAllPaths(0, 4);

g.PrintCalculatedPaths();

// 4 asteroides
// [0]
// [1, 3, 4, 5]
// [2, 3, 4]
// [1, 4, 5]
// Een array met seconden waarop dee asteroide door het gas beweegt