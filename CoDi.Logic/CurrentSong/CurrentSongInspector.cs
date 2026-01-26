using System.Diagnostics;
using CoDi.Logic.Contracts.CurrentSong;
using CoDi.Logic.Contracts.CurrentSong.Model;

namespace CoDi.Logic.CurrentSong;

public class CurrentSongInspector : ICurrentSongInspector
{
    public Task<SongModel?> GetCurrentSongAsync(CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();

            Process[] localProcesses = Process.GetProcesses();

            foreach (var process in localProcesses)
            {
                cancellationToken.ThrowIfCancellationRequested();

                try
                {
                    var processName = process.ProcessName;
                    var processWindowTitle = process.MainWindowTitle;

                    if (!processName.Contains("Spotify", StringComparison.OrdinalIgnoreCase))
                        continue;

                    if (processWindowTitle.Contains("Spotify", StringComparison.OrdinalIgnoreCase))
                        continue;

                    var songInfo = processWindowTitle.Split(" - ", StringSplitOptions.RemoveEmptyEntries);

                    var artist = songInfo[0].Trim();
                    var songName = string.Join(" - ", songInfo.Skip(1)).Trim();

                    return new SongModel() { Artist = artist, Name = songName };
                }
                catch
                {
                    return null;
                }
                finally
                {
                    process.Dispose();
                }
                
            }

            return null;
        }, cancellationToken);
    }
}