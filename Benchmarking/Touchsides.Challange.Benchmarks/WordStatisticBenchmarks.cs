using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using System.Threading.Tasks;
using Touchsides.Challange.Models;
using Touchsides.Challange.Services;

namespace Touchsides.Challange.Benchmarks
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class WordStatisticBenchmarks
    {
        private string _bookContents;
        private static readonly WordStatistic _wordStatistics = new WordStatistic();

        [GlobalSetup]
        public async Task Setup()
        {
            var fileService = new FileService(new ConsoleLogger());
            _bookContents = await fileService.DownloadContentFromWebAsync("https://www.gutenberg.org/files/2600/2600-0.txt");
        }

        [Benchmark]
        public void AddOrUpdateWordStatics()
        {
            _wordStatistics.AddOrUpdateWordsFromBookContents(_bookContents);
        }
    }
}