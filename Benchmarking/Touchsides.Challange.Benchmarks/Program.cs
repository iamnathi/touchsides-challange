using BenchmarkDotNet.Running;

namespace Touchsides.Challange.Benchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<WordStatisticBenchmarks>();
        }
    }
}