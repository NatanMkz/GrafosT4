using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public abstract class Graph
    {
        public List<string> NodeNames;

        public Graph(
            bool directed = false,
            bool weighted = false,
            int nodes = 0,
            int edges = 0
        )
        {
            Directed = directed;
            Weighted = weighted;
            Nodes = nodes;
            Edges = edges;
        }

        public bool Directed { get; protected set; }

        public bool Weighted { get; protected set; }

        public int Nodes { get; protected set; }

        public int Edges { get; protected set; }

        public abstract bool NodeInsert(string name);

        public abstract bool NodeDelete(string name);

        public abstract int NodeIndex(string name);

        public abstract string NodeLabel(int node);

        public abstract List<int> GetNeighbors(int node);

        public abstract bool EdgeInsert(int from, int to, double weight = 1);

        public abstract bool EdgeDelete(int from, int to);

        public abstract bool EdgeExists(int from, int to);

        public abstract double EdgeWeight(int from, int to);

        public abstract void GraphPrint();

        public bool LoadFile(string path)
        {
            if (!File.Exists(path)) throw new Exception("Arquivo não encontrado.");

            bool setted = false;
            int nodes = 0;
            int edges = 0;

            foreach (string line in File.ReadLines(path))
            {
                string[] items = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (!setted)
                {
                    setted = true;
                    nodes = Convert.ToInt32(items[0]);
                    edges = Convert.ToInt32(items[1]);
                    Directed = Convert.ToBoolean(Convert.ToInt32(items[2]));
                    Weighted = Convert.ToBoolean(Convert.ToInt32(items[3]));

                    continue;
                }

                string nodeFrom = Convert.ToString(items[0]);
                string nodeTo = Convert.ToString(items[1]);
                double weight = Weighted ? Convert.ToDouble("0" + items[2].Replace(".", ",")) : 0;

                if (this.NodeIndex(nodeFrom) == -1) this.NodeInsert(nodeFrom);
                if (this.NodeIndex(nodeTo) == -1) this.NodeInsert(nodeTo);

                this.EdgeInsert(this.NodeIndex(nodeFrom), this.NodeIndex(nodeTo), weight);
            }
            Nodes = NodeNames.Count;
            Edges = edges;

            //if (Nodes != nodes) throw new Exception("O número de vértices informado no cabeçalho do arquivo difere da quantidade inserida.");
            //if (Edges != edges) throw new Exception("O número de arestas informado no cabeçalho do arquivo difere da quantidade inserida.");

            return true;
        }


        public bool IsPlanar()
        {
            if (Nodes <= 2)
            {
                // V ≤ 2
                return true;
            }
            else if (HasThreeCicle())
            {
                // A ≤ 3V – 6
                return (Edges <= (3 * Nodes) - 6) && !this.HasK5();
            }

            // A ≤ 2V – 4
            return (Edges <= (2 * Nodes) - 4) && !this.HasK5();
        }

        public bool HasK5()
        {
            if ((Nodes < 5) || (Edges < 10))
            {
                return false;
            }

            for (int v1 = 0; v1 < Nodes; v1++)
            {
                List<int> neighbors1 = this.GetNeighbors(v1);
                if (neighbors1.Count < 4) continue;

                foreach (var v2 in neighbors1)
                {
                    List<int> neighbors2 = this.GetNeighbors(v2);
                    List<int> commmon_v1v2 = neighbors1.Intersect(neighbors2).ToList();

                    if (
                        (neighbors2.Count < 4) ||
                        !neighbors2.Contains(v1) ||
                        (commmon_v1v2.Count < 3)
                    )
                    {
                        continue;
                    }

                    foreach (var v3 in commmon_v1v2)
                    {
                        List<int> neighbors3 = this.GetNeighbors(v3);
                        List<int> commmon_v1v2v3 = commmon_v1v2.Intersect(neighbors3).ToList();

                        if (
                            (neighbors3.Count < 4) ||
                            !neighbors3.Contains(v1) ||
                            !neighbors3.Contains(v2) ||
                            (commmon_v1v2v3.Count < 2)
                        )
                        {
                            continue;
                        }


                        foreach (var v4 in commmon_v1v2v3)
                        {
                            List<int> neighbors4 = this.GetNeighbors(v4);
                            List<int> commmon_v1v2v3v4 = commmon_v1v2v3.Intersect(neighbors4).ToList();

                            if (
                                (neighbors4.Count < 4) ||
                                !neighbors4.Contains(v1) ||
                                !neighbors4.Contains(v2) ||
                                !neighbors4.Contains(v3) ||
                                (commmon_v1v2v3v4.Count < 1)
                            )
                            {
                                continue;
                            }


                            foreach (var v5 in commmon_v1v2v3v4)
                            {
                                List<int> neighbors5 = this.GetNeighbors(v5);

                                if (
                                    (neighbors5.Count >= 4) &&
                                    neighbors5.Contains(v1) &&
                                    neighbors5.Contains(v2) &&
                                    neighbors5.Contains(v3) &&
                                    neighbors5.Contains(v4)
                                )
                                {
                                    return true;
                                }

                            }
                        }
                    }
                }
            }

            return false;
        }

        public bool HasThreeCicle()
        {
            for (int node = 0; node < Nodes; node++)
            {
                List<int> neighbors1 = this.GetNeighbors(node);

                foreach (var neighbor1 in neighbors1)
                {
                    List<int> neighbors2 = this.GetNeighbors(neighbor1);


                    foreach (var neighbor2 in neighbors2)
                    {
                        if (node == neighbor2) return true;
                    }
                }


            }


            return false;
        }

    }
}
