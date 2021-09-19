using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using Touchsides.Challange.Models;
using Touchsides.Challange.Services;

namespace Touchsides.Challange
{
    public class Program
    {
        
        static CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();

        public static async Task Main(string[] args)
        {
            var serviceProvider = ConfigureServices(new ServiceCollection());

            Console.WriteLine("Touchsides Technical Challange : Nkosinathi Koza's Submission");
            Console.WriteLine();

            Console.CancelKeyPress += new ConsoleCancelEventHandler(OnCancelKeyPressed);

            do
            {
                Console.Write("Enter the absolute URI to the file (or press X to exit program): ");
                var uriString = Console.ReadLine();

                if (!Uri.IsWellFormedUriString(uriString, UriKind.Absolute))
                {
                    Console.WriteLine($"{uriString} is not a valid absolute URI.");
                    Console.WriteLine();
                    continue;
                }

                var bookUri = new Uri(uriString, UriKind.Absolute);
                var fileService = serviceProvider.GetRequiredService<IFileService>();
                var wordStats = new WordStatistic();
                var bookContents = string.Empty;

                Console.WriteLine("This might take some time, stay put...");
                Console.WriteLine();

                if (!string.IsNullOrWhiteSpace(bookUri.Scheme))
                {
                    bookContents = await fileService.DownloadContentFromWebAsync(bookUri, CancellationTokenSource.Token);                    
                }
                else
                {
                    bookContents = await fileService.DownloadContentFromWebAsync(bookUri, CancellationTokenSource.Token);
                }

                wordStats.AddOrUpdateWordsFromBookContents(bookContents);

                //var dirtyWords = bookContents
                //    .Replace(Environment.NewLine, " ")
                //    .Split(EnglishPantuationCharacters);

                //for (int index = 0; index < dirtyWords.Length; index++)
                //{
                //    wordStats.AddOrUpdateWord(dirtyWords[index]);
                //}

                Console.WriteLine(wordStats);

                Console.WriteLine();
            } while (true);            
        }

        private static void OnCancelKeyPressed(object sender, ConsoleCancelEventArgs args)
        {
            args.Cancel = true;
            CancellationTokenSource.Cancel();
            CancellationTokenSource.Dispose();
            Console.WriteLine("Goodbye...");
            Environment.Exit(0);
        }

        private static IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILogger, ConsoleLogger>();
            services.AddSingleton<IFileService, FileService>();

            return services.BuildServiceProvider();
        }
    }
}