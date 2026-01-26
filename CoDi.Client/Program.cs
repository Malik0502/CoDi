using System.Threading.Channels;
using CoDi.Client.CurrentSong;
using CoDi.Logic.Contracts.CurrentSong;
using CoDi.Logic.CurrentSong;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoDi.Client
{
    class Program
    {

        static async Task Main(string[] args)
        {
            IHostBuilder builder = CreateHostBuilder(args);
            var host = builder.Build();

            var songWatcher = host.Services.GetRequiredService<SongWatcher>();

            Console.WriteLine("CoDi Client is now listening...");
            Console.WriteLine("Press Ctrl+C to cancel the Service");

            using var cts = new CancellationTokenSource();

            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            var songTask = RunWatcherAsync(
                name: "Song",
                interval: TimeSpan.FromSeconds(5),
                work: () => songWatcher.WatchSongs(cts.Token),
                cancellationToken: cts.Token);

            await Task.WhenAll(songTask);
        }

        private static async Task RunWatcherAsync(
            string name, 
            TimeSpan interval, 
            Func<Task<string?>> work, 
            CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var result = await work();
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        Console.WriteLine($"[{name}]");
                        Console.WriteLine($"{result}");
                    }
                }
                catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine("Cancellation was requested...");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[{name}] ERROR: {ex.Message}");
                }

                await Task.Delay(interval, cancellationToken);
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureServices(services =>
            {
                services.AddScoped<ICurrentSongInspector, CurrentSongInspector>();
                services.AddTransient<SongWatcher>();
            });
        }


    }
}
