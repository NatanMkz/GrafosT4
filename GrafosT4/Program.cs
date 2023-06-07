using System;
using Graph;
using System.Diagnostics;

namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Meus Grafos");
            Console.WriteLine("\r");

            WelshPowell welsh = new WelshPowell();
            //welsh.LoadFile(Environment.CurrentDirectory + "/files/r250-66-65.txt");
            welsh.LoadFile(Environment.CurrentDirectory + "/files/r1000-234-234.txt");
            //welsh.LoadFile(Environment.CurrentDirectory + "/files/C4000-260-X.txt");

            Stopwatch stopwatchWelsh = new Stopwatch();

            stopwatchWelsh.Start();
            welsh.WelshPowellColoring();
            stopwatchWelsh.Stop();

            long tempoDecorridoWelsh = stopwatchWelsh.ElapsedMilliseconds;

            Console.WriteLine($"Tempo de execução WelshPowell: {tempoDecorridoWelsh} ms");
            Console.WriteLine($"Quantidade de cores usadas: {welsh.colors.Count}");


            Dsatur dsatur = new Dsatur();
            //dsatur.LoadFile(Environment.CurrentDirectory + "/files/r250-66-65.txt");
            dsatur.LoadFile(Environment.CurrentDirectory + "/files/r1000-234-234.txt");
            //dsatur.LoadFile(Environment.CurrentDirectory + "/files/C4000-260-X.txt");

            Stopwatch stopwatchDsatur = new Stopwatch();

            stopwatchDsatur.Start();
            dsatur.DsaturColoring();
            stopwatchDsatur.Stop();

            long tempoDecorridoDsatur = stopwatchDsatur.ElapsedMilliseconds;

            Console.WriteLine($"Tempo de execução Dsatur: {tempoDecorridoDsatur} ms");
            Console.WriteLine($"Quantidade de cores usadas: {dsatur.colors.Count}");
        }
    }
}
