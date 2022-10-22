using System.Text;

namespace AsteroïdegordelProject
{
    // This class represents a directed graph using adjacency
    // list representation
    public class Graph
    {
        public int V; // No. of vertices
        public List<int>[] adj; // No. of vertices

        static int level;

        // Constructor
        public Graph(int V)
        {
            this.V = V;
            this.adj = new List<int>[2 * V];

            for (int i = 0; i < 2 * V; i++)
                this.adj[i] = new List<int>();
        }

        // adds an edge
        public void addEdge(int v, int w, int weight)
        {

            // split all edges of weight 2 into two
            // edges of weight 1 each. The intermediate
            // vertex number is maximum vertex number + 1,
            // that is V.
            if (weight == 2)
            {
                adj[v].Add(v + this.V);
                adj[v + this.V].Add(w);
            }
            else // Weight is 1
                adj[v].Add(w); // Add w to v's list.
        }

        // print shortest path from a source vertex 's' to
        // destination vertex 'd'.
        public int printShortestPath(int[] parent, int s, int d)
        {
            level = 0;

            // If we reached root of shortest path tree
            if (parent[s] == -1)
            {
                Console.Write("Shortest Path between" +
                                "{0} and {1} is {2} ", s, d, s);
                return level;
            }

            printShortestPath(parent, parent[s], d);

            level++;
            if (s < this.V)
                Console.Write("{0} ", s);

            return level;
        }

        // finds shortest path from source vertex 's' to
        // destination vertex 'd'.

        // This function mainly does BFS and prints the
        // shortest path from src to dest. It is assumed
        // that weight of every edge is 1
        public int findShortestPath(int src, int dest)
        {
            bool[] visited = new bool[2 * this.V];
            int[] parent = new int[2 * this.V];

            // Initialize parent[] and visited[]
            for (int i = 0; i < 2 * this.V; i++)
            {
                visited[i] = false;
                parent[i] = -1;
            }

            // Create a queue for BFS
            List<int> queue = new List<int>();

            // Mark the current node as visited and enqueue it
            visited[src] = true;
            queue.Add(src);

            while (queue.Count != 0)
            {

                // Dequeue a vertex from queue and print it
                int s = queue[0];

                if (s == dest)
                    return printShortestPath(parent, s, dest);
                queue.RemoveAt(0);

                // Get all adjacent vertices of the dequeued vertex s
                // If a adjacent has not been visited, then mark it
                // visited and enqueue it
                foreach (int i in this.adj[s])
                {
                    if (!visited[i])
                    {
                        visited[i] = true;
                        queue.Add(i);
                        parent[i] = s;
                    }
                }
            }
            return 0;
        }
    }
}
