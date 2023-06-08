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

            Console.WriteLine("Grafo Lista");
            Console.WriteLine("\r");

            GraphList graphList = new GraphList(false, true);
            string directory = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\files\\slides_modificado.txt";
            graphList.LoadFile(directory);
            graphList.GraphPrint();

            Console.WriteLine("Imprimindo Busca em Profundidade (BFS): ");

            DepthFirstSearch dfs = new DepthFirstSearch(graphList);
            dfs.StartDepth(0, 4);
        }
    }
}
