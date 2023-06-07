using System;
using Graph;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public class BreadthFirstSearch
    {
        private List<int> fifo = new List<int>();
        private List<Fragas> adjacency = new List<Fragas>();
        private List<int> accessed = new List<int>();
        private Graph graph;
        private int destiny = -1;

        public BreadthFirstSearch(Graph graph)
        {
            this.graph = graph;

            for (int i = 0; i < graph.Nodes; i++)
            {
                adjacency.Add(new Fragas(i));
            }
        }

        public void StartBreadth(int source, int destiny = -1)
        {
            this.destiny = destiny;
            if (BreadthSearch(source))
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

                //Console.WriteLine("\nImprimindo o caminho...");
                //foreach (int item in this.lifo)
                //{
                //    Console.Write(graph.NodeLabel(item));

                //    if (this.lifo.Last() != item)
                //    {
                //        Console.Write(" -> ");
                //    }
                //}
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

        private bool BreadthSearch(int source)
        {
            fifo.Add(source);
            adjacency.ElementAt(source).closed = true;

            while (fifo.Count > 0)
            {
                int current = fifo.First();
                fifo.RemoveAt(0);
                accessed.Add(current);

                if (current == destiny)
                {
                    return true;
                }

                List<int> neighbors = graph.GetNeighbors(current);

                foreach (int item in neighbors)
                {
                    if (adjacency.ElementAt(item).closed || fifo.Contains(item))
                    {
                        continue;
                    }

                    fifo.Add(item);
                    adjacency.ElementAt(item).closed = true;
                }
            }

            return false;
        }
    }
}
