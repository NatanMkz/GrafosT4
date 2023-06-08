using System;
using Graph;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Meus Grafos");
            Console.WriteLine("\r");

            Console.WriteLine("Grafo Lista Com algoritmo Ford Fukerson");
            Console.WriteLine("\r");

            GraphList graphList = new GraphList();

            graphList.LoadFile("files/slides_modificado.txt");
            graphList.GraphPrint();

            FordFukerson fukerson = new FordFukerson(graphList);

            fukerson.CalculateFordFukerson(0, 1);

            //Console.WriteLine("Imprimindo Busca em Profundidade (BFS): ");

            //DepthFirstSearch dfs = new DepthFirstSearch(graphList);
            //dfs.StartDepth(0, 4);
        }
    }
}
