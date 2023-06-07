using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace Graph
{


    public class Dsatur : GraphList
    {
        public List<int> colors = new List<int>();
        public List<DsaturNode> DsaturNodes = new List<DsaturNode>();

        public void DsaturColoring()
        {
            int color = 1;

            for (int i = 0; i < Nodes; i++)
            {
                DsaturNode node = new DsaturNode(i, this.GetNeighbors(i).Count);
                this.DsaturNodes.Add(node);
            }

            this.DsaturNodes = this.DsaturNodes.OrderByDescending(x => x.Degree).ToList();
            this.DsaturNodes[0].Color = color;
            this.colors.Add(color);

            while (this.DsaturNodes.Where(x => x.Color == -1).ToList().Count > 0)
            {
                int index = this.GetMaxSaturationNode();

                if (index == -1)
                {
                    break;
                }

                color = this.GetNextColor(index);
                this.DsaturNodes.Single(x => x.Index == index).Color = color;
            }

            this.DsaturNodes = this.DsaturNodes.OrderBy(x => x.Index).ToList();

            //foreach (var node in this.DsaturNodes)
            //{
            //    Console.WriteLine("Vertex {0}: Color {1}", node.Index, node.Color);
            //}
        }

        public int GetNextColor(int index)
        {
            List<int> colorsUsed = new List<int>();
            var neighbors = this.GetNeighbors(index);


            foreach (var neighbor in neighbors)
            {
                colorsUsed.Add(this.DsaturNodes.Single(x => x.Index == neighbor).Color);
            }


            foreach (var color in this.colors)
            {
                if (!colorsUsed.Contains(color))
                {
                    return color;
                }
            }

            int nextColor = this.colors.Max();
            nextColor++;
            this.colors.Add(nextColor);

            return nextColor;
        }

        public int GetMaxSaturationNode()
        {
            int saturation;

            foreach (var node in this.DsaturNodes)
            {
                saturation = 0;
                var neighbors = this.GetNeighbors(node.Index);

                foreach (var neighbor in neighbors)
                {
                    if (this.DsaturNodes.Single(x => x.Index == neighbor).Color != -1)
                    {
                        saturation++;
                    }
                }

                node.Saturarion = saturation;
            }

            List<DsaturNode> notColored = this.DsaturNodes.Where(x => x.Color == -1).ToList();

            if (notColored.Count == 0)
            {
                return -1;
            }

            //Alterado retorno no .net 3.1 Favor testar o metodo de dsatur novamente
            return notColored.Where(x => x.Saturarion == notColored.Max(x => x.Saturarion)).ToList().Max(x => x.Degree);
        }

    }

    public class DsaturNode
    {
        public DsaturNode(int index, int degree)
        {
            this.Index = index;
            this.Saturarion = 0;
            this.Degree = degree;
            this.Color = -1;
        }

        public int Index;
        public int Saturarion;
        public int Degree;
        public int Color;
    }
}
