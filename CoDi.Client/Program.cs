using CoDi.Client.CurrentSong;
using CoDi.Client.RunningSoftware;
using CoDi.Common.Constants;
using CoDi.Data;
using CoDi.Data.Contracts.CurrentSong;
using CoDi.Data.CurrentSong;
using CoDi.Logic.Contracts.CurrentSong;
using CoDi.Logic.Contracts.RunningSoftware;
using CoDi.Logic.CurrentSong;
using CoDi.Logic.RunningSoftware;
using Microsoft.EntityFrameworkCore;
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

            using (var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<CoDiContext>();
                await db.Database.MigrateAsync();
            }

            var songWatcher = host.Services.GetRequiredService<SongWatcher>();
            var softwareWatcher = host.Services.GetRequiredService<SoftwareWatcher>();

            Console.WriteLine("CoDi Client is now listening...");
            Console.WriteLine("Press Ctrl+C to cancel the Service");

            using var cts = new CancellationTokenSource();

            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            
            await Task.WhenAll(CreateTasks(songWatcher, softwareWatcher, cts));
        }

        private static List<Task> CreateTasks(SongWatcher songWatcher, SoftwareWatcher softwareWatcher, CancellationTokenSource cts)
        {
            var tasks = new List<Task>();
            
            var songTask = RunWatcherAsync(
                name: "Song",
                interval: TimeSpan.FromSeconds(WatcherCallInterval.SongWatcherInterval),
                work: () => songWatcher.WatchSongs(cts.Token)!,
                cancellationToken: cts.Token);

            var softwareTask = RunWatcherAsync(
                name: "Software",
                interval: TimeSpan.FromSeconds(WatcherCallInterval.SoftwareWatcherInterval),
                work: () => softwareWatcher.WatchSoftware(cts.Token)!,
                cancellationToken: cts.Token);

            tasks.Add(songTask);
            tasks.Add(softwareTask);

            return tasks;
        }

        private static async Task RunWatcherAsync(string name, TimeSpan interval, Func<Task<string?>> work, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var result = await work();
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        Console.WriteLine($"[{name}] {result}");
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
                services.AddDbContext<CoDiContext>();
                services.AddTransient<SongWatcher>();
                services.AddScoped<ICurrentSongInspector, CurrentSongInspector>();
                services.AddScoped<ISongRepository, SongRepository>();
                services.AddScoped<IDailySongStatsRepository, DailySongStatsRepository>();
                services.AddScoped<ISoftwareInspector, SoftwareInspector>();
                services.AddTransient<SoftwareWatcher>();
            });
        }


    }
}
