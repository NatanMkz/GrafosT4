using System;
using Graph;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public class DepthFirstSearch
    {
        private List<int> lifo = new List<int>();
        private List<Fragas> adjacency = new List<Fragas>();
        private List<int> accessed = new List<int>();
        private Graph graph;
        private int destiny = -1;

        public DepthFirstSearch(Graph graph)
        {
            this.graph = graph;

            for (int i = 0; i < graph.Nodes; i++)
            {
                adjacency.Add(new Fragas(i));
            }
        }

        public void StartDepth(int source, int destiny = -1)
        {
            this.destiny = destiny;

            if (DepthRecursive(source))
            {
                Console.WriteLine($"Foi possível localizar na busca em profundidade entre os pontos {source} e {destiny}.");
                Console.WriteLine("Imprimindo acessados...");
                foreach (int item in this.accessed)
                {
                    Console.Write(graph.NodeLabel(item));

                    if (this.accessed.Last() != item)
                    {
                        Console.Write(" -> ");
                    }
                }

                Console.WriteLine("\nImprimindo o caminho...");
                foreach (int item in this.lifo)
                {
                    Console.Write(graph.NodeLabel(item));

                    if (this.lifo.Last() != item)
                    {
                        Console.Write(" -> ");
                    }
                }

            }
            else
            {
                Console.WriteLine("Não foi possível localizar um caminho para os pontos selecionados.");
                Console.WriteLine("Imprimindo acessados...");

                foreach (int item in this.accessed)
                {
                    Console.Write(graph.NodeLabel(item));

                    if (this.accessed.Last() != item)
                    {
                        Console.Write(" -> ");
                    }
                }
            }
        }

        private bool DepthRecursive(int source)
        {
            accessed.Add(source);
            lifo.Add(source);

            if (source == this.destiny)
            {
                return true;
            }

            List<int> neighbors = graph.GetNeighbors(source);

            if (neighbors.Contains(this.destiny))
            {
                return DepthRecursive(this.destiny);
            }

            foreach (int item in neighbors)
            {
                if (adjacency.ElementAt(item).closed || lifo.Contains(item))
                {
                    continue;
                }
                else if (DepthRecursive(item))
                {
                    return true;
                }
            }

            adjacency.ElementAt(source).closed = true;
            lifo.Remove(source);

            return false;
        }
    }
}
