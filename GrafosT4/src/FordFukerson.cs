using Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace Graph
{

    public class FordFukerson
    {
        public GraphList Graph;
        public List<FordEdge>[] FordList;

        public FordFukerson(GraphList graph)
        {
            Graph = graph;
        }


        public bool CalculateFordFukerson(int from, int to)
        {
            //Clona o grafo
            FordList = new List<FordEdge>[Graph.List.Count()];

            for (int i = 0; i < Graph.List.Count(); i++)
            {
                List<FordEdge> fordList = new List<FordEdge>();


                foreach (Edge e in Graph.List[i])
                {
                    FordEdge fordEdge = new FordEdge(e.Weight);
                    fordList.Add(fordEdge);
                }

                FordList[i] = fordList;
            }


            FordList[0].ElementAt(0).CurrentFlow = 10;




            return true;
        }



    }


    public class FordEdge
    {
        public double MaxCapacity;
        public double CurrentFlow;

        public FordEdge(double nodeWeight)
        {
            MaxCapacity = nodeWeight;
            CurrentFlow = 0;
        }

        // TODO : Criar função para retornar a quantidade disponível

    }
}
