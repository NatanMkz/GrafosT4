using Graph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace Graph
{

    public class FordFukerson
    {
        public GraphMatriz Graph;
        public GraphMatriz Residual;
        public double MaxFlow { get; set; }

        public FordFukerson(GraphMatriz graph)
        {
            Graph = graph;
            Residual = new GraphMatriz();
            Residual.LoadFile(Graph.filePath);
            MaxFlow = 0;
        }


        public bool CalculateFordFukerson(int from, int to)
        {
            int u, v;
            int[] pathTo = new int[Residual.Nodes];

            while (BFS(from, to, pathTo))
            {
                double pathFlow = int.MaxValue;

                for (v = to; v != from; v = pathTo[v])
                {
                    u = pathTo[v];
                    pathFlow = Math.Min(pathFlow, Residual.Matrix[u, v]);
                }

                for (v = to; v != from; v = pathTo[v])
                {
                    u = pathTo[v];
                    Residual.Matrix[u, v] -= pathFlow;
                    Residual.Matrix[v, u] += pathFlow;
                }

                MaxFlow += pathFlow;
            }

            return true;
        }

        private bool BFS(int from, int to, int[] parent)
        {
            bool[] visited = new bool[Residual.Nodes];

            for (int i = 0; i < Residual.Nodes; ++i)
            {
                visited[i] = false;
            }

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(from);
            visited[from] = true;
            parent[from] = -1;

            while (queue.Count > 0)
            {
                int u = queue.Dequeue();

                for (int v = 0; v < Residual.Nodes; ++v)
                {
                    if (visited[v] == false && Residual.Matrix[u, v] > 0)
                    {
                        queue.Enqueue(v);
                        parent[v] = u;
                        visited[v] = true;
                    }
                }
            }

            return (visited[to] == true);
        }

    }

}
