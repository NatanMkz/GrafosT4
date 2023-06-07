using System;
using Graph;
using System.Diagnostics;
using System.Collections.Generic;

namespace Graph
{
    public class Dijkstra
    {
        private List<Fragas> adjacencyList = new List<Fragas>();
        private Graph graph;
        public Dijkstra(Graph graph)
        {
            this.graph = graph;

            for (int i = 0; i < graph.Nodes; i++)
            {
                adjacencyList.Add(new Fragas(i));
            }
        }

        public List<Fragas> FuncaoDijkstra(int source)
        {
            adjacencyList[source].distance = 0;
            List<int> neighbors = new List<int>();
            bool allClosed = false;

            double edgeWeight = 0;

            do
            {
                neighbors = graph.GetNeighbors(source);

                foreach (int index in neighbors)
                {
                    edgeWeight = graph.EdgeWeight(source, index);

                    if (adjacencyList[index].closed)
                    {
                        continue;
                    }
                    else if (adjacencyList[index].distance > (edgeWeight + adjacencyList[source].distance))
                    {
                        adjacencyList[index].distance = (edgeWeight + adjacencyList[source].distance);
                        adjacencyList[index].lastIndex = source;
                    }
                }

                adjacencyList[source].closed = true;

                int nextSource = -1;
                double temp = 99999;

                foreach (int index in neighbors)
                {
                    if (!adjacencyList[index].closed && (adjacencyList[index].distance < temp))
                    {

                        nextSource = index;
                        temp = adjacencyList[index].distance;
                    }
                }

                if (nextSource == -1)
                {
                    foreach (Fragas nextFragas in adjacencyList)
                    {
                        if (!nextFragas.closed)
                        {
                            nextSource = nextFragas.index;

                            break;
                        }
                    }
                }

                source = nextSource;
                allClosed = (nextSource == -1);
            } while (!allClosed);

            return adjacencyList;
        }

        private int GetMinimumDistance(int[] distances, bool[] visited)
        {
            int minDistance = int.MaxValue;
            int minVertex = 0;

            for (int i = 0; i < graph.Nodes; i++)
            {
                if (!visited[i] && distances[i] <= minDistance)
                {
                    minDistance = distances[i];
                    minVertex = i;
                }
            }

            return minVertex;
        }

        public void PrintTest(int source)
        {
            foreach (Fragas fraga in adjacencyList)
            {
                Console.WriteLine($"\r\nDistância mais curta do vértice {graph.NodeLabel(source)} {source} ao vértice {graph.NodeLabel(fraga.index)} {fraga.index} é: {adjacencyList[fraga.index].distance} Anterior {adjacencyList[fraga.index].lastIndex}");
            }
        }

    }


    public class Fragas
    {
        public int index { get; set; }
        public bool closed { get; set; }
        public double distance { get; set; }
        public int lastIndex { get; set; }

        public Fragas(int index, bool closed = false, double distance = 999999, int lastIndex = -1)
        {
            this.index = index;
            this.closed = closed;
            this.distance = distance;
            this.lastIndex = lastIndex;
        }
    }

}
