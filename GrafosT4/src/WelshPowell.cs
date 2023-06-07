using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class WelshPowell : GraphList
    {
        public List<int> colors = new List<int>();
        public List<WelshNode> WelshNodes = new List<WelshNode>();

        public void WelshPowellColoring()
        {
            int color = 1;

            for (int i = 0; i < Nodes; i++)
            {
                WelshNode node = new WelshNode(i, this.GetNeighbors(i).Count);
                this.WelshNodes.Add(node);
            }

            this.WelshNodes = this.WelshNodes.OrderByDescending(x => x.Degree).ToList();

            while (this.WelshNodes.Where(x => x.Color == -1).ToList().Count > 0)
            {
                this.colors.Add(color);

                foreach (var uncoloredNode in this.WelshNodes.Where(x => x.Color == -1).ToList())
                {
                    // Se ele não tiver vizinhos que possuam essa cor, atribuir.
                    if (this.IsColorAvailable(uncoloredNode.Index, color))
                    {
                        uncoloredNode.Color = color;
                    }

                }

                color++;
            }
        }

        private bool IsColorAvailable(int index, int color)
        {
            List<int> colorsUsed = new List<int>();
            var neighbors = this.GetNeighbors(index);

            foreach (var neighbor in neighbors)
            {
                colorsUsed.Add(this.WelshNodes.Single(x => x.Index == neighbor).Color);
            }

            return !colorsUsed.Contains(color);
        }

    }

    public class WelshNode
    {
        public WelshNode(int index, int degree)
        {
            this.Index = index;
            this.Degree = degree;
            this.Color = -1;
        }

        public int Index;
        public int Degree;
        public int Color;
    }
}