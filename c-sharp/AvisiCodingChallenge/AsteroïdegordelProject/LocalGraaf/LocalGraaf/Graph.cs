namespace LocalGraaf;

public class Graph
{
    // No. of vertices in graph
    private int v;

    // adjacency list (adjecent vertices) so each vertex has a list with integers with connected vertices
    private List<AdjecentVertex>[] adjList;

    private List<CalculatedPath> CalculatedPaths;

    private bool IsUndirectedGraph;

    // Constructor
    public Graph(int vertices, bool isUndirectedGraph)
    {

        // initialise vertex count
        this.v = vertices;

        // initialise adjacency list
        InitAdjList();

        CalculatedPaths = new List<CalculatedPath>();
        IsUndirectedGraph = isUndirectedGraph;
    }

    // utility method to initialise
    // adjacency list
    private void InitAdjList()
    {
        adjList = new List<AdjecentVertex>[v];

        for (var i = 0; i < v; i++)
        {
            adjList[i] = new List<AdjecentVertex>();
        }
    }

    // add edge from u to v
    public void AddEdge(int from, int to, int weight)
    {
        // Add v to u's list.
        adjList[from].Add(new AdjecentVertex(to, weight));

        // And the opposite (because it is undirected an add edge should go both ways

        if (IsUndirectedGraph)
        {
            // if IsUndirectedGraph, the Edge should go both ways (so also from to, to from ;))
            adjList[to].Add(new AdjecentVertex(from, weight));
        }
    }

    // Prints all paths from
    // 's' to 'd'
    public void printAllPaths(int s, int d)
    {
        bool[] isVisited = new bool[v];
        List<int> pathList = new List<int>();

        // add source to path[]
        pathList.Add(s);

        // Call recursive utility
        CalculateAllPossiblePaths(s, d, isVisited, pathList);
    }

    // A recursive function to print
    // all paths from 'startNode' to 'endNode'.
    // isVisited[] keeps track of
    // vertices in current path.
    // localPathList<> stores actual
    // vertices in the current path
    private void CalculateAllPossiblePaths(int startNode, int endNode,
        bool[] isVisited,
        List<int> localPathList)
    {

        if (startNode.Equals(endNode))
        {
            var pathWeight = CalculatePathWeight(localPathList);
            // Console.WriteLine(string.Join(" ", localPathList) + " with weight: " + pathWeight.ToString());

            // Add outcome (found path) as object
            CalculatedPaths.Add(new CalculatedPath() { Cost = pathWeight, Path = string.Join(" ", localPathList) });

            // if match found then no need
            // to traverse more till depth
            return;
        }

        // Mark the current node
        isVisited[startNode] = true;

        // Recur for all the vertices
        // adjacent to current vertex
        foreach (var i in adjList[startNode])
        {
            if (!isVisited[i.VertexNumber])
            {
                // store current node
                // in path[]
                localPathList.Add(i.VertexNumber);
                CalculateAllPossiblePaths(i.VertexNumber, endNode, isVisited,
                    localPathList);

                // remove current node
                // in path[]
                localPathList.Remove(i.VertexNumber);
            }
        }

        // Mark the current node
        isVisited[startNode] = false;
    }

    public void PrintCalculatedPaths()
    {
        var sortedPaths = CalculatedPaths.OrderBy(cp => cp.Cost);
        Console.WriteLine($"{sortedPaths.Count()} total paths found");

        foreach (var path in sortedPaths)
        {
            Console.WriteLine($"{path.Path} with cost {path.Cost}");
        }
    }

    private int CalculatePathWeight(List<int> pathList)
    {
        int totalWeight = 0;
        int totalWeightsToCalculate = pathList.Count - 1; // 2paths is 1 connection, 3 paths is 2 connections, etc.

        for(var i=0; i< totalWeightsToCalculate; i++)
        {
            var from = pathList[i];
            var to = pathList[i+1];

            var relation = adjList[from].Where(toNode => toNode.VertexNumber == to).First();
            totalWeight += relation.Weight;
        }

        return totalWeight;
    }
}