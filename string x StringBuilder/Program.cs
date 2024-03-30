using BenchmarkDotNet.Running;
using CommandLine;

namespace string_x_StringBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var Methods = new Methods();
            Console.WriteLine(Methods.PrintString());
            Console.WriteLine(Methods.PrintStringBuilder());

            var results = BenchmarkRunner.Run<Methods>();
        }
    }
}

