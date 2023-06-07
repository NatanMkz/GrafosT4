using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graph
{
    public class GraphList : Graph
    {
        public List<Edge>[] List;
        public GraphList(
            bool directed = false,
            bool weighted = false,
            int nodes = 0,
            int edges = 0,
            List<string>? nodeNames = null
        ) : base(directed, weighted, nodes, edges)
        {
            NodeNames = nodeNames ?? new List<string>();
            Nodes = NodeNames.Count;
            List = new List<Edge>[NodeNames.Count];

            for (int i = 0; i < NodeNames.Count; i++)
            {
                List[i] = new List<Edge>();
            }
        }

        public List<Edge>[] GetList()
        {
            return List;
        }

        public override bool NodeInsert(string label)
        {
            List<Edge>[] newList = new List<Edge>[++Nodes];

            for (int i = 0; i < Nodes - 1; i++)
            {
                newList[i] = List[i];
            }

            newList[Nodes - 1] = new List<Edge>();
            List = newList;
            NodeNames.Add(label);

            return true;
        }

        public override bool NodeDelete(string label)
        {
            if (Nodes == 0)
            {
                return false;
            }


            int indexDel = NodeNames.IndexOf(label);

            if (indexDel == -1)
            {
                return false;
            }

            int indexNew = 0;
            List<Edge>[] newList = new List<Edge>[Nodes - 1];

            NodeNames.Remove(label);

            for (int i = 0; i < Nodes; i++)
            {
                if (indexDel == i)
                {
                    continue;
                }

                foreach (var edge in List[i])
                {
                    if (edge.ToNode == indexDel)
                    {
                        List[i].Remove(edge);

                        break;
                    }
                }

                newList[indexNew] = List[i];
                indexNew++;
            }

            List = newList;
            Nodes--;

            return true;
        }

        public override int NodeIndex(string name)
        {
            return NodeNames.Contains(name) ? NodeNames.IndexOf(name) : -1;
        }

        public override string NodeLabel(int index)
        {
            return NodeNames[index] ?? "";
        }

        public override List<int> GetNeighbors(int node)
        {
            List<int> neighbors = new List<int>();

            if (node >= List.Length || node < 0)
            {
                return neighbors;
            }

            neighbors = List.ElementAt(node).Select(edge => edge.ToNode).ToList();

            return neighbors;
        }

        public override bool EdgeInsert(int from, int to, double weight = 1)
        {
            if (!Directed)
            {
                List[to].Add(new Edge(from, weight));
            }

            List[from].Add(new Edge(to, weight));
            Edges++;

            return true;
        }

        public override bool EdgeDelete(int from, int to)
        {
            List[from].Remove(List[from].First(a => a.ToNode == to));

            if (!Directed)
            {
                List[to].Remove(List[to].First(b => b.ToNode == from));
            }

            return true;

        }

        public override bool EdgeExists(int from, int to)
        {
            foreach (var edge in List[from])
            {
                if (edge.ToNode == to)
                {
                    return true;
                }
            }

            return false;
        }

        public override double EdgeWeight(int from, int to)
        {
            foreach (var edge in List[from])
            {
                if (edge.ToNode == to)
                {
                    return edge.Weight;
                }
            }

            return 0;
        }



        public override void GraphPrint()
        {
            for (int i = 0; i < Nodes; i++)
            {
                Console.Write(NodeLabel(i));
                foreach (var edge in List[i])
                {
                    Console.Write($"({edge.ToNode}, {edge.Weight})");
                }
                Console.WriteLine("\r");
            }
        }

        public void PrintAdjacentEdge(int index)
        {
            List<int> neighbors = this.GetNeighbors(index);

            foreach (int neighbor in neighbors)
            {
                Console.WriteLine($"{neighbor} ");
            }
        }

    }

    public class Edge
    {
        public int ToNode;
        public double Weight;

        public Edge(int toNode, double weight)
        {
            ToNode = toNode;
            Weight = weight;
        }

    }
}
