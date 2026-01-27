using System.Diagnostics;
using CoDi.Data.Contracts.CurrentSong;
using CoDi.Data.Contracts.Entities;
using CoDi.Logic.Contracts.CurrentSong;
using CoDi.Logic.Contracts.CurrentSong.Model;

namespace CoDi.Logic.CurrentSong;

public class CurrentSongInspector(ISongRepository songRepository, IDailySongStatsRepository dailySongStatsRepository) : ICurrentSongInspector
{
    public async Task<SongModel?> GetCurrentSongAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        Process[] localProcesses = Process.GetProcesses();

        SongModel? song = null;

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

                song = ExtractSongInfo(processWindowTitle);
                
                break;
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

        if (song == null)
            return null;

        var dbEntrySong = await songRepository.AddSongAsync(
            new Song { Artist = song.Artist, Name = song.Name }, cancellationToken);

        if (dbEntrySong == null)
            return null;

        await dailySongStatsRepository.AddOrUpdateDailySongStatsAsync(dbEntrySong.Id, cancellationToken);

        return song;
    }

    private SongModel ExtractSongInfo(string processWindowTitle)
    {
        var songInfo = processWindowTitle.Split(" - ", StringSplitOptions.RemoveEmptyEntries);

        var artist = songInfo[0].Trim();
        var songName = string.Join(" - ", songInfo.Skip(1)).Trim();

        return new SongModel() { Artist = artist, Name = songName };
    }
}