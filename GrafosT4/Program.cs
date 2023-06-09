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
            Console.WriteLine("Grafo Matriz Com algoritmo Ford Fukerson");
            Console.WriteLine("\r");

            GraphMatriz graph = new GraphMatriz();
            graph.LoadFile("files/exemplo2.txt");
            //graph.GraphPrint();

            FordFukerson fukerson = new FordFukerson(graph);
            fukerson.CalculateFordFukerson(0, 5);

            Console.WriteLine("O fluxo máximo possível é: " + fukerson.MaxFlow);
        }
    }

}
