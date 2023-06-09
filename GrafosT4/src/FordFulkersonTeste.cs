using System;
using System.Collections.Generic;
using System.Text;

namespace Graph
{

    public class FordFulkerson
    {
        private int[,] graph;
        private int[,] residualGraph;
        private bool[] visited;
        private int[] parent;
        private int numVertices;

        public FordFulkerson(int[,] graph)
        {
            this.graph = graph;
            numVertices = graph.GetLength(0);
        }

        private bool BFS(int[,] residualGraph, int source, int sink)
        {
            visited = new bool[numVertices];
            parent = new int[numVertices];

            for (int i = 0; i < numVertices; i++)
            {
                visited[i] = false;
            }

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(source);
            visited[source] = true;
            parent[source] = -1;

            while (queue.Count > 0)
            {
                int u = queue.Dequeue();

                for (int v = 0; v < numVertices; v++)
                {
                    if (visited[v] == false && residualGraph[u, v] > 0)
                    {
                        queue.Enqueue(v);
                        parent[v] = u;
                        visited[v] = true;
                    }
                }
            }

            return visited[sink];
        }

        private int FordFulkersonAlgorithm(int[,] graph, int source, int sink)
        {
            residualGraph = (int[,])graph.Clone();
            int maxFlow = 0;

            while (BFS(residualGraph, source, sink))
            {
                int pathFlow = int.MaxValue;

                for (int v = sink; v != source; v = parent[v])
                {
                    int u = parent[v];
                    pathFlow = Math.Min(pathFlow, residualGraph[u, v]);
                }

                for (int v = sink; v != source; v = parent[v])
                {
                    int u = parent[v];
                    residualGraph[u, v] -= pathFlow;
                    residualGraph[v, u] += pathFlow;
                }

                maxFlow += pathFlow;
            }

            return maxFlow;
        }

        public int FindMaxFlow(int source, int sink)
        {
            return FordFulkersonAlgorithm(graph, source, sink);
        }
    }
}
