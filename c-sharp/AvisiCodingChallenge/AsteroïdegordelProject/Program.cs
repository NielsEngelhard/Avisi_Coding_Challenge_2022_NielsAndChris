// See https://aka.ms/new-console-template for more information


using AsteroïdegordelProject;

int[][] edges = new int[9][];
edges[0] = new int[] { 0, 1, 3 };
edges[1] = new int[] { 0, 2, 3 };
edges[2] = new int[] { 0, 3, 9 };
edges[3] = new int[] { 1, 2, 4 };
edges[4] = new int[] { 1, 3, 3 };
edges[5] = new int[] { 1, 4, 3 };
edges[6] = new int[] { 2, 3, 3 };
edges[7] = new int[] { 2, 4, 3 };
edges[8] = new int[] { 3, 4, 1 };

//int[][] waits = new int[4][];
//waits[0] = new int[] { 0 };
//waits[1] = new int[] { 1, 3, 4, 5 };
//waits[2] = new int[] { 2, 3, 4 };
//waits[3] = new int[] { 1, 4, 5 };


// Create a graph given in the above diagram
int V = 5;
Graph g = new Graph(V);
//g.addEdge(0, 1, 2);
//g.addEdge(0, 2, 2);
//g.addEdge(1, 2, 1);
//g.addEdge(1, 3, 1);
//g.addEdge(2, 0, 1);
//g.addEdge(2, 3, 2);
//g.addEdge(3, 3, 2);

foreach (var edge in edges)
{
    var from = edge[0];
    var to = edge[1];
    var weight = edge[2];

    g.addEdge(from, to, weight);
}

int src = 0, dest = 4;
Console.Write("\nShortest Distance between" +
                    " {0} and {1} is {2}\n", src,
                    dest, g.findShortestPath(src, dest));